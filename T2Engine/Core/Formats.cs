using System.Collections.Generic;
using T2Engine.Helpers;
namespace T2Engine.Core
{
    public static class Formats
    {
        static readonly List<string> markers = new List<string> { "firstRow", "otherRow", "lastRow" };
        private static string GetRowFormats(string rowFormats, string valueOf)
        {
            return rowFormats.GetStringFor(markers, valueOf);
        }

      

        public static string GetFirstRowFormats(string rowFormats)
        {
            string str = GetRowFormats(rowFormats, "firstRow");
            if (str.Length == 0)
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
