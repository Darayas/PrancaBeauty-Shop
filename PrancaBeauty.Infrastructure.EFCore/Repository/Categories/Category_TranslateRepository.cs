using Framework.Infrastructure;
using PrancaBeauty.Domin.Categories.Contracts;
using PrancaBeauty.Domin.Categories.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Categories
{
    public class Category_TranslateRepository : BaseRepository<tblCategory_Translates>, ICategory_TranslateRepository
    {
        public Category_TranslateRepository(MainContext context) : base(context)
        {

        }
    }
}
