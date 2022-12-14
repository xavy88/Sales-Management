using System.ComponentModel.DataAnnotations;

namespace Sales_Management_Web.Model.DTO
{
    public class OrderCreateDTO
    {
        [Required]
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ServiceId { get; set; }
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
        public string Remark { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        
    }
}
