using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Categories.CategoriesAgg.Contracts
{
    public interface ICategoryRepository : IRepository<tblCategoris>
    {
    }
}
