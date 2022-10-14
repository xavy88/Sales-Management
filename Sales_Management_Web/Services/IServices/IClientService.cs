using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IClientService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ClientCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ClientUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
