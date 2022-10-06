using Sales_Management_Web.Model;
using Sales_Management_Web.Models;

namespace Sales_Management_Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
