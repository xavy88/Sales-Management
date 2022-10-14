using Sales_Management_Utility;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string authUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            authUrl = configuration.GetValue<string>("ServiceUrls:Sales_Management_API");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = authUrl + "/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = authUrl + "/api/UsersAuth/register"
            });
        }
    }
}
