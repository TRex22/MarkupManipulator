//This is the basic program I made to learn C#
//it uses strings and other variables
//does some basic calculations
//gets time
//opens the textfile Assembly.txt which I found http://www.textfiles.com/computers/asm.txt
//it is the 8086 Family Architecture
//the program can also write a textfile and read it back
//The last thing the program will read a textfile and then convert it line by line to XML
//no real formatting yet.
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicsOfCsharp_TeachingMyself
{
    internal class Program
    {
        /* todo rk move this to a settings */
        private const string SqlFileLocation =
            @"C:\Users\jason.chalom\Desktop\Programming Mockups\SQLFile.sql";
        private static void Main(string[] args)
        {
            int choice = 1090;
            
            while (choice != '0')
            {
                Console.Clear();
                //menu
                Console.WriteLine("1. Run File Line Editor <TacT Beta>");
                Console.WriteLine("0. Exit Application");
                Console.WriteLine();
                choice = Console.Read();

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Enter File Location Which You Want To Edit");
                        var fileprocessor = new CleanUpSqlFileProcessor(SqlFileLocation);
                        fileprocessor.Clean();
                        //fileprocessor.OutputToFile();
                        Console.Read();
                        break;
                    case '0':
                        Console.WriteLine();
                        Console.WriteLine("The Program will now exit.");
                        //pause and then exit
                        Console.Read();
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Incorrect choice.");
                        break;

                }
            }

        }
    }

}