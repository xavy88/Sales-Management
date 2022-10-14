using Sales_Management_Utility;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string departmentUrl;

        public DepartmentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            departmentUrl = configuration.GetValue<string>("ServiceUrls:Sales_Management_API");
        }
        public Task<T> CreateAsync<T>(DepartmentCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = departmentUrl + "/api/DepartmentAPI",
                Token = token,
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = departmentUrl + "/api/DepartmentAPI/"+id,
                Token = token,
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = departmentUrl + "/api/DepartmentAPI",
                Token = token,
            });

        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = departmentUrl + "/api/DepartmentAPI/" + id,
                Token = token,
            });
        }

        public Task<T> UpdateAsync<T>(DepartmentUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = departmentUrl + "/api/DepartmentAPI/" + dto.Id,
                Token = token,
            });
        }
    }
}
