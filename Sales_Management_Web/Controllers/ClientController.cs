using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales_Management_Web.Model;
using Sales_Management_Web.Model.DTO;
using Sales_Management_Web.Services.IServices;

namespace Sales_Management_Web.Controllers
{
    
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper; 
        }
        public async Task<IActionResult> Index()
        {
            List<ClientDTO> list = new();

            var response = await _clientService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ClientDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _clientService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Client created successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Something were wrong created the Client";
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _clientService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                ClientDTO model = JsonConvert.DeserializeObject<ClientDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ClientUpdateDTO>(model));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ClientUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Client updated successfully";
                var response = await _clientService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Something were wrong updating the Client";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _clientService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                ClientDTO model = JsonConvert.DeserializeObject<ClientDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ClientDTO model)
        {
            
                var response = await _clientService.DeleteAsync<APIResponse>(model.Id);
                if (response != null && response.IsSuccess)
                {
                TempData["success"] = "Client deleted successfully";
                return RedirectToAction(nameof(Index));
                }
            TempData["error"] = "Something were wrong deleting the Client";
            return View(model);
        }
    }
}
