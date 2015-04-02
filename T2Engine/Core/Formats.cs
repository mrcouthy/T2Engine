using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2Engine.Helpers;
namespace T2Engine.Core
{
    public static class Formats
    {

        private static string GetRowFormats(string rowFormats, string valueOf)
        {
            var markers = new List<string> { "fristRow", "otherRow", "lastRow" };
            return rowFormats.GetStringFor(markers, valueOf);
        }

        public static string GetFirstRowFormats(string rowFormats )
        {
            string str = GetRowFormats(rowFormats, "fristRow");
            if (str.Length ==0)
            {
                str = GetOtherRowFormats(rowFormats);
            }
            return str;
        }
        public static string GetLastRowFormats(string rowFormats)
        {
            string str = GetRowFormats(rowFormats, "lastRow");
            if (str.Length == 0)
            {
                str = GetOtherRowFormats(rowFormats);
            }
            return str;
        }
        public static string GetOtherRowFormats(string rowFormats)
        {
            string str = GetRowFormats(rowFormats, "otherRow");
            if (str.Length == 0)
            {
                str = rowFormats;
            }
            return str;
        }
    }
}
