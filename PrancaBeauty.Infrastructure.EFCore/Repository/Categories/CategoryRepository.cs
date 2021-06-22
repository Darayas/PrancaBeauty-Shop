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
    public class CategoryRepository : BaseRepository<tblCategoris>, ICategoryRepository
    {
        public CategoryRepository(MainContext Context) : base(Context)
        {

        }
    }
}
