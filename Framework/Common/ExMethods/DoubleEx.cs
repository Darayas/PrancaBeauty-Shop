using System.Globalization;

namespace Framework.Common.ExMethods
{
    public static class DoubleEx
    {
        public static string ToN3(this double Doubl)
        {
            string str = Doubl.ToString("N3", new CultureInfo("en-US"));
            str = str.Trim('0').TrimEnd('.'); ;

            if (str == "")
            {
                return "0";
            }
            else
            {
                return str;
            }
        }
    }
}
