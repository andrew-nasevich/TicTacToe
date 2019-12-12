namespace TicTacToe.Console.Interfaces
{
    public interface IConsole
    {
        void WriteLine(string value);

        void Write(string value);

        string ReadString(string message);

        int ReadInt(string message);
    }
}