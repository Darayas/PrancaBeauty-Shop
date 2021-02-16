using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrancaBeauty.Domin.Templates.Entitis;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Mapping.Templates
{
    public class tblTamplatesConfiguration : IEntityTypeConfiguration<tblTamplates>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblTamplates> builder)
        {

        }
    }
}
