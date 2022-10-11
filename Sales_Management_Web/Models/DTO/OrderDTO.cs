using System.ComponentModel.DataAnnotations;

namespace Sales_Management_Web.Model.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        public ClientDTO Client { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public ServiceDTO Service { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public EmployeeDTO Employee { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
