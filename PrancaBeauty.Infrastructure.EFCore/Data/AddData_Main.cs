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
                new AddData_AccessLevel().Run();
                new AddData_Users().Run();
                new AddData_languages().Run();
                new AddData_Settings().Run();
                new AddData_Template().Run();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
