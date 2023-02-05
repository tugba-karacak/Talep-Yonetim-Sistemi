using System.Collections.Generic;
using UpSchool.HelpDesk.PresentationLayer.Helpers;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.ApiServices
{
    public class UserApiService
    {
        private readonly HttpClient httpClient;
        private readonly TokenHelper tokenHelper;
        private string? token;

        public UserApiService(HttpClient httpClient, TokenHelper tokenHelper)
        {
            this.httpClient = httpClient;
            this.tokenHelper = tokenHelper;


            token = this.tokenHelper.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<Result<string>> GetUserAsync(int userId)
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<string>>($"api/Users/{userId}");

            return response ?? new Result<string>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<List<ApplicationUserModel>>> GetMembersAsync()
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<List<ApplicationUserModel>>>($"api/Users/GetMembers");

            return response ?? new Result<List<ApplicationUserModel>>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<ApplicationUserModel>> GetProfileAsync(int id)
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<ApplicationUserModel>>($"api/Users/GetProfile/{id}");

            return response ?? new Result<ApplicationUserModel>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<NoContent>> UpdateProfile(UpdateToProfileModel model)
        {
            var content = StringContentHelper.CreateStringContent(model);
            var response = await this.httpClient.PutAsync("api/Users/", content);

            return new Result<NoContent> { IsSuccess = true, Message = "İşlem başarılı" };
        }

        public async Task<Result<StaticsDataModel>> GetStatics()
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<StaticsDataModel>>($"api/Users/Statics");

            return response ?? new Result<StaticsDataModel>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<List<StaticForGraphModel>>> GetStaticForGraph()
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<List<StaticForGraphModel>>>($"api/Users/StaticForGraph");

            return response ?? new Result<List<StaticForGraphModel>>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

    }
}
