using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF
{
    public class CustomBinaryFileReader
    {
        public string Pattern { get; set; }

        public CustomBinaryFileReader(string pattern)
        {
            Pattern = pattern;
        }

    }
}
