using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public DepartmentAPIController(ILogger<DepartmentAPIController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            IEnumerable<Department> departmentList = await _db.Departments.ToListAsync();
            return Ok(_mapper.Map<List<DepartmentDTO>>(departmentList));
        }
       
        [HttpGet("{id:int}",Name ="GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DepartmentDTO>(department));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DepartmentDTO>> CreateDeparment([FromBody] DepartmentCreateDTO createDTO)
        {
            if ( await _db.Departments.FirstOrDefaultAsync(x=>x.Name.ToLower() == createDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("ErrorMessage", "Department already exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                ModelState.AddModelError("ErrorMessage", "Error Creating Department!");
                return BadRequest(createDTO);
            }
            Department model = _mapper.Map<Department>(createDTO);
            
            await _db.Departments.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetDepartment", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name ="DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var department = await _db.Departments.FirstOrDefaultAsync(x=>x.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            _db.Departments.Remove(department);
           await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDTO updateDTO)
        {
            if(updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }
            Department model = _mapper.Map<Department>(updateDTO);
           
            _db.Departments.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();

        }
    }
}
