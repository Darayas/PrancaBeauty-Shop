using Framework.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using PrancaBeauty.Infrastructure.EFCore.Contracts;
using PrancaBeauty.Infrastructure.EFCore.Mapping.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Infrastructure.EFCore.Context
{
    public class MainContext : IdentityDbContext<tblUsers, tblRoles, Guid>
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public MainContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var EntitiesAssembly = typeof(IEntity).Assembly;
            builder.RegisterAllEntities<IEntity>(EntitiesAssembly);

            var EntitiesConfAssembly = typeof(IEntityConf).Assembly;
            builder.RegisterEntityTypeConfigurtion(EntitiesConfAssembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PrancaBeautyDb;Trusted_Connection=True;");
        }
    }
}
