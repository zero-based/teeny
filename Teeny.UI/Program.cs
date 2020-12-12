using ConsoleTables;
using System;
using System.Text;
using Teeny.Core;

namespace Teeny.UI
{
    internal static class Program
    {
        private static bool _keepReading = true;

        private static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                _keepReading = false;
            };

            Console.WriteLine("Enter Tiny Code (press ctrl + c to stop):");

            var code = new StringBuilder();
            while (_keepReading)
            {
                var line = Console.ReadLine();
                code.Append(line);
            }

            var scanner = new Scanner();
            var tokensTable = scanner.Scan(code.ToString());

            ConsoleTable.From(tokensTable)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);

        }
    }
}
