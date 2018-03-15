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
                //count keeps track of current row and also total rows
                int count = 0;

                //Lists to keep track of values after being parsed
                List<string> GUID = new List<string>();
                List<string> Val1 = new List<string>();
                List<string> Val2 = new List<string>();
                List<string> Val3 = new List<string>();

                //Loops through entire test.csv and assigns each value to its corresponding list
                while (!reader.EndOfStream)
                {
                    var data = reader.ReadLine();
                    count++;
                    string[] values = data.Split(',');
                    string GUIDVal = values[0];
                    string Val1Val = values[1];
                    string Val2Val = values[2];
                    string Val3Val = values[3];
                    GUID.Add(GUIDVal);
                    Val1.Add(Val1Val);
                    Val2.Add(Val2Val);
                    Val3.Add(Val3Val);
                }
            }
        }
    }
}
