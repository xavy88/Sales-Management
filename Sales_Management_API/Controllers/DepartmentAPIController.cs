using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Model.DTO;

namespace Sales_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly ILogger<DepartmentAPIController> _logger;
        private readonly ApplicationDbContext _db;

        public DepartmentAPIController(ILogger<DepartmentAPIController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            return Ok(_db.Departments.ToList());
        }
       
        [HttpGet("{id:int}",Name ="GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DepartmentDTO> GetDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var department = _db.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DepartmentDTO> CreateDeparment([FromBody] DepartmentDTO departmentDTO)
        {
            if (_db.Departments.FirstOrDefault(x=>x.Name.ToLower() == departmentDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("ErrorMessage", "Department already exists!");
                return BadRequest(ModelState);
            }
            if (departmentDTO == null)
            {
                return BadRequest(departmentDTO);
            }
            if (departmentDTO.Id >0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Department model = new Department()
            {
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                Status = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            _db.Departments.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetDepartment", new { id = departmentDTO.Id }, departmentDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name ="DeleteDepartment")]
        public IActionResult DeleteDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var department = _db.Departments.FirstOrDefault(x=>x.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            _db.Departments.Remove(department);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDTO)
        {
            if(departmentDTO == null || id != departmentDTO.Id)
            {
                return BadRequest();
            }

            Department model = new Department()
            {
                Id = id,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                Status = departmentDTO.Status,
                UpdatedDate = DateTime.Now,
            };
            _db.Departments.Update(model);
            _db.SaveChanges();
            return NoContent();

        }
    }
}
