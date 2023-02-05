using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class WorkRequestController : Controller
    {
        private readonly WorkRequestApiService workRequestApiService;
        private readonly UserApiService userApiService;

        public WorkRequestController(WorkRequestApiService workRequestApiService, UserApiService userApiService)
        {
            this.workRequestApiService = workRequestApiService;
            this.userApiService = userApiService;
        }

        public async Task<IActionResult> List()
        {
            var result = await this.workRequestApiService.GetAllAsync();
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View(new CreateWorkRequestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await this.workRequestApiService.CreateAsync(model);
                if (result.IsSuccess)
                {
                    return RedirectToAction("List");
                }
                ModelState.AddModelError("","Sunucu tarafında bir hata oluştu, ürün yöneticinizle görüşünüz");
            }
         
            return View(model);
        }

   
        public async Task<IActionResult> Update(int id)
        {
            var result = await this.workRequestApiService.GetAsync(id);
            if (!result.IsSuccess) {
                ModelState.AddModelError("", "Sunucu tarafında bir hata oluştu, ürün yöneticinizle görüşünüz");
                return View(new WorkRequestModel());
            }
            return View(new UpdateWorkRequestModel
            {
                Id = result.Data.Id,
                Description= result.Data.Description,
                Title= result.Data.Title,
            });
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateWorkRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.workRequestApiService.UpdateAsync(model);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("", "Sunucu tarafında bir hata oluştu, ürün yöneticinizle görüşünüz");
                    return View(new WorkRequestModel());
                }
                return RedirectToAction("List");
            }
            return View(model);
        }

        public async Task<IActionResult> AssignDeveloper(int workRequestId)
        {
            //bütün memberlar listenecek;

            var memberResult = await this.userApiService.GetMembersAsync();
            ViewBag.Members = memberResult.Data;
            return View(new AssignUserModel
            {
                WorkRequestId = workRequestId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> AssignDeveloper(AssignUserModel model)
        {
            await this.workRequestApiService.AssignUserAsync(model);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> AssignToMe()
        {
            var stringId = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var id = int.Parse(stringId);
            var workRequestData = await this.workRequestApiService.GetWorkRequestsByUserIdAsync(id);

        

            return View(workRequestData.Data);
        }

        public async Task<IActionResult> SetCompleted(int id)
        {
            await this.workRequestApiService.CompletedAsync(id);
            return RedirectToAction("AssignToMe");
        }
    }
}
