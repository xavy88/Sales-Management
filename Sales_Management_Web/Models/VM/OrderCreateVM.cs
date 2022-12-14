using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Models.VM
{
    public class OrderCreateVM
    {
        public OrderCreateVM()
        {
            Order = new OrderCreateDTO();
        }
        public OrderCreateDTO Order { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeList { get; set; }

    }
}
