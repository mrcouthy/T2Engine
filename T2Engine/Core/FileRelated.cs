using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Engine.Helpers;

namespace T2Engine.Core
{
    public static class FileRelated
    {
        public static string GetFileFormatedFileName(string inputFile, string formatDirectory)
        {
            string formateName = new DirectoryInfo(formatDirectory).Name;
            string dataFileName = Path.GetFileNameWithoutExtension(inputFile);
            string fileNameFormat = GetFileNameFormat(formatDirectory);
            if (fileNameFormat.Length==0)
            {
                return dataFileName + formateName + ".txt";
            }
            else
            {
                return fileNameFormat.ReplaceCaseInsensitive("{datafileName}", dataFileName).ReplaceCaseInsensitive("{formatName}", formateName);    
            }
            
        }

        public static string GetFileNameFormat(string formatDirectory)
        {
            var formats = Directory.GetFiles(formatDirectory);
            var main = (from f in formats
                        where Path.GetFileNameWithoutExtension(f).StartsWith("Main")
                        select f).FirstOrDefault();
            string mainfile = Path.GetFileNameWithoutExtension(main);
            if (mainfile.IndexOf("{") > 0)
            {
                return mainfile.Substring(mainfile.IndexOf("{"));
            }
            else
            {
                return string.Empty;
            }
        }



    }
}
