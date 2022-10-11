using System.ComponentModel.DataAnnotations;

namespace Sales_Management_Web.Model.DTO
{
    public class ClientUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } = true;
        public DateTime UpdatedDate { get; set; }

    }
}
