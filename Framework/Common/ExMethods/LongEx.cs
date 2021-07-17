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
            // Byte
            if (FileSize < 1024)
            {
                return $"{FileSize} Byte";
            }
            else if (FileSize >= 1024 && FileSize < 1048576)
            {
                return $"{FileSize / 1024} KB";
            }
            else if (FileSize >= 1048576 && FileSize < ‭1073741824‬)
            {
                return $"{FileSize / 1024 / 1024}.{(FileSize % 1048576).ToString().Trim('0')} MB";
            }
            else if (FileSize >= 1073741824 && FileSize < ‭‭1099511627776‬‬)
            {
                return $"{(FileSize / 1024 / 1024/1024)}.{(FileSize % 1073741824).ToString().Trim('0')} MB";
            }

            return "~";
        }
    }
}
