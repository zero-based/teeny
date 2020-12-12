using ConsoleTables;
using System;
using System.Text;
using Teeny.Core;

namespace Teeny.UI
{
    internal static class Program
    {
        private static bool KeepReading { get; set; } = true;

        static Program()
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                KeepReading = false;
            };
        }

        private static void Main(string[] args)
        {
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
            while (KeepReading)
            {
                var line = Console.ReadLine();
                code.Append(line);
            }

            try
            {
                var scanner = new Scanner();
                var tokensTable = scanner.Scan(code.ToString());

                Console.WriteLine();
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
