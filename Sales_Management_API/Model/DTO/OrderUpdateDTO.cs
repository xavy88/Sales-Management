using System.ComponentModel.DataAnnotations;

namespace Sales_Management_API.Model.DTO
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
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
        public bool Status { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
