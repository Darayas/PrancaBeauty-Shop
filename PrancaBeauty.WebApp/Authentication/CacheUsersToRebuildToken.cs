using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public static class CacheUsersToRebuildToken
    {
        private static List<string> UserIds { get; set; }

        public static void AddRange(List<string> UsersId)
        {
            if (UserIds == null)
                UserIds = new List<string>();

            UserIds.AddRange(UsersId);
        }

        public static bool Any(string UserId)
        {
            if (UserIds == null)
                return false;

            return UserIds.Any(a => a == UserId);
        }

        public static void Remove(string UserId)
        {
            if (UserIds != null)
            {
                UserIds.Remove(UserId);
            }
        }
    }
}
