using Framework.Infrastructure;
using PrancaBeauty.Domin.StettingsAgg.Contracts;
using PrancaBeauty.Domin.StettingsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Settings
{
    public class SettingRepository : BaseRepository<tblSettings>, ISettingRepository
    {
        public SettingRepository(MainContext Context) : base(Context)
        {

        }
    }
}
