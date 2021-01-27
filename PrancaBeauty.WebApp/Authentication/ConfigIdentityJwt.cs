using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public static class ConfigIdentityJwt
    {
        private static string _SecretKey;
        private static byte[] _SecrectCode;

        public static void AddJwtAuthentication(this IServiceCollection services, string SecretCode, string SecretKey, string Audience, string Issuer)
        {
            _SecretKey = SecretKey;
            _SecrectCode = Encoding.ASCII.GetBytes(SecretCode);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {

            });
        }
    }
}
