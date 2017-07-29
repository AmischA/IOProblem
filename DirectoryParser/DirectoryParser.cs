using System;
using System.IO;

namespace NixSolutions.IOProblem.DirectoryParsers
{
    public class DirectoryParser
    {
        private const string defaultPath = @"c:\users\public";
        private readonly string receivedPath;
        internal DirectoryNode RootNode { get; set; }
        
        public DirectoryParser(string path = defaultPath)
        {
            receivedPath = path;
            RootNode = new DirectoryNode(receivedPath);
        }

        public void BuildDirectoryTree()
        {
            BuildDirectoryTree(RootNode);
        }

        // recursively builds directory tree starting from root path
        private void BuildDirectoryTree(DirectoryNode directoryNode)
        {
            DirectoryInfo directoryNodeInfo = new DirectoryInfo(directoryNode.GetDirectoryFullName());

            AddDirectoryInformation(directoryNode);
           
            foreach (DirectoryInfo subDirectory in directoryNodeInfo.GetDirectories())
            {
                try
                {
                    DirectoryNode childNode = new DirectoryNode(subDirectory.FullName);
                    directoryNode.AddChildNode(childNode);
                    BuildDirectoryTree(childNode);
                }
                catch (UnauthorizedAccessException ignored)
                {
                    //NOP
                }
            }
        }

        // adds directory information to the node such as number of files, total size, list of files extensions
        private void AddDirectoryInformation(DirectoryNode directoryNode)
        {
            DirectoryInfo directoryNodeInfo = new DirectoryInfo(directoryNode.GetDirectoryFullName());
            try
            {
                long totalSize = 0;
                foreach (FileInfo file in directoryNodeInfo.GetFiles())
                {
                    string fileExtension = (file.Extension.Length > 0)? file.Extension.Remove(0, 1) : "";
                    directoryNode.AddFileExtension(fileExtension);
                    totalSize += file.Length;
                }
                int numberOfFiles = directoryNodeInfo.GetFiles().Length;
                directoryNode.NumberOfFilesInDirectory = numberOfFiles;
                directoryNode.TotalSize = totalSize;
            }
            catch (UnauthorizedAccessException ignored)
            {
                // NOP
            }
        }
    }
}