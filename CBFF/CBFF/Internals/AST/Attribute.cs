using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBFF.Internals.AST
{
    public class Attribute : Base.iAst
    {
        public AttributeType Type { get; set; }
        public object Value { get; set; }
    }

    public enum AttributeType
    {
        @string, @int, @double, binary, node, @array
    }
}
