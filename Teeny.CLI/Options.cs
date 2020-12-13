using CommandLine;

namespace Teeny.CLI
{
    public class Options
    {
        [Option('f', "file", Required = false, HelpText = "Input file-name including path to be compiled.")]
        public string InputFile { get; set; }

        [Option('s', "silent", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Silent { get; set; }
    }
}