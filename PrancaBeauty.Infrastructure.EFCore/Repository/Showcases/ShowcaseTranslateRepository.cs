﻿using Framework.Infrastructure;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Showcases
{
    public class ShowcaseTranslateRepository : BaseRepository<tblShowcasesTranslates>, IShowcaseTranslateRepository
    {
        public ShowcaseTranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
