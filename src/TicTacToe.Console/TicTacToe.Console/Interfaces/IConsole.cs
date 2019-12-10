namespace TicTacToe.Console.Interfaces
{
    public interface IConsole
    {
        void WriteLine(object value);

        string ReadLine();
    }
}