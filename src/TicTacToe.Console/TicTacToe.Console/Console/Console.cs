using System;
using TicTacToe.Console.Interfaces;

namespace TicTacToe.Console.Console
{
    public class Console : IConsole
    {
        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        public void Write(string value)
        {
            System.Console.Write(value);
        }

        public string ReadString(string message)
        {
            string readString;
            do
            {
                WriteLine(message);
                readString = System.Console.ReadLine();
            } while (String.IsNullOrEmpty(readString));

            return readString;
        }

        public int ReadInt(string message)
        {
            int inputInt;
            bool parseResult;
            do
            {
                var rawInt = ReadString(message);
                parseResult = Int32.TryParse(rawInt, out inputInt);
            } while (!parseResult);

            return inputInt;
        }
    }
}