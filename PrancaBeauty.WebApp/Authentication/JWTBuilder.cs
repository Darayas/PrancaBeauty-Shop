using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Microsoft.IdentityModel.Tokens;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Roles;
using PrancaBeauty.Application.Contracts.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public class JWTBuilder : IJWTBuilder
    {
        private readonly IUserApplication _UserApplication;
        private readonly IRoleApplication _RoleApplication;
        public JWTBuilder(IUserApplication userApplication, IRoleApplication roleApplication)
        {
            _UserApplication = userApplication;
            _RoleApplication = roleApplication;
        }

        public async Task<string> CreateTokenAync(string UserId)
        {

            var _UserDetails = await _UserApplication.GetAllUserDetailsAsync(new InpGetAllUserDetails { UserId = UserId });
            if (_UserDetails == null)
                throw new Exception();

            var _UserRoles = await _RoleApplication.GetRolesByUserAsync(new InpGetRolesByUser { UserId = UserId });
            if (_UserDetails == null)
                throw new Exception();

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,_UserDetails.Id.ToString()),
                new Claim(ClaimTypes.Name, _UserDetails.UserName),
                new Claim(ClaimTypes.Email, _UserDetails.Email),
                new Claim(ClaimTypes.MobilePhone, _UserDetails.PhoneNumber??""),
                new Claim(ClaimTypes.GivenName, _UserDetails.FirstName),
                new Claim(ClaimTypes.Surname, _UserDetails.LastName),
                new Claim("AccessLevel", _UserDetails.AccessLevelTitle),
                new Claim("Date", _UserDetails.Date.ToString("yyyy/MM/dd HH:mm:ss")),
            };

            Claims.AddRange(_UserRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var _Key = Encoding.ASCII.GetBytes(AuthConst.SecretCode);
            var TokenDescreptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_Key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = AuthConst.Issuer,
                Audience = AuthConst.Audience,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddHours(48)
            };

            var _SecurityToken = new JwtSecurityTokenHandler().CreateToken(TokenDescreptor);
            string _GeneratedToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(_SecurityToken);

            return _GeneratedToken.AesEncrypt(AuthConst.SecretKey);
        }
    }
}
