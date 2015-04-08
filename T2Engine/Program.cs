using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using CLAP;
using T2Engine.Core;

namespace T2Engine
{
    class Program
    {

        static void Main(string[] args)
        {
            //set environement variable path ="exe path folder"
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //string template = "Hello @Model.Name, welcome to RazorEngine!@1+2";
            //var result =Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });
            string workingFolder = Properties.Settings.Default.WorkingFolder;

            if (workingFolder.Length != 0 && !Directory.Exists(workingFolder))
            {
                Console.WriteLine("Working directory {0} not found ! Using Default folder.", workingFolder);
                Console.ReadLine();
                workingFolder = "";
            }
            Directory.SetCurrentDirectory(workingFolder);
            Console.WriteLine(Directory.GetCurrentDirectory());

            Parser.Run<TEng>(args);
            //TEng.Transform();
            //TEng.t3();
        }
    }

    public class TEng
    {
        [Empty]
        public static void Transform()
        {
            var inputFiles = Directory.GetFiles("Input/");
            var formatDirectories = Directory.GetDirectories("Templates/");
            var fd = (from string format in formatDirectories
                      where !Path.GetFileName(format).StartsWith("_")
                      select format).ToArray();
            new Transform().Loop(fd, inputFiles);
        }

        [Verb]
        public static void Append()
        {
            Console.WriteLine("T3 append");
        }

        [Verb]
        public static void CreateFiles()
        {
            var inputFiles = Directory.GetFiles("Input/");
            var formatDirectories = Directory.GetDirectories("Templates/");

            foreach (var inputFile in inputFiles)
            {
                var a = File.ReadAllLines(inputFile);
                //  string outputFileName = FileRelated.GetFileFormatedFileName(inputFile, formatDirectory);
                foreach (var b in a)
                {
                    string filname = b.Substring(0, b.IndexOf(".")) + ".csv";
                    string ins = File.ReadAllText(@"D:\teng\in.csv");
                    File.WriteAllText("Output/" + filname, ins);
                }
            }
        }
    }
}
