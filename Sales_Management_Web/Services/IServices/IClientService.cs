using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IClientService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ClientCreateDTO dto);
        Task<T> UpdateAsync<T>(ClientUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
