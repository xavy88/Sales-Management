using Sales_Management_Web.Models.DTO;

namespace Sales_Management_Web.Model.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
        
    }
}
