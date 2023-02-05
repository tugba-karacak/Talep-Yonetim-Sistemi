using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.Tools
{
    public class AccessTokenGenerator
    {
        public IUow uow;

        public ApplicationUser _applicationUser { get; set; }

        public List<string> roles;

        /// <summary>
        /// Class'ın oluşturulması.
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_config"></param>
        /// <param name="_applicationUser"></param>
        /// <returns></returns>
        public AccessTokenGenerator(IUow uow,
                                    ApplicationUser applicationUser, List<string> roles)
        {
            this.uow = uow;
            _applicationUser = applicationUser;
            this.roles = roles;
        }

        /// <summary>
        /// Kullanıcı üzerinde tanımlı tokenı döner;Token yoksa oluşturur. Expire olmuşsa update eder.
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationUserToken> GetToken()
        {
            ApplicationUserToken? userTokens = null;
            TokenResponseDto? tokenInfo = null;

            //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
            if (this.uow.GetRepository<ApplicationUserToken>().GetQueryable().Count(x => x.UserId == _applicationUser.Id) > 0)
            {
                //İlgili token bilgileri bulunur.
                userTokens = this.uow.GetRepository<ApplicationUserToken>().GetQueryable().FirstOrDefault(x => x.UserId == _applicationUser.Id);

                //Expire olmuş ise yeni token oluşturup günceller.
                if (userTokens?.ExpireDate <= DateTime.UtcNow)
                {
                    //Create new token
                    tokenInfo = GenerateToken();

                    userTokens.ExpireDate = tokenInfo.ExpireDate;
                    userTokens.Value = tokenInfo.Token;

                    await this.uow.CommitAsync();
                }
            }
            else
            {
                //Create new token
                tokenInfo = GenerateToken();

                userTokens = new ApplicationUserToken();

                userTokens.UserId = _applicationUser.Id;
                userTokens.LoginProvider = "SystemAPI";
                userTokens.Name = _applicationUser?.UserName;
                userTokens.ExpireDate = tokenInfo.ExpireDate;
                userTokens.Value = tokenInfo.Token;

                await this.uow.GetRepository<ApplicationUserToken>().CreateAsync(userTokens);
            }

            await this.uow.CommitAsync();

            return userTokens;
        }

        /// <summary>
        /// Kullanıcıya ait tokenı siler.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteToken()
        {
            bool ret = true;

            try
            {
                //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
                if (this.uow.GetRepository<ApplicationUserToken>().GetQueryable().Count(x => x.UserId == _applicationUser.Id) > 0)
                {
                    ApplicationUserToken? userTokens = this.uow.GetRepository<ApplicationUserToken>().GetQueryable().FirstOrDefault(x => x.UserId == _applicationUser.Id);

                    if (userTokens != null)
                    {
                        this.uow.GetRepository<ApplicationUserToken>().Remove(userTokens);
                    }

                }

                await this.uow.CommitAsync();
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Yeni token oluşturur.
        /// </summary>
        /// <returns></returns>
        private TokenResponseDto GenerateToken()
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(this.roles[0]))
                claims.Add(new Claim(ClaimTypes.Role, this.roles[0]));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, _applicationUser.Id.ToString()));

            if (!string.IsNullOrEmpty(_applicationUser.UserName))
                claims.Add(new Claim("Username", _applicationUser.UserName));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cokgizliTokenkeyi!_cokGizlicokgizliTokenkeyi!_cokGizli"));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(30);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer:"http://localhost", audience: "http://localhost", claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            

            TokenResponseDto tokenInfo = new TokenResponseDto();

            tokenInfo.Token = handler.WriteToken(jwtSecurityToken);
            tokenInfo.ExpireDate = expireDate;

            return tokenInfo;
        }
    }
}
