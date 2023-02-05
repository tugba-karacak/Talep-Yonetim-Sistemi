using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;
using UpSchool.HelpDesk.PresentationLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Runtime;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Reflection;

namespace UpSchool.HelpDesk.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthApiService authApiService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AccountController(AuthApiService authApiService, IHttpContextAccessor httpContextAccessor)
        {
            this.authApiService = authApiService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.authApiService.LoginAsync(model);
                if (result.IsSuccess)
                {

                    if (result.Data != null)
                    {
                        JwtSecurityTokenHandler handler = new();
                        var token = handler.ReadJwtToken(result.Data.Token);

                        var roleValue = token.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                        var idvalue = token.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                        var claims = new List<Claim>
                            {
                                new Claim("accessToken",result.Data.Token),
                                new Claim(ClaimTypes.NameIdentifier,idvalue),
                                new Claim(ClaimTypes.Role, roleValue),
                            };


                        await HttpContext.SignInAsync("default", new ClaimsPrincipal(new ClaimsIdentity(claims, "default")));
                        if (roleValue == "Admin")
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Profile");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View(new UserRegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                await this.authApiService.RegisterAsync(model);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        private async Task SendMail(string emailAddress)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailAddress));

                email.Subject = "Test Email Subject";
                email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Example HTML Message Body</h1>" };

                // send email
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.tugbakaracak.com", 587, false);
                smtp.Authenticate("mail@tugbakaracak.com", "!9Gp597ed");
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("default");
            return RedirectToAction("Login");
        }
    }
}
