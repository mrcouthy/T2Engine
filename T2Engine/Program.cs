using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using RazorEngine;
using T2Engine.Core;
using RazorEngine.Templating;
namespace T2Engine
{
    class Program
    {

        static void Main(string[] args)
        {

            //string template = "Hello @Model.Name, welcome to RazorEngine!@1+2";
            //var result =Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            var InputFiles = Directory.GetFiles("Input/");
            var FormatDirectories = Directory.GetDirectories("Templates/");
            new Transform().Loop(FormatDirectories, InputFiles);
        }


    }
}
