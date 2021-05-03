using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public class Roles
    {
        public const string AdminPage = "AdminPage";

        #region AccessLevels
        public const string CanManageAccessLevel = "CanManageAccessLevel";
        public const string CanViewListAccessLevel = "CanViewListAccessLevel";
        public const string CanAddAccessLevel = "CanAddAccessLevel";
        public const string CanEditAccessLevel = "CanEditAccessLevel";
        public const string CanRemoveAccessLevel = "CanRemoveAccessLevel";
        #endregion

        #region Users
        public const string CanManageUsers = "CanManageUsers";
        public const string CanViewListUsers = "CanViewListUsers";
        public const string CanAddUsers = "CanAddUsers";
        public const string CanEditUsers = "CanEditUsers";
        public const string CanRemoveUsers = "CanRemoveUsers";
        #endregion
    }
}
