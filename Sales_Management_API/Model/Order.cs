using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_Management_API.Model
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; }
        [ForeignKey("Client")]
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Service")]
        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public double Price { get; set; }
        [ForeignKey("Employee")]
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Remark { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
