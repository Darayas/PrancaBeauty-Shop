using Framework.Infrastructure;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Contracts;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Categories
{
    public class CategoryRepository : BaseRepository<tblCategoris>, ICategoryRepository
    {
        public CategoryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
