using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BerwynApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(@"../../csv/test.csv"))
            {
                List<string> listA = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    listA.Add(line);
                    Console.Write(line);
                }
            }
        }
    }
}
