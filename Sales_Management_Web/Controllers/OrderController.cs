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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IServicesService _servicesService;
        private readonly IClientService _clientService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper, IClientService clientService, IServicesService servicesService, IEmployeeService employeeService)        {
            _servicesService = servicesService;
            _orderService = orderService;
            _clientService = clientService;
            _employeeService = employeeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<OrderDTO> list = new();

            var response = await _orderService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            OrderCreateVM orderVM = new();
            var responseService = await _servicesService.GetAllAsync<APIResponse>();
            var responseClient = await _clientService.GetAllAsync<APIResponse>();
            var responseEmployee = await _employeeService.GetAllAsync<APIResponse>();
            if ((responseService != null && responseService.IsSuccess) && (responseClient != null && responseClient.IsSuccess) && (responseEmployee != null && responseEmployee.IsSuccess))
            {
                orderVM.ServiceList = JsonConvert.DeserializeObject<List<ServiceDTO>>(Convert.ToString(responseService.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                orderVM.CompanyList = JsonConvert.DeserializeObject<List<ClientDTO>>(Convert.ToString(responseClient.Result)).Select(i => new SelectListItem
                {
                    Text = i.CompanyName,
                    Value = i.Id.ToString(),
                });
                orderVM.EmployeeList = JsonConvert.DeserializeObject<List<EmployeeDTO>>(Convert.ToString(responseEmployee.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
            return View(orderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.CreateAsync<APIResponse>(model.Order);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Order created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count>0)
                    {
                        TempData["error"] = "Something were wrong creating the Order";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var responseService = await _servicesService.GetAllAsync<APIResponse>();
            var responseClient = await _clientService.GetAllAsync<APIResponse>();
            var responseEmployee = await _employeeService.GetAllAsync<APIResponse>();
            if ((responseService != null && responseService.IsSuccess) && (responseClient != null && responseClient.IsSuccess) && (responseEmployee != null && responseEmployee.IsSuccess))
            {
                model.ServiceList = JsonConvert.DeserializeObject<List<ServiceDTO>>(Convert.ToString(responseService.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                model.CompanyList = JsonConvert.DeserializeObject<List<ClientDTO>>(Convert.ToString(responseClient.Result)).Select(i => new SelectListItem
                {
                    Text = i.CompanyName,
                    Value = i.Id.ToString(),
                });
                model.EmployeeList = JsonConvert.DeserializeObject<List<EmployeeDTO>>(Convert.ToString(responseEmployee.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }
                          

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            OrderUpdateVM orderVM = new();
            var response = await _orderService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                OrderDTO model = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(response.Result));
                orderVM.Order =  (_mapper.Map<OrderUpdateDTO>(model));
            }

            var responseService = await _servicesService.GetAllAsync<APIResponse>();
            var responseClient = await _clientService.GetAllAsync<APIResponse>();
            var responseEmployee = await _employeeService.GetAllAsync<APIResponse>();
            if ((responseService != null && responseService.IsSuccess) && (responseClient != null && responseClient.IsSuccess) && (responseEmployee != null && responseEmployee.IsSuccess))
            {
                orderVM.ServiceList = JsonConvert.DeserializeObject<List<ServiceDTO>>(Convert.ToString(responseService.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                orderVM.CompanyList = JsonConvert.DeserializeObject<List<ClientDTO>>(Convert.ToString(responseClient.Result)).Select(i => new SelectListItem
                {
                    Text = i.CompanyName,
                    Value = i.Id.ToString(),
                });
                orderVM.EmployeeList = JsonConvert.DeserializeObject<List<EmployeeDTO>>(Convert.ToString(responseEmployee.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                return View(orderVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OrderUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.UpdateAsync<APIResponse>(model.Order);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Order updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        TempData["error"] = "Something were wrong updating the Order";
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }


            var responseService = await _servicesService.GetAllAsync<APIResponse>();
            var responseClient = await _clientService.GetAllAsync<APIResponse>();
            var responseEmployee = await _employeeService.GetAllAsync<APIResponse>();
            if ((responseService != null && responseService.IsSuccess) && (responseClient != null && responseClient.IsSuccess) && (responseEmployee != null && responseEmployee.IsSuccess))
            {
                model.ServiceList = JsonConvert.DeserializeObject<List<ServiceDTO>>(Convert.ToString(responseService.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                model.CompanyList = JsonConvert.DeserializeObject<List<ClientDTO>>(Convert.ToString(responseClient.Result)).Select(i => new SelectListItem
                {
                    Text = i.CompanyName,
                    Value = i.Id.ToString(),
                });
                model.EmployeeList = JsonConvert.DeserializeObject<List<EmployeeDTO>>(Convert.ToString(responseEmployee.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
            }


            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            OrderDeleteVM orderVM = new();
            var response = await _orderService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                OrderDTO model = JsonConvert.DeserializeObject<OrderDTO>(Convert.ToString(response.Result));
                orderVM.Order = model;
            }

            var responseService = await _servicesService.GetAllAsync<APIResponse>();
            var responseClient = await _clientService.GetAllAsync<APIResponse>();
            var responseEmployee = await _employeeService.GetAllAsync<APIResponse>();
            if ((responseService != null && responseService.IsSuccess) && (responseClient != null && responseClient.IsSuccess) && (responseEmployee != null && responseEmployee.IsSuccess))
            {
                orderVM.ServiceList = JsonConvert.DeserializeObject<List<ServiceDTO>>(Convert.ToString(responseService.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                orderVM.CompanyList = JsonConvert.DeserializeObject<List<ClientDTO>>(Convert.ToString(responseClient.Result)).Select(i => new SelectListItem
                {
                    Text = i.CompanyName,
                    Value = i.Id.ToString(),
                });
                orderVM.EmployeeList = JsonConvert.DeserializeObject<List<EmployeeDTO>>(Convert.ToString(responseEmployee.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                });
                return View(orderVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(OrderDeleteVM model)
        {

            var response = await _orderService.DeleteAsync<APIResponse>(model.Order.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Order deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Something were wrong deleting the Order";
            return View(model);
        }
    }
}
