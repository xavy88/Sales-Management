using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web.Models.VM
{
    public class EmployeeUpdateVM
    {
        public EmployeeUpdateVM()
        {
            Employee = new EmployeeUpdateDTO();
        }
        public EmployeeUpdateDTO Employee { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}
