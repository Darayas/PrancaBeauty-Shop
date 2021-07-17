using Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.ExMethods
{
    public static class LongEx
    {
        public static string GetFileSizeName(this long FileSize)
        {
            string[] Names = { "B", "KB", "MB", "GB", "TB", "ExB" };
            double Number = FileSize;
            int index = 0;

            while (Number > 1024)
            {
                Number = Number / 1024;
                index++;
            }

            return Number.ToString("0.#") + " " + Names[index];
        }
    }
}
