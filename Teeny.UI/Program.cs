using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;
using Teeny.Core;

namespace Teeny.UI
{

    class Program
    {
        private static bool keepRunning = true;
         
        static void Main(string[] args)
        {
            StringBuilder code = new StringBuilder();
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                Program.keepRunning = false;
            };

            Console.WriteLine("Enter input:");
            while (Program.keepRunning)
            {
                string  newline = Console.ReadLine();
                code.Append(newline);
            }
            Scanner scanner = new Scanner();
            List<TokenRecord> scanned = scanner.Scan(code.ToString());
            Console.WriteLine('\n');
            ConsoleTable
                 .From<TokenRecord>(scanned)
                 .Configure(o => o.NumberAlignment = Alignment.Right)
                   .Write(Format.Alternative);

            Console.ReadKey();    
       
        }
    }
}
