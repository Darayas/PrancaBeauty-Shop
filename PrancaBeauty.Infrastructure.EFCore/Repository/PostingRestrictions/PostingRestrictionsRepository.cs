using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Contracts;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.PostingRestrictions
{
    public class PostingRestrictionsRepository : BaseRepository<tblPostingRestrictions>, IPostingRestrictionsRepository
    {
        public PostingRestrictionsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
