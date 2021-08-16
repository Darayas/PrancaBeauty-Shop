﻿using System;
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
        public const string CanChangeUsersStatus = "CanChangeUsersStatus";
        public const string CanChangeUsersAccessLevel = "CanChangeUsersAccessLevel";
        #endregion

        #region Categories
        public const string CanManageCategories = "CanManageCategories";
        public const string CanViewListCategories = "CanViewListCategories";
        public const string CanAddCategory = "CanAddCategory";
        public const string CanEditCategory = "CanEditCategory";
        public const string CanRemoveCategory = "CanRemoveCategory";
        #endregion

        #region Products
        public const string CanManageProducts = "CanManageProducts";
        public const string CanViewListProducts = "CanViewListProducts";
        public const string CanViewListAllUserProducts = "CanViewListAllUserProducts";
        #endregion
    }
}
