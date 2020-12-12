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

            const string banner = @"

                ████████ ███████ ███████ ███    ██ ██    ██ 
                   ██    ██      ██      ████   ██  ██  ██  
                   ██    █████   █████   ██ ██  ██   ████   
                   ██    ██      ██      ██  ██ ██    ██    
                   ██    ███████ ███████ ██   ████    ██    

                 a   t e e n y   t i n y   c o m p i l e r

            ";

            Console.WriteLine(banner);
            Console.WriteLine("Enter tiny code (press ctrl + c to stop):");

            var code = new StringBuilder();
            while (_keepReading)
            {
                var line = Console.ReadLine();
                code.Append(line);
            }

            try
            {
                var scanner = new Scanner();
                var tokensTable = scanner.Scan(code.ToString());

                ConsoleTable.From(tokensTable)
                    .Write(Format.Alternative);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR:");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }

        }
    }
}
