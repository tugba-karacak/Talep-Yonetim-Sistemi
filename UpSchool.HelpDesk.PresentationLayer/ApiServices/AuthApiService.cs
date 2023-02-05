using System.Text.Json;
using UpSchool.HelpDesk.PresentationLayer.Helpers;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.ApiServices
{
    public class AuthApiService
    {
        private readonly HttpClient httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Result<TokenResponseModel>> LoginAsync(UserLoginModel model)
        {

            var content = StringContentHelper.CreateStringContent(model);
            var response = await this.httpClient.PostAsync("api/Auth/Login", content);

            var jsonData = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Result<TokenResponseModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return result;
        }

        public async Task<Result<NoContent>> RegisterAsync(UserRegisterModel model)
        {

            var content = StringContentHelper.CreateStringContent(model);
            var response = await this.httpClient.PostAsync("api/Auth/Register", content);

            var jsonData = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Result<NoContent>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return result;
        }



    }
}
