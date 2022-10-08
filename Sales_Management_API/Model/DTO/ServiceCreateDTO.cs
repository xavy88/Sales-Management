using System.ComponentModel.DataAnnotations;

namespace Sales_Management_API.Model.DTO
{
    public class ServiceCreateDTO
    {
        public string Name { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string PriceRange { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; }

    }
}
