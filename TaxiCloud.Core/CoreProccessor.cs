using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TaxiCloud.Core.Abstract;

namespace TaxiCloud.Core
{
    public class CoreProccessor : ICommandExecutor
    {
        private readonly List<CommandWrapper> _commands;
        private readonly ConcurrentDictionary<CommandWrapper, CommandStatus> _statuses;
        private readonly ILogger _logger;

        public CoreProccessor()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _statuses = new ConcurrentDictionary<CommandWrapper, CommandStatus>();
            _commands = new List<CommandWrapper>();
            var threadCommandStarter = new Thread(CommandStarter)
            {
                IsBackground = true,
                Name = "CommandStarterThread"
            };
            threadCommandStarter.Start();
        }

        private void CommandStarter()
        {
            while (true)
            {
                lock (_commands)
                {
                    var forDel = new List<CommandWrapper>();
                    var dt = DateTime.Now;
                    foreach (var cmd in _commands)
                    {
                        var cntRunningTasks = _statuses.Count(x => x.Value == CommandStatus.Running);
                        if (cntRunningTasks >= Environment.ProcessorCount)
                        {
                            _logger.Warn($"Can't start tasks. Resources are full. [{cntRunningTasks} / {Environment.ProcessorCount}]");
                            break;
                        }
                        switch (cmd.Type)
                        {
                            case CommandExecuteType.At:
                                if (cmd.Date > dt)
                                    continue;
                                break;
                            case CommandExecuteType.After:
                                if ((dt - cmd.DateTimePulled) < cmd.Time)
                                    continue;
                                break;
                            case CommandExecuteType.Daily:
                                if (cmd.LastExecute.Day == dt.Day)
                                    continue;
                                if (dt.Hour < cmd.Hour ||
                                    dt.Minute < cmd.Minute ||
                                    dt.Second < cmd.Second)
                                    continue;
                                break;
                            case CommandExecuteType.Every:
                                if ((dt - cmd.LastExecute) < cmd.Time)
                                    continue;
                                break;
                            case CommandExecuteType.Now:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (_statuses[cmd] == CommandStatus.Running)
                        {
                            _logger.Info($"Command {cmd.Command.Name} waits until prev is completed");
                            continue;
                        }
                        cmd.LastExecute = dt;
                        if (cmd.Type != CommandExecuteType.Daily &&
                            cmd.Type != CommandExecuteType.Every)
                            forDel.Add(cmd);
                        CreateCommandThread(cmd);
                    }
                    foreach (var f in forDel)
                        _commands.Remove(f);
                }
                Thread.Sleep(1000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void CreateCommandThread(CommandWrapper command)
        {
            var threadCommandContext = new Thread(RunCommand)
            {
                IsBackground = true
            };
            threadCommandContext.Start(command);
        }

        private void RunCommand(object args)
        {
            if (!(args is CommandWrapper cmd))
                return;
            _logger.Debug($"Command {cmd.Command.Name} started");
            _statuses[cmd] = CommandStatus.Running;
            cmd.Command.Execute();
            _statuses[cmd] = CommandStatus.Completed;
        }

        public void ExecuteCommand(ICommand command)
        {
            PushCommand(new CommandWrapper
            {
                Command = command,
                DateTimePulled = DateTime.Now,
                Type = CommandExecuteType.Now
            });
        }

        public void ExecuteCommandAfter(ICommand command, TimeSpan time)
        {
            PushCommand(new CommandWrapper
            {
                Command = command,
                DateTimePulled = DateTime.Now,
                Type = CommandExecuteType.After,
                Time = time
            });
        }

        public void ExecuteCommandAt(ICommand command, DateTime date)
        {
            PushCommand(new CommandWrapper
            {
                Command = command,
                DateTimePulled = DateTime.Now,
                Type = CommandExecuteType.At,
                Date = date
            });
        }

        public void ExecuteCommandDaily(ICommand command, int hour, int minute, int second)
        {
            PushCommand(new CommandWrapper
            {
                Command = command,
                DateTimePulled = DateTime.Now,
                Type = CommandExecuteType.Daily,
                Hour = hour,
                Minute = minute,
                Second = second,
                LastExecute = new DateTime(1900, 1, 1)
            });
        }

        public void ExecuteCommandEvery(ICommand command, TimeSpan time)
        {
            PushCommand(new CommandWrapper
            {
                Command = command,
                DateTimePulled = DateTime.Now,
                Type = CommandExecuteType.Every,
                Time = time,
                LastExecute = new DateTime(1900, 1, 1)
            });
        }

        private void PushCommand(CommandWrapper commandWrapper)
        {
            lock (_commands)
                _commands.Add(commandWrapper);
            _statuses[commandWrapper] = CommandStatus.Waiting;
        }

        private class CommandWrapper
        {
            public ICommand Command;
            public DateTime DateTimePulled;
            public CommandExecuteType Type;
            public TimeSpan Time;
            public DateTime Date;
            public int Hour;
            public int Minute;
            public int Second;
            public DateTime LastExecute;
        }

        private enum CommandExecuteType
        {
            Now,
            After,
            At,
            Daily,
            Every
        }

        private enum CommandStatus
        {
            Waiting,
            Running,
            Completed
        }
    }
}