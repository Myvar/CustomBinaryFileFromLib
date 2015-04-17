using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF.Internals.AST
{
    public class Node : Base.iAst
    {
        public List<Node> Nodes { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}