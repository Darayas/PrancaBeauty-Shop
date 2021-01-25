using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
