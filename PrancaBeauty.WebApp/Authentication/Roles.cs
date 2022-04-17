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
        public const string CanChangeUsersStatus = "CanChangeUsersStatus";
        public const string CanChangeUsersAccessLevel = "CanChangeUsersAccessLevel";
        public const string CanChangeProfileImage = "CanChangeProfileImage";
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
        public const string CanViewListAllAuthorUserProducts = "CanViewListAllAuthorUserProducts";
        public const string CanViewListAllSellerUserProducts = "CanViewListAllSellerUserProducts";
        public const string CanAddProduct = "CanAddProduct";
        public const string CanEditProduct = "CanEditProduct";
        public const string CanEditProductForAllUser = "CanEditProductForAllUser";
        public const string CanRemoveAllUserProduct = "CanRemoveAllUserProduct";
        public const string CanRemoveProduct = "CanRemoveProduct";
        public const string CanMoveToRecycleBinProduct = "CanMoveToRecycleBinProduct";
        public const string CanMoveToRecycleBinAllUserProduct = "CanMoveToRecycleBinAllUserProduct";
        public const string CanChangeStatusProduct = "CanChangeStatusProduct";

        public const string CanChangeStatusProductReviews = "CanChangeStatusProductReviews";
        public const string CanRemoveProductReviews = "CanRemoveProductReviews";
        public const string CanChangeStatusProductReviewsForAllUser = "CanChangeStatusProductReviewsForAllUser";
        public const string CanRemoveProductReviewsForAllUser = "CanRemoveProductReviewsForAllUser";

        public const string CanChangeStatusProductAsks = "CanChangeStatusProductAsks";
        public const string CanRemoveProductAsks = "CanRemoveProductAsks";
        public const string CanChangeStatusProductAsksForAllUser = "CanChangeStatusProductAsksForAllUser";
        public const string CanRemoveProductAsksForAllUser = "CanRemoveProductReviewsForAllUser";
        #endregion

        #region ProductSeller
        public const string CanViewListProductSellerList = "CanViewListProductSellerList";
        public const string CanViewListProductSellerListAllUser = "CanViewListProductSellerListAllUser";
        public const string CanAddProductSeller = "CanAddProductSeller";
        public const string CanAddProductSellerAllUser = "CanAddProductSellerAllUser";
        public const string CanEditProductSeller = "CanEditProductSeller";
        public const string CanDeleteProductSeller = "CanDeleteProductSeller";
        public const string CanChangeStatusProductSeller = "CanChangeStatusProductSeller";
        public const string CanViewDetailsProductSeller = "CanViewDetailsProductSeller";
        #endregion

        #region Files
        public const string CanManageAllUserFiles = "CanManageAllUserFiles";
        #endregion

        #region Slider
        public const string CanManageSlider = "CanManageSlider";
        public const string CanViewListSlider = "CanViewListSlider";
        public const string CanAddSlide = "CanAddSlide";
        public const string CanEditSlide = "CanEditSlide";
        public const string CanRemoveSlide = "CanRemoveSlide";
        #endregion

        #region Showcase
        public const string CanManageShowcase = "CanManageShowcase";
        public const string CanViewListShowcase = "CanViewListShowcase";
        public const string CanAddShowcase = "CanAddShowcase";
        public const string CanEditShowcase = "CanEditShowcase";
        public const string CanRemoveShowcase = "CanRemoveShowcase";
        #endregion

        #region Showcase
        public const string CanViewListShowcaseTab = "CanViewListShowcaseTab";
        public const string CanAddShowcaseTab = "CanAddShowcaseTab";
        public const string CanEditShowcaseTab = "CanEditShowcaseTab";
        public const string CanRemoveShowcaseTab = "CanRemoveShowcaseTab";
        #endregion
    }
}
