using System;
using System.IO;
using System.Net.Mime;
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
            var inputFiles = Directory.GetFiles("Input/");
            var formatDirectories = Directory.GetDirectories("Templates/");

            new Transform().Loop(formatDirectories, inputFiles);
        }


    }
}
