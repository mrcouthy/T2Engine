using System;
using System.IO;
using T2Engine.Core;

namespace T2Engine
{
    class Program
    {

        static void Main(string[] args)
        {
            //string template = "Hello @Model.Name, welcome to RazorEngine!@1+2";
            //var result =Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });
            string workingFolder = Properties.Settings.Default.WorkingFolder;
            
            if (workingFolder.Length != 0 && !Directory.Exists(workingFolder))
            {
                Console.WriteLine("Working directory {0} not found ! Using Default folder.", workingFolder);
                workingFolder = "";
            }
            Directory.SetCurrentDirectory(workingFolder);
            var inputFiles = Directory.GetFiles("Input/");
            var formatDirectories = Directory.GetDirectories("Templates/");

            new Transform().Loop(formatDirectories, inputFiles);
        }


    }
}
