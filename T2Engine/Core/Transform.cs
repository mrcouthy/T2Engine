﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using T2Engine.Helpers;
using T2Engine.Utilities;

namespace T2Engine.Core
{
    public class Transform
    {
        public void Loop(string[] formatDirectories, string[] inputFiles, bool isAppend)
        {
            foreach (var inputFile in inputFiles)
            {
                foreach (var formatDirectory in formatDirectories)
                {
                    string outputFileName = FileRelated.GetFileFormatedFileName(inputFile, formatDirectory);
                    string output = DoAFormat(formatDirectory, inputFile);
                    if (isAppend)
                    {
                        File.AppendAllText("Output/" + outputFileName, output);
                    }
                    else
                    {
                        File.WriteAllText("Output/" + outputFileName, output);
                    }
                }
            }
        }

        private string DoAFormat(string templateFolder, string inputFile)
        {
            string formateName = new DirectoryInfo(templateFolder).Name;
            var replaceStrings = new string[] { " ", ".", @"/", "-" };
            var formats = Directory.GetFiles(templateFolder);
            var main = (from f in formats
                        where Path.GetFileNameWithoutExtension(f).StartsWith("Main")
                        select f).FirstOrDefault();
            var rowFormatFiles = (from f in formats
                                  where Path.GetFileNameWithoutExtension(f).StartsWith("Row")
                                  orderby f
                                  select f).ToList();
            string inputFileName = Path.GetFileNameWithoutExtension(inputFile);
            string template = FileHelper.ReadAllText(main);
            string finalOutput = template;
            //Replace all rows
            for (int rowcount = 0; rowcount < rowFormatFiles.Count(); rowcount++)
            {
                string rowFormat = FileHelper.ReadAllText(rowFormatFiles[rowcount]);
                int totalRows = 0;
                string rowText = GetRowStrings(rowFormat, inputFile, out totalRows);
                string rowFromatString = "{" + string.Format("Row{0}", rowcount + 1) + "}";

                finalOutput = finalOutput.ReplaceCaseInsensitive(rowFromatString, rowText);
                finalOutput = finalOutput.ReplaceCaseInsensitive("{totalrows}", totalRows.ToString());
            }
            //Replaces for main file
            finalOutput = finalOutput.ReplaceCaseInsensitive("{fileName}", inputFileName);
            finalOutput = finalOutput.ReplaceCaseInsensitive("{formatname}", formateName);

            return finalOutput;
        }

        /// <summary>
        ///    //{count} =rowcount, {0} = first col , {1} = second col etc
        /// </summary>
        /// <param name="rowFormats">Read from files such as Row1.fmt</param>
        /// <param name="dataFilePath"></param>
        /// <returns></returns>
        public string GetRowStrings(string rowFormats, string dataFilePath, out int totalRows)
        {
            var rowStrings = new List<string>();
            var dataRows = FileHelper.ReadCSV(dataFilePath);//dont read after blank or indication of end of row?
            totalRows = dataRows.Count;
            var firstRowFormat = Formats.GetFirstRowFormats(rowFormats);
            var lastRowFormat = Formats.GetLastRowFormats(rowFormats);
            var otherRowFormat = Formats.GetOtherRowFormats(rowFormats);

            rowStrings.Add(TransformARow(firstRowFormat, dataRows, 0));
            for (int count = 1; count < totalRows - 1; count++)
            {
                rowStrings.Add(TransformARow(otherRowFormat, dataRows, count));
            }
            rowStrings.Add(TransformARow(lastRowFormat, dataRows, totalRows - 1));
            return string.Join(System.Environment.NewLine, rowStrings);
        }

        private static string TransformARow(string rowFormat, List<List<string>> dataRows, int count)
        {
            //get columns data
            var columns = dataRows[count];
            string rowString = rowFormat.ReplaceCaseInsensitive("{count}", (count + 1).ToString());
            for (int i = 0; i < columns.Count; i++)
            {
                rowString = rowString.ReplaceCaseInsensitive("{" + i + "}", columns[i].Trim());
            }
            return rowString;
        }
    }
}
