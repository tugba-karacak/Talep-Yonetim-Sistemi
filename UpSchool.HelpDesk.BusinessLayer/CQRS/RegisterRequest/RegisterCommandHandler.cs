using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.RegisterRequest
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<NoContent>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUow uow;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUow uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.uow = uow;
        }

        public async Task<Result<NoContent>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            Result<NoContent> result = new Result<NoContent>();
            try
            {

                var existsUser = await  uow.GetRepository<ApplicationUser>().GetByFilterAsync(x=>x.Email == request.EmailAddress);

                if (existsUser != null)
                {
                    result.IsSuccess = false;
                    result.Message = "Kullanıcı zaten var.";

                }

                //Kullanıcı bilgileri set edilir.
                ApplicationUser user = new ApplicationUser();

               
                user.Email = request.EmailAddress.Trim();
                user.UserName = request.EmailAddress.Trim();
                user.Name = request.Name.Trim();
                user.Image = "defaultProfil.jpg";

                //Kullanıcı oluşturulur.
                IdentityResult createUserResult = await _userManager.CreateAsync(user, request.Password.Trim());

                //Kullanıcı oluşturuldu ise
                if (createUserResult.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync("Member");

                    if (!roleExists)
                    {
                  
                        _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = "Member",
                        }).Wait();
                    }

                    //Kullanıcıya ilgili rol ataması yapılır.
                    _userManager.AddToRoleAsync(user, "Member").Wait();

                    result.IsSuccess = true;
                    result.Message = "Kullanıcı başarılı şekilde oluşturuldu.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", createUserResult?.Errors?.FirstOrDefault()?.Description);
                }

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
