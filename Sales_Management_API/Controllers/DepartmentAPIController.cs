using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sales_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly ILogger<DepartmentAPIController> _logger;

        public DepartmentAPIController(ILogger<DepartmentAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            _logger.LogInformation("Getting all Departmens");
            return Ok();
        }

        
    }
}
