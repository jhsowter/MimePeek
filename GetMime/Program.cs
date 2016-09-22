using CommandLine;
using System;
using System.IO;
using System.Text;

namespace GetMime
{
    internal class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to read.")]
        public string InputFile { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("Supply a file. e.g. GetMine -i resume.pdf");
            return usage.ToString();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine(Mime.Get(options.InputFile));
            }

#if DEBUG
            Console.Read();
#endif
        }
    }
}