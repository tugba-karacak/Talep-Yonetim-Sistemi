using MediatR;
using Microsoft.AspNetCore.Identity;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.BusinessLayer.Tools;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.LoginRequest
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUow _uow;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUow uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
        }

        public async Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Result<TokenResponseDto> result = new Result<TokenResponseDto>();

            try
            {


                //Kulllanıcı bulunur.
                ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

                //Kullanıcı var ise;
                //if (user == null)
                //{
                //    return Unauthorized();
                //}


                if (user == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Kullanıcı adı veya şifre hatalı.";
                    return result;
                }

                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user,
                                                                                                                   request.Password,
                                                                                                                   false,
                                                                                                                   false);
                //Kullanıcı adı ve şifre kontrolü
                if (signInResult.Succeeded == false)
                {
                    result.IsSuccess = false;
                    result.Message = "Kullanıcı adı veya şifre hatalı.";
                    return result;

                }




                var roles = await _userManager.GetRolesAsync(user);

                AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(_uow, user, roles.ToList());
                ApplicationUserToken? userTokens = await accessTokenGenerator.GetToken();

                result.IsSuccess = true;
                result.Message = "Kullanıcı giriş yaptı.";
                result.Data = new TokenResponseDto
                {
                    Token = userTokens.Value,
                    ExpireDate = userTokens.ExpireDate
                };




                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;

                return result;
            }
        }
    }
}
