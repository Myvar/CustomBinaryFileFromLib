using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF.Internals.AST
{
    [Serializable()] 
    public class Node : Base.IAst
    {
        public List<Base.IAst> Nodes { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}