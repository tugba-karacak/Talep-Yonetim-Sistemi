using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserApiService userApiService;

        public ProfileController(UserApiService userApiService)
        {
            this.userApiService = userApiService;
        }

        public async Task<IActionResult> Index()
        {
            var stringId = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var id = int.Parse(stringId);
            var profileData = await this.userApiService.GetProfileAsync(id);
            return View(profileData.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileModel model)
        {
            
            if(model.File != null)
            {
                var path = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(model.File.FileName);
                var newFileName = Guid.NewGuid()+extension;
                var savePath = Path.Combine(path, "wwwroot","images", newFileName);

                var stream = new FileStream(savePath, FileMode.Create);

                await model.File.CopyToAsync(stream);

                model.Image = newFileName;
            }

            await this.userApiService.UpdateProfile(new UpdateToProfileModel
            {
                Id = model.Id,
                Image = model.Image,
                Name = model.Name
            });

            return RedirectToAction("Index");
        }
    }
}
