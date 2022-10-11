using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Sales_Management_Web.Model;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Models.VM;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServicesService _servicesService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public ServiceController(IServicesService servicesService, IMapper mapper, IDepartmentService departmentService)
        {
            _servicesService = servicesService;
            _departmentService = departmentService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<ServiceDTO> list = new();

            var response = await _servicesService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ServiceDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            ServiceCreateVM serviceVM = new();
            var response = await _departmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                serviceVM.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(serviceVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _servicesService.CreateAsync<APIResponse>(model.Service);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Service created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count>0)
                    {
                        TempData["error"] = "Something were wrong created the Service";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

           
            var resp = await _departmentService.GetAllAsync<APIResponse>();
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
            ServiceUpdateVM serviceVM = new();
            var response = await _servicesService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                ServiceDTO model = JsonConvert.DeserializeObject<ServiceDTO>(Convert.ToString(response.Result));
                serviceVM.Service =  (_mapper.Map<ServiceUpdateDTO>(model));
            }

            response = await _departmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                serviceVM.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                return View(serviceVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _servicesService.UpdateAsync<APIResponse>(model.Service);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Service updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        TempData["error"] = "Something were wrong updating the Service";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }


            var resp = await _departmentService.GetAllAsync<APIResponse>();
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
            ServiceDeleteVM serviceVM = new();
            var response = await _servicesService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                ServiceDTO model = JsonConvert.DeserializeObject<ServiceDTO>(Convert.ToString(response.Result));
                serviceVM.Service = model;
            }

            response = await _departmentService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                serviceVM.DepartmentList = JsonConvert.DeserializeObject<List<DepartmentDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                return View(serviceVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ServiceDeleteVM model)
        {

            var response = await _servicesService.DeleteAsync<APIResponse>(model.Service.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
