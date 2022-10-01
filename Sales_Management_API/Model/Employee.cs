using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_Management_API.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Department")]
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime HiredDate { get; set; }
        public string Remark { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
