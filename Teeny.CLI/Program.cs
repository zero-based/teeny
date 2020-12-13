using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine;
using ConsoleTables;
using Teeny.Core;

namespace Teeny.CLI
{
    internal static class Program
    {
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

            var helpWriter = new StringWriter();
            var parser = new Parser(with => with.HelpWriter = helpWriter);
            parser.ParseArguments<Options>(args)
                .WithParsed(RunWithOptions)
                .WithNotParsed(errors => HandleParseError(errors, helpWriter));
        }

        private static void HandleParseError(IEnumerable<Error> errors, StringWriter helpWriter)
        {
            var enumerable = errors.ToList();
            if (enumerable.IsVersion() || enumerable.IsHelp())
                Console.WriteLine(helpWriter.ToString());
            else
                Console.Error.WriteLine(helpWriter.ToString());
        }

        private static void RunWithOptions(Options opts)
        {
            try
            {
                var code = ReadWithOptions(opts);
                var tokensTable = ScanWithOptions(code, opts);
                Debug.WriteLine(tokensTable);
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
        }

        private static string ReadWithOptions(Options opts)
        {
            string code;

            if (opts.InputFile == null)
            {
                code = ReadUserInput();
            }
            else
            {
                code = ReadInputFile(opts.InputFile);

                if (opts.Silent) return code;

                Console.WriteLine($"Tiny code from {opts.InputFile}:\n");
                Console.WriteLine(code);
            }

            return code;
        }

        private static IEnumerable<TokenRecord> ScanWithOptions(string code, Options opts)
        {
            var scanner = new Scanner();
            var tokensTable = scanner.Scan(code);

            if (opts.Silent) return tokensTable;

            Console.WriteLine();
            ConsoleTable.From(tokensTable)
                .Write(Format.Alternative);

            return tokensTable;
        }

        private static string ReadUserInput()
        {
            Console.WriteLine("Enter tiny code (press ctrl + c to stop):");

            var keepReading = true;
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                keepReading = false;
            };

            var code = new StringBuilder();
            while (keepReading)
            {
                var line = Console.ReadLine();
                code.Append(line);
            }

            return code.ToString();
        }

        private static string ReadInputFile(string inputFile)
        {
            if (!Regex.IsMatch(inputFile, @".*\.t$"))
                throw new IOException("The file must be of type '*.t'.");

            try
            {
                using var sr = new StreamReader(inputFile);
                return sr.ReadToEnd();
            }
            catch (IOException)
            {
                throw new IOException("The file could not be read or doesn't exist.");
            }
        }

        private static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {message}");
            Console.ResetColor();
        }
    }
}
