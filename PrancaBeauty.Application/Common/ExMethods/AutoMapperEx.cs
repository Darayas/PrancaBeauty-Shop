using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Application.Contracts.Mapping;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace PrancaBeauty.Application.Common.ExMethods
{
    public static class AutoMapperEx
    {
        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            var ProfileAssem = typeof(AutoMapping).Assembly.GetTypes().Where(a => a !=typeof(Profile) && typeof(Profile).IsAssignableFrom(a));

            services.AddAutoMapper(opt =>
            {
                opt.AddProfiles(ProfileAssem.Select(profile => (Profile)Activator.CreateInstance(profile)));
            });
        }
    }
}
