using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using T2Engine.Core;

namespace T2Engine
{
    class Program
    {

        static void Main(string[] args)
        {
            var InputFiles = Directory.GetFiles("Input/");
            var FormatDirectories = Directory.GetDirectories("Templates/");
            Dictionary<string, string> Replaces = new Dictionary<string, string>();
            Replaces.Add("", "");

            Loop(FormatDirectories, InputFiles);
        }

        private static void Loop(string[] FormatDirectories, string[] InputFiles )
        {
            foreach (var inputFile in InputFiles)
            {
                foreach (var formatDirectory in FormatDirectories)
                {
                    // string fileName = Path.GetFileNameWithoutExtension(inputFile);
                    DoAFormat(formatDirectory,  inputFile);

                    //foreach (var formatFile in rows)
                    //{
                    //    var format = Formats.GetFormats( formatFile);
                    //}
                }
            }

        }

        private static void DoAFormat(string formatDirectory,   string inputFile)
        {
            var formats = Directory.GetFiles(formatDirectory);
            var main = (from f in formats
                        where Path.GetFileNameWithoutExtension(f).StartsWith("Main")
                        select f).FirstOrDefault();
            var rows = (from f in formats
                        where Path.GetFileNameWithoutExtension(f).StartsWith("Row")
                        orderby f
                        select f).ToList();

            string sql = File.ReadAllText(main);
            string s = sql.Replace("{fileName}", "abc");
            //s = s.Replace("{fileName}", "abc");
            int rowcount = 1;
            foreach (var row in rows)
            {
             
                string rowFormat = File.ReadAllText(row);
                string rowText = GetRowFormated(rowFormat, row);
                s = s.Replace(string .Format( "{Row{0}}" ,rowcount)  ,rowText);
                rowcount++;
            }

        }

        public static string GetRowFormated(string rowFormat,string file)
        {
            var fields = new List<string>();
            var splited = System.IO.File.ReadAllLines(file);
            int count = 0;
            foreach (var s in splited)
            {
                count++;
                var emtity = s.Split(new[] { "," }, StringSplitOptions.None);
                var field = emtity[0];
                var lenght = emtity[1];
                string clean = rowFormat.Replace(" ", "_").Replace(".", "_").Replace(@"/", "_").Replace("-", "_");
                fields.Add(clean);
            }
            return string.Join(System.Environment.NewLine, fields);
        }
    }
}
