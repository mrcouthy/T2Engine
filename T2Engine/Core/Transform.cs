using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Engine.Helpers;
namespace T2Engine.Core
{
    public class Transform
    {
        public void Loop(string[] formatDirectories, string[] inputFiles)
        {
            foreach (var inputFile in inputFiles)
            {
                foreach (var formatDirectory in formatDirectories)
                {
                    string outputFileName = GetFileFormatedFileName(inputFile, formatDirectory);
                    string output = DoAFormat(formatDirectory, inputFile);
                    File.WriteAllText("Output/" + outputFileName, output);
                }
            }
        }


        public string GetFileNameFormat(string formatDirectory)
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
                return mainfile;
            }
        }


        public string GetFileFormatedFileName(string inputFile, string formatDirectory)
        {
            string formateName = new DirectoryInfo(formatDirectory).Name;
            string dataFileName = Path.GetFileNameWithoutExtension(inputFile);
            string fileNameFormat = GetFileNameFormat(formatDirectory);
            return fileNameFormat.ReplaceCaseInsensitive("{datafileName}", dataFileName).ReplaceCaseInsensitive("{formatName}", formateName);
        }

        private string DoAFormat(string formatDirectory, string inputFile)
        {
            string formateName = new DirectoryInfo(formatDirectory).Name;
            var replaceStrings = new string[] { " ", ".", @"/", "-" };
            var formats = Directory.GetFiles(formatDirectory);
            var main = (from f in formats
                        where Path.GetFileNameWithoutExtension(f).StartsWith("Main")
                        select f).FirstOrDefault();
            var rowFormatFiles = (from f in formats
                                  where Path.GetFileNameWithoutExtension(f).StartsWith("Row")
                                  orderby f
                                  select f).ToList();
            string inputFileName = Path.GetFileNameWithoutExtension(inputFile);
            string template = File.ReadAllText(main);
            string finalOutput = template;
            for (int rowcount = 1; rowcount < rowFormatFiles.Count(); rowcount++)
            {
                string rowFormat = File.ReadAllText(rowFormatFiles[rowcount]);
                string rowFromatString = "{" + string.Format("Row{0}", rowcount) + "}";
                string rowText = GetRowStrings(rowFormat, inputFile);
                finalOutput = finalOutput.ReplaceCaseInsensitive(rowFromatString, rowText);
                rowcount++;
            }
            finalOutput = template.ReplaceCaseInsensitive("{datafileName}", inputFileName);
            finalOutput = finalOutput.ReplaceCaseInsensitive("{formatname}", formateName);
            return finalOutput;
        }

        /// <summary>
        ///    //{count} =rowcount, {col1} = first col , {col2} = second col etc
        /// </summary>
        /// <param name="rowFormat"></param>
        /// <param name="dataFilePath"></param>
        /// <returns></returns>
        public string GetRowStrings(string rowFormat, string dataFilePath)
        {
            var rowStrings = new List<string>();
            var dataRows = File.ReadAllLines(dataFilePath);
            for (int count = 0; count < dataRows.Length; count++)
            {
                //get columns data
                var columns = dataRows[count].Split(new[] { "," }, StringSplitOptions.None);
                string rowString = rowFormat.ReplaceCaseInsensitive("{count}", count.ToString());
                for (int i = 0; i < columns.Length; i++)
                {
                    rowString = rowString.ReplaceCaseInsensitive("{" + i + "}", columns[i]);
                }
                rowStrings.Add(rowString);
            }
            return string.Join(System.Environment.NewLine, rowStrings);
        }
    }
}
