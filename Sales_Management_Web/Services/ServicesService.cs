using Sales_Management_Utility;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Services
{
    public class ServicesService : BaseService, IServicesService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string serviceUrl;

        public ServicesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            serviceUrl = configuration.GetValue<string>("ServiceUrls:Sales_Management_API");
        }
        public Task<T> CreateAsync<T>(ServiceCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = serviceUrl + "/api/ServiceAPI",
                Token = token,
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = serviceUrl + "/api/ServiceAPI/" + id,
                Token = token,
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = serviceUrl + "/api/ServiceAPI",
                Token = token,
            });

        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = serviceUrl + "/api/ServiceAPI/" + id,
                Token = token,
            });
        }

        public Task<T> UpdateAsync<T>(ServiceUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = serviceUrl + "/api/ServiceAPI/" + dto.Id,
                Token = token,
            });
        }
    }
}
