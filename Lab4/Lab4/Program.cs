using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using LabsLibrary;
using McMaster.Extensions.CommandLineUtils;

namespace Lab4
{
    [Command(Name = "Lab4", Description = "Example: Lab4 run lab1 -i INPUT1.txt -o OUTPUT1.txt")]
    [Subcommand(typeof(VersionCommand))]
    [Subcommand(typeof(RunCommand))]
    [Subcommand(typeof(SetPathCommand))]
    public class Program
    {
        public static int Main(string[] args)
        {
            return CommandLineApplication.Execute<Program>(args);
        }

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify a subcommand.");
            app.ShowHelp();
            return 1;
        }
    }

    [Command(Description = "Display program version")]
    public class VersionCommand
    {
        private void OnExecute()
        {
            Console.WriteLine("Author: Andriy Nechytailo");
            string appVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown version";
            Console.WriteLine($"Version: {appVersion}");
        }
    }

    [Command(Description = "Run lab")]
    public class RunCommand
    {
        [Argument(0, Name = "lab", Description = "The lab to run (lab1, lab2, or lab3)")]
        public string Lab { get; }

        [Option("-i|--input", Description = "Input file")]
        public string Input { get; }

        [Option("-o|--output", Description = "Output file")]
        public string Output { get; }

        private int OnExecute(IConsole console)
        {
            string labValue = Lab;

            if (string.IsNullOrEmpty(labValue))
            {
                console.Error.WriteLine("Invalid lab value. Use lab1, lab2, or lab3.");
                return 1;
            }

            string inputFilePath = Input ?? string.Empty;
            string outputFilePath = Output ?? string.Empty;

            if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
            {
                string labPath = Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User);

                if (!string.IsNullOrWhiteSpace(labPath))
                {
                    inputFilePath = Path.Combine(labPath, "INPUT.txt");
                    outputFilePath = Path.Combine(labPath, "OUTPUT.txt");
                }
                else
                {
                    string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    inputFilePath = Path.Combine(homeDirectory, "INPUT.txt");
                    outputFilePath = Path.Combine(homeDirectory, "OUTPUT.txt");
                }
            }

            if (labValue == "lab1")
            {
                var lab1 = new Lab1(inputFilePath, outputFilePath);
                lab1.Run();
            }
            else if (labValue == "lab2")
            {
                var lab2 = new Lab2(inputFilePath, outputFilePath);
                lab2.Run();
            }
            else if (labValue == "lab3")
            {
                var lab3 = new Lab3(inputFilePath, outputFilePath);
                lab3.Run();
            }
            else
            {
                console.Error.WriteLine("Invalid lab value. Use lab1, lab2, or lab3.");
                return 1;
            }

            return 0;
        }
    }

    [Command(Description = "Set LAB_PATH environment variable")]
    public class SetPathCommand
    {
        [Option("-p|--path", Description = "Path to the folder with input and output files")]
        public string Path { get; }

        private int OnExecute(IConsole console)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                console.Error.WriteLine("The --path parameter is required.");
                return 1;
            }

            Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.User);
            console.WriteLine($"LAB_PATH set to {Path}");

            return 0;
        }
    }
}

