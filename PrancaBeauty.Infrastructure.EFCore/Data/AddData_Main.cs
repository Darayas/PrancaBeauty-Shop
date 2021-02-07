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

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
