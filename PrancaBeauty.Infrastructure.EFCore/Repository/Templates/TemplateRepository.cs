using Framework.Infrastructure;
using PrancaBeauty.Domin.TemplatesAgg.Contracts;
using PrancaBeauty.Domin.TemplatesAgg.Entitis;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Templates
{
    public class TemplateRepository : BaseRepository<tblTamplates>, ITemplateRepository
    {
        public TemplateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
