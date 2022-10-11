using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Services.IServices
{
    public interface IEmployeeService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(EmployeeCreateDTO dto);
        Task<T> UpdateAsync<T>(EmployeeUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
