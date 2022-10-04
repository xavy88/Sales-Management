using AutoMapper;
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
    public class ClientAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<ClientAPIController> _logger;
        private readonly IClientRepository _dbClient;
        private readonly IMapper _mapper;

        public ClientAPIController(ILogger<ClientAPIController> logger, IClientRepository dbClient, IMapper mapper)
        {
            _logger = logger;
            _dbClient = dbClient;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetClients()
        {
            try
            {
                IEnumerable<Client> clientList = await _dbClient.GetAllAsync();
                _response.Result = _mapper.Map<List<ClientDTO>>(clientList);
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

        [HttpGet("{id:int}", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetClient(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var client = await _dbClient.GetAsync(x => x.Id == id);
                if (client == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ClientDTO>(client);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateClient([FromBody] ClientCreateDTO createDTO)
        {
            try
            {
                if (await _dbClient.GetAsync(x => x.CompanyName.ToLower() == createDTO.CompanyName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessage", "Client already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Error Creating Client!");
                    return BadRequest(createDTO);
                }
                Client client = _mapper.Map<Client>(createDTO);

                await _dbClient.CreateAsync(client);

                _response.Result = _mapper.Map<ClientDTO>(client);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("Getlient", new { id = client.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteClient")]
        public async Task<ActionResult<APIResponse>> DeleteClient(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var client = await _dbClient.GetAsync(x => x.Id == id);
                if (client == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Error Deleting Client!" + id);
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbClient.RemoveAsync(client);
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

        [HttpPut("{id:int}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateClient(int id, [FromBody] ClientUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Client model = _mapper.Map<Client>(updateDTO);

                await _dbClient.UpdateAsync(model);
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
