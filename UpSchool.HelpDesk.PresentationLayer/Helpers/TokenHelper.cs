namespace UpSchool.HelpDesk.PresentationLayer.Helpers
{
    public class TokenHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string? GetToken()
        {
            var token = this.httpContextAccessor?.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == "accessToken")?.Value;

            return token;
        }
    }
}
