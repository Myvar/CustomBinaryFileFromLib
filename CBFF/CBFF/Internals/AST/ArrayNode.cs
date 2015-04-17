using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF.Internals.AST
{
    public class ArrayNode : Base.IAst
    {
        public List<Base.IAst> Nodes { get; set; }
        public List<Attribute> Attributes { get; set; }
        public Dictionary<string,List<object>> Values { get; set; }

        public ArrayNode()
        {
            Values = new Dictionary<string, List<object>>();
        }
    }
}
