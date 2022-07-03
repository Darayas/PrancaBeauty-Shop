using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Main
    {
        public void Run()
        {
            try
            {
                //new AddData_FileServers().Run();
                //new AddData_FileType().Run();
                //new AddData_FilePath().Run();
                //new AddData_Roles().Run();
                //new AddData_AccessLevel().Run();
                //new AddData_Countris().Run();
                //new AddData_languages().Run();
                //new AddData_Currencies().Run();
                //new AddData_CountrisTranslates().Run();
                //new AddData_Province().Run();
                //new AddData_Cities().Run();
                //new AddData_Users().Run();
                //new AddData_Sellers().Run();
                //new AddData_Settings().Run();
                //new AddData_Template().Run();
                //new AddData_ProductTopics().Run();
                //new AddData_ProductProperties().Run();
                //new AddData_ProductVariants().Run();
                //new AddData_Guarantee().Run();
                //new AddData_Category().Run();
                //new AddData_ProductReviewAttributes().Run();
                //new AddData_TaxGroups().Run();
                //new AddData_BankAccounts().Run();
                //new AddData_Wallets().Run();
                //new AddData_ProductGroups().Run();
                //new AddData_ProductGroupPercents().Run();
                //new AddData_ShippingMethods().Run();
                //new AddData_ShippingMethodRestricts().Run();
                new AddData_PaymentGates().Run();
                new AddData_PaymentGateRestricts().Run();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
