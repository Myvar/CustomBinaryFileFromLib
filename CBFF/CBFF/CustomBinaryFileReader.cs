using CBFF.Internals.AST.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CBFF
{
    public class CustomBinaryFileReader
    {
       public string Pattern { get; set; }
        private List<IAst> ast { get; set; }

        public CustomBinaryFileReader(string pattern, string filepath)
        {
            Pattern = pattern;
            ast = CBFF.Internals.PatternParser.ParsePattern(pattern);

            if (File.Exists(filepath))
            {

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                List<IAst> obj = (List<IAst>)formatter.Deserialize(stream);
                ast = obj;
                stream.Close();




               /* using (BinaryReader reader = new BinaryReader(File.Open(filepath, FileMode.Open)))
                {
                    var base64 = reader.ReadString();
                    var data = Convert.FromBase64String(base64);
                    var output = Encoding.UTF8.GetString(data);
                    List<IAst> deserializedIAst = JsonConvert.DeserializeObject<List<IAst>>(output);
                    ast = deserializedIAst;
                }*/
            }

        }

        public object GetValue(string xpath)
        {
            //regex stuff
            string pat = @"(?<segmint>((([A-Za-z]|[0-9])+)(.)?)+)(\[(?<index>[0-9]+)\])?";
            var m = Regex.Match(xpath, pat);
            retvalue = null;
            //data we want
            string Xpath = "";
            List<string> Segments = new List<string>();
            int index = 0;

            //lets get the data
            Xpath = xpath;
            try
            {
                index = int.Parse(m.Groups["index"].Value);
            }
            catch (Exception e)
            {

            }
            var s1 = m.Groups["segmint"].Value.Split('.');
            Segments = s1.ToList();

            foreach (var s in ast)
            {
                CurrentObj = s;
                if (SetObjectInAst("", Segments))
                {
                    return retvalue;
                  
                }
            }

            return retvalue;

        }
        private object retvalue = null;
        private int CurrentSegmint = 0;
        private IAst CurrentObj = null;
        private bool SetObjectInAst(object value, List<string> Segments)
        {
            try
            {
                if (CurrentSegmint < Segments.Count + 1)
                {
                    if (CurrentObj is Internals.AST.Attribute)
                    {
                        var co = CurrentObj as Internals.AST.Attribute;
                       retvalue = co.Value;
                        return true;
                    }

                    if (CurrentObj is Internals.AST.Node)
                    {
                        var co = CurrentObj as Internals.AST.Node;

                        foreach (var c in co.Nodes)
                        {
                            if (c.Name == Segments[CurrentSegmint])
                            {
                                CurrentObj = c;
                                break;
                            }
                        }
                        foreach (var c in co.Attributes)
                        {
                            if (c.Name == Segments[CurrentSegmint])
                            {
                                CurrentObj = c;
                                break;
                            }
                        }
                    }
                    if (CurrentObj is Internals.AST.ArrayNode)
                    {
                        var co = CurrentObj as Internals.AST.ArrayNode;
                        foreach (var c in co.Nodes)
                        {
                            if (c.Name == Segments[CurrentSegmint])
                            {
                                CurrentObj = c;
                                break;
                            }
                        }
                        foreach (var c in co.Attributes)
                        {
                            if (c.Name == Segments[CurrentSegmint])
                            {
                                CurrentObj = c;
                                break;
                            }
                        }
                    }


                    CurrentSegmint++;
                    SetObjectInAst(value, Segments);

                }
                CurrentSegmint = 0;
            }
            catch (Exception e)
            {

            }
            return false;
        }

    }
}
