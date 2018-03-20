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
                int numberOfDuplicates = 0;

                //Lists to keep track of values after being parsed
                List<string> GUID = new List<string>();
                List<string> Val1 = new List<string>();
                List<string> Val2 = new List<string>();
                List<string> Val3 = new List<string>();
                List<int> ValSumList = new List<int>();
                List<string> duplicateGUIDVals = new List<string>();
                Hashtable myTable = new Hashtable();


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


                for(var i = 0; i < count; i++)
                {
                    string Val1Str = Val1[i].Replace("\"", "");
                    string Val2Str = Val2[i].Replace("\"", "");
                    int Val1Val = 0;
                    int Val2Val = 0;
                    int together = 0;
                    Int32.TryParse(Val1Str, out Val1Val);
                    Int32.TryParse(Val2Str, out Val2Val);
                    together = Val1Val + Val2Val;
                    ValSumList.Add(together);
                  
                    if(Val1Val + Val2Val > currentMax)
                    {
                        currentMax = Val1Val + Val2Val;
                        currentMaxID = GUID[i];
                    }
                   
                }

                //Find all duplicate GUID Values
                for(var x = 0; x<count; x++)
                {
                    try
                    {
                        myTable.Add(GUID[x], count);
                    }
                    catch
                    {
                        duplicateGUIDVals.Add(GUID[x]);
                        numberOfDuplicates++;
                    }
                }
/*
                var GUIDToBeChecked = "";

                for(var j = 1; j<count; j++)
                {
                    GUIDToBeChecked = GUID[j];

                    for(var hello = 0; hello < count; hello++)
                    {
                        if(GUIDToBeChecked == GUID[hello] && j != hello)
                        {
                            duplicateGUIDVals.Add(GUID[hello]);
                            numberOfDuplicates++;
                        }
                    }
               
                }
*/
                //find average length of Val3
                int lengthTally = 0;
                
                for(var k = 0; k < count; k++)
                {
                    string CurVal3 = Val3[k];
                    int CurLen = CurVal3.Length;
                    lengthTally += CurLen;
                }

                int avgLen = lengthTally / count;
                string avgLenStr = avgLen.ToString();

                //construct messy string to be output
                string totalNumRecords = (count-1).ToString();
                string largestGUIDVal = currentMaxID;
                string duplicateIDs = string.Join(Environment.NewLine, duplicateGUIDVals.ToArray());
                string avgLenFinal = avgLenStr;

                string answer = "Hello Steve! The total number of records in this .csv is {0}, The GUID of the largest sum of Val1 and Val2 is {1}, and the average length of Val3 is {2}. The GUID of duplicate entries are {3}";
                var stringToLog = string.Format(answer, totalNumRecords, largestGUIDVal, avgLenFinal, duplicateIDs);

                Console.WriteLine(stringToLog);

                //Write to text file???

                StreamWriter SW = new StreamWriter(@"output.csv");

                for (int z = 0; z < count; z++)
                {
                    string line = "{0},{1},{2},{3}";
                    //GUID
                    string G2W = GUID[z];
                    //Val1+Val2
                    string ValSum = ValSumList[z].ToString();
                    //IsDuplicateOrNot
                    string isDuplicate = "N";
                    for(var y = 0; y < numberOfDuplicates; y++)
                    {
                        if(G2W == duplicateGUIDVals[y])
                        {
                            isDuplicate = "Y";
                        }
                    }
                    //Val3GreaterLength
                    string isGreaterThanAverage = "N";
                    int Val3Length = Val3[z].Length;
                    if (Val3Length > avgLen)
                    {
                        isGreaterThanAverage = "Y";
                    }

                    string formattedLine = string.Format(line, G2W, ValSum, isDuplicate, isGreaterThanAverage);
                    SW.WriteLine(formattedLine);
                }

                SW.Close();

            }
          
        }
    }
}
