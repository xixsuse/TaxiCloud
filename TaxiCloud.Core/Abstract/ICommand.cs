namespace TaxiCloud.Core.Abstract
{
    public interface ICommand
    {
        /// <summary>
        /// Выполнить команду
        /// </summary>
        void Execute();

        /// <summary>
        /// Название команды
        /// </summary>
        string Name { get; }
    }
}