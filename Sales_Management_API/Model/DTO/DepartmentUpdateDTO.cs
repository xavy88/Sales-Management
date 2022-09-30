using System.ComponentModel.DataAnnotations;

namespace Sales_Management_API.Model.DTO
{
    public class DepartmentUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
