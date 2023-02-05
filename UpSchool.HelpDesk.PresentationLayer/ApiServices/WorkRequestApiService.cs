using System.Reflection;
using System.Text.Json;
using UpSchool.HelpDesk.PresentationLayer.Helpers;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.ApiServices
{
    public class WorkRequestApiService
    {

        private readonly HttpClient httpClient;
        private readonly TokenHelper tokenHelper;
        private string? token;

        public WorkRequestApiService(HttpClient httpClient, TokenHelper tokenHelper)
        {
            this.httpClient = httpClient;
            this.tokenHelper = tokenHelper;


            token = this.tokenHelper.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<Result<List<WorkRequestModel>>> GetAllAsync()
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<List<WorkRequestModel>>>("api/WorkRequests");

            return response ?? new Result<List<WorkRequestModel>>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<List<WorkRequestModel>>> GetWorkRequestsByUserIdAsync(int userId)
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<List<WorkRequestModel>>>($"api/WorkRequests/GetByUserId/{userId}");

            return response ?? new Result<List<WorkRequestModel>>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<WorkRequestModel>> CreateAsync(CreateWorkRequestModel createWorkRequestModel)
        {
            var content = StringContentHelper.CreateStringContent(createWorkRequestModel);
            var response = await this.httpClient.PostAsync("api/WorkRequests", content);

            var jsonData = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Result<WorkRequestModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return result ?? new Result<WorkRequestModel>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }
        public async Task<Result<NoContent>> UpdateAsync(UpdateWorkRequestModel updateWorkRequestModel)
        {
            var content = StringContentHelper.CreateStringContent(updateWorkRequestModel);
            await this.httpClient.PutAsync("api/WorkRequests", content);

            return new Result<NoContent>()
            {
                IsSuccess = true,
                Message = "Başarılı",
            };
        }

        public async Task<Result<WorkRequestModel>> GetAsync(int id)
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<WorkRequestModel>>($"api/WorkRequests/{id}");
            return response ?? new Result<WorkRequestModel>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<NoContent>> AssignUserAsync(AssignUserModel model)
        {
            var content = StringContentHelper.CreateStringContent(model);
            var response = await this.httpClient.PutAsync($"api/WorkRequests/AssignUser", content);

            return new Result<NoContent> { IsSuccess = true, Message = "İşlem başarılı" };
        }


        public async Task<Result<NoContent>> CompletedAsync(int id)
        {
            var content = StringContentHelper.CreateStringContent(new CompletedWorkRequestModel(id));
            var response = await this.httpClient.PutAsync($"api/WorkRequests/SetCompleted", content);

            return new Result<NoContent> { IsSuccess = true, Message = "İşlem başarılı" };
        }
    }
}
