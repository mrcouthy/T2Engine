using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace T2Engine.Utilities
{
    public class FileHelper
    {
        public static string ReadAllText(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            string fileContents = string.Empty;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContents = reader.ReadToEnd();
            }
            return fileContents;
        }

        public static string[] ReadAllLines(string path)
        {
            string fileContents = ReadAllText(path);
            fileContents = fileContents.TrimEnd('\r', '\n');
            return fileContents.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
        }

        public static List<List<string>> ReadCSV(string filePath)
        {
            var csvRead = new List<List<string>>();
            using (var csvReader = new TextFieldParser(filePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                while (true)
                {
                    List<string> a = new List<string>();
                    string[] parts = csvReader.ReadFields();

                    if (parts == null)
                    {
                        break;
                    }
                    else
                    {
                        a = parts.ToList();
                        csvRead.Add(a);
                    }
                }
            }
            return csvRead;
        }
    }
}
