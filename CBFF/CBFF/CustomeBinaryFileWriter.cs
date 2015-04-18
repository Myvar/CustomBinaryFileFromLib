using CBFF.Internals.AST.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using CBFF.Internals.AST;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace CBFF
{
    public class CustomeBinaryFileWriter
    {
        public string Pattern { get; set; }
        private List<IAst> ast { get; set; }

        public CustomeBinaryFileWriter(string pattern)
        {
            Pattern = pattern;
            ast = CBFF.Internals.PatternParser.ParsePattern(pattern);
        }

        public void SetValue(string xpath, object value)
        {
            //regex stuff
            string pat = @"(?<segmint>((([A-Za-z]|[0-9])+)(.)?)+)(\[(?<index>[0-9]+)\])?";
            var m = Regex.Match(xpath, pat);

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
                if (SetObjectInAst(value, Segments))
                {
                    break;
                }
            }



        }

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
                        co.Value = value;
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
            catch(Exception e)
            {

            }
            return false;
        }

        public void WriteFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, ast);
            stream.Close();

            /*using (BinaryWriter b = new BinaryWriter(new FileStream(path, FileMode.Create)))
            {
                string output = JsonConvert.SerializeObject(ast);
                var bytes = Encoding.UTF8.GetBytes(output);
                var base64 = Convert.ToBase64String(bytes);
                b.Write(base64);                 
                
            }*/
        }
     
     
    }
}
