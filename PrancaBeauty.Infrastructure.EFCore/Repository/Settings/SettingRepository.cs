using Framework.Infrastructure;
using PrancaBeauty.Domin.Settings.SettingsAgg.Contracts;
using PrancaBeauty.Domin.Settings.SettingsAgg.Entities;
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
