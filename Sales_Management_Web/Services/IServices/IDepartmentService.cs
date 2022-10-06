using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IDepartmentService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(DepartmentCreateDTO dto);
        Task<T> UpdateAsync<T>(DepartmentUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
