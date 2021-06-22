using PrancaBeauty.Domin.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Categories
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryApplication(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }
    }
}
