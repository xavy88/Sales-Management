using Sales_Management_Utility;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Services
{
    public class ClientService : BaseService, IClientService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string clientUrl;

        public ClientService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            clientUrl = configuration.GetValue<string>("ServiceUrls:Sales_Management_API");
        }
        public Task<T> CreateAsync<T>(ClientCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = clientUrl + "/api/ClientAPI",
                Token = token,
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = clientUrl + "/api/ClientAPI/"+id,
                Token = token,
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = clientUrl + "/api/ClientAPI",
                Token = token,
            });

        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = clientUrl + "/api/ClientAPI/" + id,
                Token = token,
            });
        }

        public Task<T> UpdateAsync<T>(ClientUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = clientUrl + "/api/ClientAPI/" + dto.Id,
                Token = token,
            });
        }
    }
}
