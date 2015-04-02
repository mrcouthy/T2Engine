using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2Engine.Helpers
{
    public static class Helpers
    {
        public static string ReplaceCaseInsensitive(this string str, string old, string newvalue)
        {
            return ReplaceString(str, old, newvalue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string ReplaceString(string str, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder();

            int previousIndex = 0;
            int index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }

        private static string RemoveFirstLine(this string str)
        {
            const int noOflines = 1;
            string[] lines = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1).ToArray();
            string output = string.Join(Environment.NewLine, lines);
            return output.Trim();
        }

        public static string GetStringFor(this string str, List<string> markers, string valueOf)
        {
            foreach (var marker in markers)
            {
                if (String.Equals(valueOf, marker, StringComparison.CurrentCultureIgnoreCase))
                {
                    var fromPosition = str.IndexOf( valueOf,StringComparison.InvariantCultureIgnoreCase);
                    if (fromPosition<0)
                    {
                        return string.Empty;
                    }
                    var toPosition = str.GetPositionJustGreaterThan(markers, fromPosition);
                    string strr= str.Substring(fromPosition, toPosition - fromPosition);
                    strr = strr.RemoveFirstLine();
                    return strr;
                }
            }
            return string.Empty;
        }

        public static int GetPositionJustGreaterThan(this string str, List<string> Markers, int position)
        {
            var positions = Markers.Select(marker => str.IndexOf(marker, StringComparison.InvariantCultureIgnoreCase)).ToList();
            positions.Add(0);
            positions.Add(str.Length);
            positions.Sort();
            var pos = position;
            foreach (var p in positions)
            {
                if (p > position)
                    return p;
            }
            return str.Length;
        }
    }
}
