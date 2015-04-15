using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using CLAP;
using T2Engine.Core;
using T2Engine.Utilities;

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
        
        [Verb(IsDefault =true,Description ="Creates files based on template" )]
        public static void Transform(
               [Parameter(Default = false, Description = "Append or not ?")]
            bool append )
        {
            Console.WriteLine(append);
            var inputFiles = Directory.GetFiles("Input/");
            var formatDirectories = Directory.GetDirectories("Templates/");
            var fd = (from string format in formatDirectories
                      where !Path.GetFileName(format).StartsWith("_")
                      select format).ToArray();
            new Transform().Loop(fd, inputFiles,append);
        }

        [Verb(Description ="Creates files for each row of first column")]
        public static void CreateFiles()
        {
            var inputFiles = Directory.GetFiles("Input/");
            var formatDirectories = Directory.GetDirectories("Templates/");
            foreach (var inputFile in inputFiles)
            {
                var a = FileHelper.ReadAllLines(inputFile);
                //  string outputFileName = FileRelated.GetFileFormatedFileName(inputFile, formatDirectory);
                foreach (var b in a)
                {
                   // string filname = b.Substring(0, b.IndexOf(".")) + ".csv";
                   // string ins = FileHelper.ReadAllText(@"D:\teng\in.csv");
                    string ins = "";
                    string filname = b + ".csv";
                    File.WriteAllText("Output/" + filname, ins);
                }
            }
        }

        //********************** Defaults **********************
        [Error]
        public static void HandleError(ExceptionContext context)
        {
            Console.WriteLine("Use help option eg '*.exe help' for Help. Err :" + context.Exception.Message);
        }


        //[Empty, Help] use this if help to be displayed on empty
        [Help]
        public static void Help(string help)
        {
            // this is an empty handler that prints
            // the automatic help string to the console.
            Console.WriteLine(help);
        }
    }
}
