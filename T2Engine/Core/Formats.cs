using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2Engine.Core
{
   public static class Formats
    {
       public static string[] GetFormats( string formatFile)
        {
            var formats = new List<string>();
            var splitedFormat = System.IO.File.ReadAllLines(formatFile);
            foreach (var f in splitedFormat)
            {
                formats.Add(f.Trim());
            }
            return formats.ToArray();
        }
    }
}
