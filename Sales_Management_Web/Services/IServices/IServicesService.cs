using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IServicesService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ServiceCreateDTO dto);
        Task<T> UpdateAsync<T>(ServiceUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
