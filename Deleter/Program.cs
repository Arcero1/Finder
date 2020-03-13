using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deleter
{
    class Program
    {
        const string name = "BSSelfDeletingLogger";
        static string fileName;
        static List<string> loggerObjectNames;
        static List<char> possibleTrailingChars = new List<char>();

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            foreach (string f in args)
            {
                fileName = f;
                PurgeFile(fileName);
            }
        }

        static bool PurgeFile(string fileName)
        {
            int lineCounter = 0;

            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines(fileName).Where(line =>
            {
                bool splitLine = false;
                bool removeLine = false;

                do
                {
                    line = line.Trim();
                    int lineBreakIndex = FindLineBreak(line);
                    splitLine = lineBreakIndex != line.Length - 1;


                    string line_temp = line.Substring(0, lineBreakIndex);
                    line = line.Substring(lineBreakIndex + 1);

                    removeLine = CheckLineAgainstRemovalCondition(line_temp);
                    if (removeLine)
                    {
                        Console.WriteLine("Removing line:\t" + lineCounter);
                    }
                } while (splitLine);

                lineCounter++;
                return !removeLine;
            });

            try
            {
                File.WriteAllLines(tempFile, linesToKeep);

                File.Delete(fileName);
                File.Move(tempFile, fileName);
            }
            catch (Exception)
            {
                Console.WriteLine("Purging " + fileName + " failed.");
                return false;
            }
            return true;
        }

        static bool CheckForDeclaration(string line)
        {
            return line.Contains(name + " ") || line.Contains(name + "*") || line.Contains(name + "&");
        }

        static void AddObject(string line)
        {
            int index = line.IndexOf(name) + name.Length;
        }

        static bool CheckLineAgainstRemovalCondition(string line)
        {
            return line.Contains("BsSelfDeletingLogger")
                || line.Contains("bsSelfDeletingLogger");
        }

        static int FindLineBreak(string line)
        {
            return line.Contains(";") ? line.IndexOf(";") : -1;
        }
    }
}
