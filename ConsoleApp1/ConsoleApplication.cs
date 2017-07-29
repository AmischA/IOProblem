using System;
using NixSolutions.IOProblem.DirectoryParsers;

namespace NixSolutions.IOProblem.Applications
{
    class ConsoleApplication
    {
        static void Main(string[] args)
        {
            new ConsoleApplication().RunApplication();
        }

        public void RunApplication()
        {
            ConsoleDirectoryParser parser;
            string input = GetInputFromUser();
            Console.Clear();

            if (input.Length > 0)
            {
                parser = new ConsoleDirectoryParser(input);
            }
            else
            {
                parser = new ConsoleDirectoryParser();
            }

            parser.BuildDirectoryTree();
            parser.PrintDirectoryTree();

            Console.WriteLine("\nPress Enter to exit");
            Console.ReadLine();            
        }

        private string GetInputFromUser()
        {
            Console.Write("Enter directory path: ");
            string input = Console.ReadLine();
            return input;
        }
    }
}