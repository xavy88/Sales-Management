using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Sales_Management_Utility;
using Sales_Management_Web.Model;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models.VM;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<EmployeeDTO> list = new();

            var response = await _employeeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<EmployeeDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            EmployeeCreateVM employeeVM = new();
            var response = await _departmentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                employeeVM.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(employeeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.CreateAsync<APIResponse>(model.Employee, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Employee created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count>0)
                    {
                        TempData["error"] = "Something were wrong created the Employee";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

           
            var resp = await _departmentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            EmployeeUpdateVM employeeVM = new();
            var response = await _employeeService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                EmployeeDTO model = JsonConvert.DeserializeObject<EmployeeDTO>(Convert.ToString(response.Result));
                employeeVM.Employee =  (_mapper.Map<EmployeeUpdateDTO>(model));
            }

            response = await _departmentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                employeeVM.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                return View(employeeVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.UpdateAsync<APIResponse>(model.Employee, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Employee updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        TempData["error"] = "Something were wrong updating the Employee";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }


            var resp = await _departmentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }


            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            EmployeeDeleteVM employeeVM = new();
            var response = await _employeeService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                EmployeeDTO model = JsonConvert.DeserializeObject<EmployeeDTO>(Convert.ToString(response.Result));
                employeeVM.Employee = model;
            }

            response = await _departmentService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                employeeVM.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                return View(employeeVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteVM model)
        {

            var response = await _employeeService.DeleteAsync<APIResponse>(model.Employee.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Employee deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Something were wrong deleting the Employee";
            return View(model);
        }
    }
}
