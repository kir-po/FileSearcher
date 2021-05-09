using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher
{
    public class SearchResults
    {
        public int FilesCount { get; set; }
        public int FoundFiles { get; set; }
        public string CurrentDirectory { get; set; }
        public string CurrentFoundFilePath { get; set; }
        public Exception Exception { get; set; }
    }
}
