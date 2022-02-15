using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.ExMethods
{
    public static class DoubleEx
    {
        public static string ToN3(this double Doubl)
        {
            string str = Doubl.ToString("N3", new CultureInfo("en-US"));
            return str.Trim('0').TrimEnd('.');
        }
    }
}
