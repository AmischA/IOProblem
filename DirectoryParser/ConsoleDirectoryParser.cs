using System;
using System.Collections.Generic;

namespace NixSolutions.IOProblem.DirectoryParsers
{
    public class ConsoleDirectoryParser
    {
        private DirectoryParser defaultDirectoryParser;

        public ConsoleDirectoryParser()
        {
            defaultDirectoryParser = new DirectoryParser();
        }

        public ConsoleDirectoryParser(string path)
        {
            defaultDirectoryParser = new DirectoryParser(path);
        }

        public ConsoleDirectoryParser(DirectoryParser defaultDirectoryParser)
        {
            this.defaultDirectoryParser = defaultDirectoryParser;
        }

        public void BuildDirectoryTree()
        {
            defaultDirectoryParser.BuildDirectoryTree();
        }

        public void PrintDirectoryTree()
        {
            DirectoryNode rootNode = defaultDirectoryParser.RootNode;
            int rootDirectoryNameLength = rootNode.GetDirectoryName().Length;
            int rootDirectoryFullNameLength = rootNode.GetDirectoryFullName().Length;
            int length = rootDirectoryFullNameLength - rootDirectoryNameLength;

            // write the part of the absolute root directory path without the name of the directory
            // directory name will be added after calling the method PrintDirectoryTree(rootNode)
            Console.Write(rootNode.GetDirectoryFullName().Substring(0, length));

            PrintDirectoryTree(rootNode);
        }

        // recursively prints every directory name
        private void PrintDirectoryTree(DirectoryNode node, int directoryTreeHeight = 0)
        {
            // adds dashes before directory's name. Number of dashes depends on how high
            // the current node is in the directory tree
            for (int index = 0; index < directoryTreeHeight; index++)
            {
                Console.Write('-');
            }
            Console.WriteLine(node.GetDirectoryName());

            PrintDirectoryInformation(node, directoryTreeHeight);

            directoryTreeHeight++;

            foreach (DirectoryNode childNode in node.GetChildNodes())
            {
                PrintDirectoryTree(childNode, directoryTreeHeight);
            }
        }

        // prints directory information for the node such as number of files, total size, list of files extensions
        private void PrintDirectoryInformation(DirectoryNode node, int directoryTreeHeight = 0)
        {
            if (node.NumberOfFilesInDirectory > 0)
            {
                string spacesBeforeInformation = "";
                for (int index = 0; index < directoryTreeHeight; index++)
                {
                    spacesBeforeInformation += " ";
                }

                string numberOfFiles = node.NumberOfFilesInDirectory + ((node.NumberOfFilesInDirectory > 1) ? " files" : " file");
                string formatedTotalSize = FormatTotalSize(node.TotalSize);

                Console.Write($"{spacesBeforeInformation}Contains: {numberOfFiles}, Total size {formatedTotalSize}, Extensions: ");

                // prints information about file extensions and their number
                foreach (KeyValuePair<string, int> extensionCountPair in node.GetFileExtensions())
                {
                    Console.Write(extensionCountPair.Key + " - " + extensionCountPair.Value + ", ");
                }
                Console.WriteLine();
            }
        }

        // convert directory size from long to string and add appropriate units. Received totalSize is in bytes
        private string FormatTotalSize(long totalSize)
        {
            long factor = 1024;
            if (totalSize < factor)                                            // bytes
            {
                return totalSize.ToString() + " B";
            }
            else if (totalSize < factor * factor)                              // kilobytes
            {
                return (totalSize / factor).ToString() + " KB";
            }
            else if (totalSize < factor * factor * factor)                     // megabytes
            {
                return (totalSize / factor / factor).ToString() + " MB";
            }
            else if (totalSize < factor * factor * factor * factor)            // gigabytes
            {
                return (totalSize / factor / factor / factor).ToString() + " GB";
            }
            else
            {
                throw new ArgumentException($"Total size can't be more than {factor * factor * factor * factor - 1} bytes");
            }
        }
    }
}