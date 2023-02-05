using System.Text.Json;
using UpSchool.HelpDesk.PresentationLayer.Helpers;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.ApiServices
{
    public class WorkRequestStateApiService
    {
        private readonly HttpClient httpClient;
        private readonly TokenHelper tokenHelper;
        private string? token;

        public WorkRequestStateApiService(HttpClient httpClient, TokenHelper tokenHelper)
        {
            this.httpClient = httpClient;
            this.tokenHelper = tokenHelper;


            token = this.tokenHelper.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }



        public async Task<Result<List<WorkRequestStateModel>>> GetWorkRequestStatesAsync(int workRequestId)
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<List<WorkRequestStateModel>>>($"api/WorkRequestStates/GetWorkRequestStates/{workRequestId}");

            return response ?? new Result<List<WorkRequestStateModel>>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<WorkRequestStateModel>> GetAsync(int id)
        {
            var response = await this.httpClient.GetFromJsonAsync<Result<WorkRequestStateModel>>($"api/WorkRequestStates/{id}");

            return response ?? new Result<WorkRequestStateModel>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }

        public async Task<Result<WorkRequestStateModel>> CreateAsync(CreateWorkRequestStateModel createWorkRequestModel)
        {
            var content = StringContentHelper.CreateStringContent(createWorkRequestModel);
            var response = await this.httpClient.PostAsync("api/WorkRequestStates", content);

            var jsonData = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Result<WorkRequestStateModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return result ?? new Result<WorkRequestStateModel>()
            {
                IsSuccess = false,
                Message = "Bir hata oluştu",
            };
        }
        public async Task<Result<NoContent>> UpdateAsync(UpdateWorkRequestStateModel updateWorkRequestModel)
        {
            var content = StringContentHelper.CreateStringContent(updateWorkRequestModel);
            await this.httpClient.PutAsync("api/WorkRequestStates",content);

            return new Result<NoContent>()
            {
                IsSuccess = true,
                Message = "Başarılı",
            };
        }

        

        public async Task<Result<NoContent>> DeleteAsync(int id)
        {
            var response = await this.httpClient.DeleteAsync($"api/WorkRequestStates/{id}");

            return new Result<NoContent> { IsSuccess = true, Message = "İşlem başarılı" };
        }

    }
}
