using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NixSolutions.IOProblem.DirectoryParsers
{
    class DirectoryNode
    {
        private string directoryFullName;

        // collection of all subdirectories of the current directory
        private IEnumerable<DirectoryNode> childNodes;

        private IDictionary<string, int> extensionCount;

        public int NumberOfFilesInDirectory { get; set; }

        // size of all files in the directory in Bytes
        public long TotalSize { get; set; }

        public DirectoryNode(string directoryFullName)
        {
            this.directoryFullName = directoryFullName;
            childNodes = new List<DirectoryNode>();
            extensionCount = new Dictionary<string, int>();
        }

        public void AddChildNode(DirectoryNode childNode)
        {
            ((List<DirectoryNode>)childNodes).Add(childNode);
        }

        public IEnumerable<DirectoryNode> GetChildNodes()
        {
            return childNodes;
        }

        public bool HasChildNodes()
        {
            return childNodes.Count() > 0;
        }

        // adds file extension to the collection and updates count
        public void AddFileExtension(string extensionName)
        {
            if (extensionCount.ContainsKey(extensionName))
            {
                extensionCount[extensionName] += 1;
            }
            else
            {
                extensionCount[extensionName] = 1;
            }
        }

        public IDictionary<string, int> GetFileExtensions()
        {
            return extensionCount;
        }

        public string GetDirectoryFullName()
        {
            return directoryFullName;
        }

        public string GetDirectoryName()
        {
            return new DirectoryInfo(directoryFullName).Name;
        }
    }
}