using CBFF.Internals.AST.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF
{
    public class CustomeBinaryFileWriter
    {
        public string Pattern { get; set; }
        private List<iAst> ast {get;set;}

        public CustomeBinaryFileWriter(string pattern)
        {
            Pattern = pattern;
            ast = CBFF.Internals.PatternParser.ParsePattern(pattern);
        }

       

        public void WriteFile(string path)
        {

        }

    }
}
