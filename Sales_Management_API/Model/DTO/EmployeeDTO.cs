using System.ComponentModel.DataAnnotations;

namespace Sales_Management_API.Model.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public DepartmentDTO Department { get; set; }
        public DateTime HiredDate { get; set; }
        public string Remark { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

    }
}
