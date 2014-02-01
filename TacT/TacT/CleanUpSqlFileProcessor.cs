using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BasicsOfCsharp_TeachingMyself
{
    public class CleanUpSqlFileProcessor
    {
        private readonly string _inputFileLocation;
        private const string dateTimeFormat = "yyyy-MM-dd hh:mm";
        public CleanUpSqlFileProcessor(string inputFileLocation)
        {
            ClearLog();
            _inputFileLocation = inputFileLocation;
        }

        private void ClearLog()
        {
            Console.Clear();
        }

        private string ProcessLine(string line, StreamWriter outputFile, TextReader tr, ref string editedFile)
        {
            if (line != null)
            {
                line = RemoveLeadingWhitespace(line);
                //line = RemoveTrailingWhitespace(line);

                if (line.StartsWith("\r\n") || line.Equals("\n", StringComparison.OrdinalIgnoreCase))
                {
                    editedFile = IgnoreLine(editedFile, line, outputFile);
                }
                else if (line.Contains("PRINT"))
                {
                    editedFile += line + "\n";
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.StartsWith("--"))
                {
                    editedFile += line + "\n";
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.Contains("INSERT INTO"))
                {
                    editedFile += line;
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.Contains("VALUES"))
                {
                    editedFile += line;
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.Contains("]") || (line.Contains("]") && line.Contains(",")))
                {
                    editedFile += line;
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.Contains(">',"))
                {
                    editedFile += line;
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.Contains(")"))
                {
                    editedFile += line + "\n";
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else if (line.Contains(""))
                {
                    editedFile += line;
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
                else
                {
                    editedFile += line;
                    Console.Write(editedFile);
                    outputFile.Write(editedFile);
                    editedFile = "";
                }
            }
            else
            {
                editedFile += line;
                Console.Write(editedFile);
                outputFile.Write(editedFile);
                editedFile = "";
            }


            line = tr.ReadLine();
            return line;
        }

        private static string IgnoreLine(string EditedFile, string line, StreamWriter OutputFile)
        {
            EditedFile += line + "\n";
            Console.Write(EditedFile);
            OutputFile.Write(EditedFile);
            EditedFile = "";
            return EditedFile;
        }

        private string RemoveLeadingWhitespace(string line)
        {
            var chars = line.ToCharArray();
            var counter = 0;
            foreach (var c in chars)
            {
                counter++;
                if (!char.IsWhiteSpace(c))
                {
                    counter --;
                    return line.Substring(counter, line.Length - counter);
                }
            }
            return line;
        }

        private string RemoveTrailingWhitespace(string line)
        {
            var chars = line.ToCharArray().Reverse();
            var counter = 0;
            foreach (var c in chars)
            {
                counter++;
                if (!char.IsWhiteSpace(c))
                {
                    return line.Substring(counter, line.Length - counter);
                }
            }
            return (string)line.Reverse();
        }

        public void Clean()
        {
            LogInfo("This Will Add a SemiColon to the SQL Statement and save a modified new file");
            var extension = Path.GetExtension(_inputFileLocation);
            var outputFilename = Path.GetFileNameWithoutExtension(_inputFileLocation);
            var path = Path.GetDirectoryName(_inputFileLocation);
            var outputFilePath = string.Format("{0}\\{1}_Output{2}", path, outputFilename, extension);

            string editedFile = "";
            using (var outputFile = File.CreateText(outputFilePath))
            {
                //read the text file
                //create reader & open file
                //Textreader tr = new StreamReader("date.txt");
                try
                {
                    TextReader tr = new StreamReader("CommsTemplates.sql");

                    using (StreamReader reader = File.OpenText("CommsTemplates.sql"))
                    {
                        var line = tr.ReadLine();
                        while (line != null)
                        {
                            line = ProcessLine(line, outputFile, tr, ref editedFile);
                        }
                        reader.Close();
                    }
                    /*
                    using (var outputFile = new System.IO.StreamWriter(@"C:\Users\jason.chalom\Dropbox\New Projects\GitHub\BasicsOfCsharp_TeachingMyself\BasicsOfCsharp_TeachingMyself_New\BasicsOfCsharp_TeachingMyself\bin\Debug\CommsTemplates_Edited.sql"))
                    {
                        outputFile.Write(EditedFile);
                    }
                */
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    Console.WriteLine("An error occured Accessing the file.");
                    Console.WriteLine(fileNotFoundException);
                    Console.ReadLine();
                }
            }
        }

        private void LogInfo(string message)
        {
            Console.WriteLine("[{0}] :{1}{2}", DateTime.Now.ToString(dateTimeFormat), message, Environment.NewLine);
        }

        public void OutputToFile()
        {
            LogInfo("This Will Begin To Write OutputFile With Edits");
            var extension = Path.GetExtension(_inputFileLocation);
            var outputFilename = Path.GetFileNameWithoutExtension(_inputFileLocation);
            var path = Path.GetDirectoryName(_inputFileLocation);
            var outputFilePath = string.Format("{0}\\{1}_Output{2}", path, outputFilename, extension);
        }
    }
}