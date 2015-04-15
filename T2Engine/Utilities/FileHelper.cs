using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
