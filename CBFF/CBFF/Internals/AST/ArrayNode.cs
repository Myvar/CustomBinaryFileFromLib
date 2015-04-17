using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF.Internals.AST
{
    public class ArrayNode : Base.iAst
    {
        public List<Base.iAst> Nodes { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
