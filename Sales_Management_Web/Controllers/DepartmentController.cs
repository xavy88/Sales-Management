using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales_Management_Web.Model;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Controllers
{
    
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper; 
        }
        public async Task<IActionResult> Index()
        {
            List<DepartmentDTO> list = new();

            var response = await _departmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _departmentService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Department created successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Something were wrong created the Department";
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _departmentService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                DepartmentDTO model = JsonConvert.DeserializeObject<DepartmentDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<DepartmentUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(DepartmentUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Department updated successfully";
                var response = await _departmentService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Something were wrong updating the Department";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _departmentService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                DepartmentDTO model = JsonConvert.DeserializeObject<DepartmentDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DepartmentDTO model)
        {
            
                var response = await _departmentService.DeleteAsync<APIResponse>(model.Id);
                if (response != null && response.IsSuccess)
                {
                TempData["success"] = "Department deleted successfully";
                return RedirectToAction(nameof(Index));
                }
            TempData["error"] = "Something were wrong deleting the Department";
            return View(model);
        }
    }
}
