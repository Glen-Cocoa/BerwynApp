using System;
using System.Collections;
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
                //find the GUID of the row with the highest sum of V1&V2
                int currentMax = 0;
                string currentMaxID = "";


                for(var i = 0; i < 10001; i++)
                {
                    string Val1Str = Val1[i].Replace("\"", "");
                    string Val2Str = Val2[i].Replace("\"", "");
                    int Val1Val = 0;
                    int Val2Val = 0;
                    Int32.TryParse(Val1Str, out Val1Val);
                    Int32.TryParse(Val2Str, out Val2Val);

                  
                    if(Val1Val + Val2Val > currentMax)
                    {
                        currentMax = Val1Val + Val2Val;
                        currentMaxID = GUID[i];
                    }
                   
                }

                //Find all duplicate GUID Values

                List<string> duplicateGUIDVals = new List<string>();
                var GUIDToBeChecked = "";

                for(var j = 1; j<10001; j++)
                {
                    GUIDToBeChecked = GUID[j];

                    for(var hello = 0; hello < 10001; hello++)
                    {
                        if(GUIDToBeChecked == GUID[hello] && j != hello)
                        {
                            duplicateGUIDVals.Add(GUID[hello]);
                        }
                    }
               
                }
                //find average length of Val3
                int lengthTally = 0;
                
                for(var k = 0; k < 10001; k++)
                {
                    string CurVal3 = Val3[k];
                    int CurLen = CurVal3.Length;
                    lengthTally += CurLen;
                }

                int avgLen = lengthTally / 10000;
                string avgLenStr = avgLen.ToString();

                //construct messy string to be output
                string totalNumRecords = count.ToString();
                string largestGUIDVal = currentMaxID;
                string duplicateIDs = string.Join(Environment.NewLine, duplicateGUIDVals.ToArray());
                string avgLenFinal = avgLenStr;

                string answer = "Hello Steve! The total number of records in this .csv is {0}, The GUID of the largest sum of Val1 and Val2 is {1}, and the average length of Val3 is {2}. The GUID of duplicate entries are {3}";
                var stringToLog = string.Format(answer, totalNumRecords, largestGUIDVal, avgLenFinal, duplicateIDs);

                Console.WriteLine(stringToLog);
            }
        }
    }
}
