using Framework.Application.Services.Email;
using Framework.Application.Services.IpList;
using Framework.Application.Services.Sms;
using Framework.Common.Utilities.Downloader;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Templates;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Domin.FileServer.FileAgg.Contracts;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Contracts;
using PrancaBeauty.Domin.StettingsAgg.Contracts;
using PrancaBeauty.Domin.TemplatesAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Infrastructure.EFCore.Context;
using PrancaBeauty.Infrastructure.EFCore.Repository.AccessLevel;
using PrancaBeauty.Infrastructure.EFCore.Repository.FileServer;
using PrancaBeauty.Infrastructure.EFCore.Repository.Region;
using PrancaBeauty.Infrastructure.EFCore.Repository.Roles;
using PrancaBeauty.Infrastructure.EFCore.Repository.Settings;
using PrancaBeauty.Infrastructure.EFCore.Repository.Templates;
using PrancaBeauty.Infrastructure.EFCore.Repository.Users;
using PrancaBeauty.Infrastructure.Logger.Serilogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.Core.Configuration
{
    public static class Bootstrapper
    {
        public static void Config(this IServiceCollection services)
        {
            services.AddDbContext<MainContext>(opt => opt.UseSqlServer("Server=.;Database=PrancaBeautyDb;Trusted_Connection=True;"));

            services.AddScoped<ILogger, Serilogger>();
            services.AddScoped<IEmailSender, GmailSender>();
            services.AddScoped<ISmsSender, KaveNegarSmsSender>();
            services.AddScoped<IDownloader, Downloader>();
            services.AddScoped<IIPList, IPList>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IAccesslevelRepository, AccessLevelRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IFileServerRepository, FileServerRepository>();
            services.AddScoped<IFileRepository, FileRepository>();

            // Applications
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ISettingApplication, SettingApplication>();
            services.AddScoped<ITemplateApplication, TemplateApplication>();
            services.AddScoped<IAccesslevelApplication, AccesslevelApplication>();
            services.AddScoped<IRoleApplication, RoleApplication>();
            services.AddScoped<ILanguageApplication, LanguageApplication>();
            services.AddScoped<IFileServerApplication, FileServerApplication>();
            services.AddScoped<IFileApplication, FileApplication>();
        }
    }
}
