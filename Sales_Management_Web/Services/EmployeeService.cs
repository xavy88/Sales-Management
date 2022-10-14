using Sales_Management_Utility;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string employeeUrl;

        public EmployeeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            employeeUrl = configuration.GetValue<string>("ServiceUrls:Sales_Management_API");
        }
        public Task<T> CreateAsync<T>(EmployeeCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = employeeUrl + "/api/EmployeeAPI",
                Token = token,
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = employeeUrl + "/api/EmployeeAPI/" + id,
                Token = token,
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = employeeUrl + "/api/EmployeeAPI",
                Token = token,
            });

        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = employeeUrl + "/api/EmployeeAPI/" + id,
                Token = token,
            });
        }

        public Task<T> UpdateAsync<T>(EmployeeUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = employeeUrl + "/api/EmployeeAPI/" + dto.Id,
                Token = token,
            });
        }
    }
}
