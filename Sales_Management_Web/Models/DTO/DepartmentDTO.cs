using System.ComponentModel.DataAnnotations;

namespace Sales_Management_Web.Model.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
