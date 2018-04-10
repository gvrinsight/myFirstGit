using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileRead
{
    class Program
    {
        static void Main(string[] args)
        {
            //Check command arguments are there or not
            if (args.Length > 0)
            {                
                //validate file existance in given argument
                if (File.Exists(args[0]))
                {
                    string outputLoc = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + ("SampleXMLReader\\DataScrubbing\\Results\\");
                    if (!Directory.Exists(outputLoc))
                        Directory.CreateDirectory(outputLoc);

                    string resultFile = Path.Combine(outputLoc, "Results_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");

                    if (!File.Exists(resultFile))
                    {
                        using (FileStream filestrm = File.Create(resultFile))
                        {
                            WriteToFile(filestrm, args[0]);                            
                        }

                        Console.WriteLine("File created from XML tag data");
                    }
                    else
                    {                        
                        using (FileStream filestrm = File.Open(resultFile, FileMode.Append, FileAccess.Write, FileShare.Write))
                        {
                            WriteToFile(filestrm, args[0]);                            
                        }
                    }                    
                }
                else
                    Console.WriteLine("Invalid Arguments. Please pass a valid file name with path");
            }
            
            //You can avoid this line to exit after processing...just waits for user to enter press button and exits program processing....
            Console.ReadLine();
        }

        private static void WriteToFile(FileStream filestrm, string filePath)
        {
            using (StreamWriter strmWriter = new StreamWriter(filestrm))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string str = string.Empty;

                    StringBuilder sb = new StringBuilder();                                        
                   
                    //Data read and exclude 'Hyderabad' city data...
                    while ((str = sr.ReadLine()) != null)
                    {                        
                        //if (str.ToUpper().Contains("NXT") && str.ToUpper().Contains("VVN"))
                        if (str.Contains("CrashReportNum"))
                            sb.AppendLine(str);
                        str = string.Empty;
                    }

                    //Write StringBuilder to a file...
                    strmWriter.Write(sb.ToString());
                }
            }
        }
    }
}