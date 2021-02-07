using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_AccessLevel
    {
        BaseRepository<tblAccessLevels> _repAccessLevel;
        public AddData_AccessLevel()
        {
            _repAccessLevel = new BaseRepository<tblAccessLevels>(new MainContext());
        }

        public void Run()
        {
            if (!_repAccessLevel.Get.Any(a => a.Name == "GeneralManager"))
            {
                _repAccessLevel.AddAsync(new tblAccessLevels()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "GeneralManager"
                }, default, false).Wait();
            }



            _repAccessLevel.SaveChangeAsync().Wait();
        }
    }
}
