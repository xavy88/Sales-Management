using System.ComponentModel.DataAnnotations;

namespace Sales_Management_Web.Model.DTO
{
    public class ServiceUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string PriceRange { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } = true;
        public DateTime UpdatedDate { get; set; }

    }
}
