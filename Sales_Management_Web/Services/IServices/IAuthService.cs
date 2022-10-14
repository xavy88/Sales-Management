using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO objCreate);
    }
}
