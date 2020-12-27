using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine;
using ConsoleTables;
using Teeny.Core.Scan;

namespace Teeny.CLI
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = Assembly.GetExecutingAssembly().GetName().Name!;

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
            scanner.Scan(code);

            if (opts.Silent) return scanner.TokensTable;

            Console.WriteLine();
            ConsoleTable.From(scanner.TokensTable)
                .Write(Format.Alternative);

            if (scanner.ErrorTable.Count != 0)
            {
                Console.WriteLine();
                ConsoleTable.From(scanner.ErrorTable)
                    .Write(Format.Alternative);
            }

            return scanner.TokensTable;
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
                code.Append("\n");
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