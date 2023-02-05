using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;

namespace UpSchool.HelpDesk.PresentationLayer.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly UserApiService userApiService;

        public DashboardController(UserApiService userApiService)
        {
            this.userApiService = userApiService;
        }

        public async  Task<IActionResult> Index()
        {
            var result = await this.userApiService.GetStatics();
            return View(result.Data);
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetGraphData()
        {
            var result = await this.userApiService.GetStaticForGraph();
            return Json(result.Data);
        }
    }
}
