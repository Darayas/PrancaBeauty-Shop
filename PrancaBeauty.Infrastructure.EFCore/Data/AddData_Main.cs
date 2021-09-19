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
                new AddData_Roles().Run();
                //new AddData_AccessLevel().Run();
                //new AddData_Countris().Run();
                //new AddData_languages().Run();
                //new AddData_CountrisTranslates().Run();
                //new AddData_Province().Run();
                //new AddData_Cities().Run();
                //new AddData_Users().Run();
                //new AddData_Settings().Run();
                new AddData_Template().Run();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
