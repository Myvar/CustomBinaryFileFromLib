using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBFF;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {


            var str = "file{header{id:string;}data:string;}";

            var Wr = new CustomeBinaryFileWriter(str);
            Wr.SetValue("file.data", "tets data");
            Wr.SetValue("file.header.id", "testfileid");

            Wr.WriteFile("test.bin");
        }
    }
}
