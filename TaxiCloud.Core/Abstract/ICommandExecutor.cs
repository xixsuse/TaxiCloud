using System;

namespace TaxiCloud.Core.Abstract
{
    public interface ICommandExecutor
    {
        /// <summary>
        /// Выполнить команду немедленно 
        /// </summary>
        /// <param name="command">Команда</param>
        void ExecuteCommand(ICommand command);

        /// <summary>
        /// Выполнить команда в определенное время
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="date">Дата и время</param>
        void ExecuteCommandAt(ICommand command, DateTime date);

        /// <summary>
        /// Выполнить команду через определенное время
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="time">Время</param>
        void ExecuteCommandAfter(ICommand command, TimeSpan time);

        /// <summary>
        /// Выполнять команду каждый день в определенное время
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="hour">Час</param>
        /// <param name="minute">Минута</param>
        /// <param name="second">Секунда</param>
        void ExecuteCommandDaily(ICommand command, int hour, int minute, int second);

        /// <summary>
        /// Выполнять команду каждый промежуток времени
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="time">Время</param>
        void ExecuteCommandEvery(ICommand command, TimeSpan time);
    }
}