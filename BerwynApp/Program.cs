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
                List<string> GUID = new List<string>();
                List<string> Val1 = new List<string>();
                List<string> Val2 = new List<string>();
                List<string> Val3 = new List<string>();
                int count = 0;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    GUID.Add(line);
                    count++;
                    Console.WriteLine(count);
                }
            }
        }
    }
}
