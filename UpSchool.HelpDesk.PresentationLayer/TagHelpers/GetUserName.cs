using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;

namespace UpSchool.HelpDesk.PresentationLayer.TagHelpers
{
    [HtmlTargetElement("getusername")]
    public class GetUserName : TagHelper
    {
        public int UserId { get; set; }

        private readonly UserApiService userApiService;

        public GetUserName(UserApiService userApiService)
        {
            this.userApiService = userApiService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var userName = userApiService.GetUserAsync(UserId).Result;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<span class='badge bg-gradient-secondary'> {0} </span>", userName.Data);
     

            var data = $"{userName.Data}";
            output.Content.SetHtmlContent(stringBuilder.ToString());
            //base.Process(context, output);
        }
    }
}
