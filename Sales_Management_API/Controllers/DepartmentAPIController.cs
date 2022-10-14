using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales_Management_API.Data;
using Sales_Management_API.Model;
using Sales_Management_API.Model.DTO;
using Sales_Management_API.Repository.IRepository;
using System.Net;

namespace Sales_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<DepartmentAPIController> _logger;
        private readonly IDepartmentRepository _dbDepartment;
        private readonly IMapper _mapper;

        public DepartmentAPIController(ILogger<DepartmentAPIController> logger, IDepartmentRepository dbDepartment, IMapper mapper)
        {
            _logger = logger;
            _dbDepartment = dbDepartment;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetDepartments()
        {
            try
            {
                IEnumerable<Department> departmentList = await _dbDepartment.GetAllAsync();
                _response.Result = _mapper.Map<List<DepartmentDTO>>(departmentList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDepartment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var department = await _dbDepartment.GetAsync(x => x.Id == id);
                if (department == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<DepartmentDTO>(department);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;

        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateDeparment([FromBody] DepartmentCreateDTO createDTO)
        {
            try
            {
                if (await _dbDepartment.GetAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessage", "Department already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Error Creating Department!");
                    return BadRequest(createDTO);
                }
                Department department = _mapper.Map<Department>(createDTO);

                await _dbDepartment.CreateAsync(department);

                _response.Result = _mapper.Map<DepartmentDTO>(department);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDepartment", new { id = department.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteDepartment")]
        public async Task<ActionResult<APIResponse>> DeleteDepartment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var department = await _dbDepartment.GetAsync(x => x.Id == id);
                if (department == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Error Deleting Department!");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbDepartment.RemoveAsync(department);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDepartment(int id, [FromBody] DepartmentUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Department model = _mapper.Map<Department>(updateDTO);

                await _dbDepartment.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;

        }

    }
}
