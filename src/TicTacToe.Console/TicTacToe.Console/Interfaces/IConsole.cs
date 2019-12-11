namespace TicTacToe.Console.Interfaces
{
    public interface IConsole
    {
        void WriteLine(object value);

        void Write(object value);

        string ReadLine();
    }
}