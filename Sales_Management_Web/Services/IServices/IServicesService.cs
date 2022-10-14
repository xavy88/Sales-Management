using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IServicesService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ServiceCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ServiceUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
