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


            var str = "file{	header	{		filevertion:string;		filename:string;			}	data	[		x:int;		y:int;		c:color;	]}color {	a:byte;	r:byte;	g:byte;	b:byte;}";
           
            var Wr = new CustomeBinaryFileWriter(str);


            Wr.WriteFile("test.bin");
        }
    }
}
