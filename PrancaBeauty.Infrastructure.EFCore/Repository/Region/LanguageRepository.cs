using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.LanguagesAgg.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Region
{
    public class LanguageRepository : BaseRepository<tblLanguages>, ILanguageRepository
    {
        public LanguageRepository(MainContext Context) : base(Context)
        {

        }
    }
}
