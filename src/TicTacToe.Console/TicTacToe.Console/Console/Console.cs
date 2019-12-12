using TicTacToe.Console.Interfaces;

namespace TicTacToe.Console.Console
{
    public class Console : IConsole
    {
        public void WriteLine(object value)
        {
            System.Console.WriteLine(value.ToString());
        }

        public void Write(object value)
        {
            System.Console.Write(value.ToString());
        }

        public string ReadString()
        {
            return System.Console.ReadLine();
        }

        public int ReadInt()
        {
            int inputInt;
            bool parseResult;
            do
            {
                var rawInt = System.Console.ReadLine();
                parseResult = int.TryParse(rawInt, out inputInt);
                if (!parseResult)
                {
                    System.Console.WriteLine("Please, enter your number again: ");
                }
            } while (!parseResult);

            return inputInt;
        }
    }
}