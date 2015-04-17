using CBFF.Internals.AST;
using CBFF.Internals.AST.Base;
using Eto.Parse;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CBFF.Internals
{
    public static class PatternParser
    {
        public static List<iAst> ParsePattern(string pat)
        {
            var ret = new List<iAst>();

            var g = new CBFF.Internals.CBFFGrammar();
            var match = g.Match(pat.Replace("/r/n"," ").Replace("/n"," ").Replace("\t"," "));

            foreach (var m in match.Matches)
            {
                if (m.Name == "object")
                {
                    var e = Iterate(m.Matches);
                    ret.Add(e[0]);
                }
                if (m.Name == "array")
                {
                    var r = Iterate(m.Matches);
                    var e = new List<ArrayNode>();
                    foreach (var i in r)
                    {
                        e.Add(new ArrayNode() { Attributes = i.Attributes, Name = i.Name, Nodes = i.Nodes });
                    }
                    ret.Add(e[0]);
                }
            }

   

            return ret;
        }

        private static List<Node> Iterate(MatchCollection m)
        {
            List<Node> Nodes = new List<Node>();

            
                var node = new Node();
                node.Nodes = new List<iAst>();
                node.Attributes = new List<Attribute>();
               // node.Name = m.Matches["NodeName"].StringValue;
                foreach(var mm in m)
                {
                    if (mm.Name == "object")
                    {
                        var r = Iterate(mm.Matches);
                        node.Nodes.AddRange(r);
                    }
                    if (mm.Name == "array")
                    {
                        var r = Iterate(mm.Matches);
                        var e = new List<ArrayNode>();
                        foreach(var i in r)
                        {
                            e.Add(new ArrayNode() {Attributes = i.Attributes , Name = i.Name , Nodes = i.Nodes });
                        }
                        node.Nodes.AddRange(e);
                    }
                    if (mm.Name == "NodeName")
                    {
                        node.Name = mm.StringValue;
                    }
                    if (mm.Name == "property")
                    {
                        string s = mm.StringValue.Replace(";", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                        string[] data = s.Split(':');
                        node.Attributes.Add(new Attribute() { Name = data[0].Trim(), Type = data[1].Trim() });
                    }
                }

                Nodes.Add(node);
            
            return Nodes;
        }

    }
}
