namespace API.Controllers.Hr
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Dtos.Hr.Clients;
    using Application.Interfaces;
    using Application.Interfaces.Hr;
    using Application.Services.Auth;
    using Domain.Enties;
    using Domain.Enties.Hr;
    using Domain.Enties.Leaves;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IGenericService<Client,CreateUpdateClientDto> _ClientService;
        private readonly IClientService clientService;

        public ClientController(
            IGenericService<Client, CreateUpdateClientDto> ClientService,
            IClientService clientService)
        {
            _ClientService = ClientService;
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _ClientService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _ClientService.GetByIdAsync(id);
            if (result is null)
            {
                return NotFound("leaveRequestId not found");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateClient([FromBody] CreateUpdateClientDto client)
        {
            var result = await _ClientService.CreateAsync(client);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] CreateUpdateClientDto updateClientDto)
        {
            var result = await _ClientService.UpdateAsync(id, updateClientDto);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return StatusCode(result.StatusCode, result.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> SoftDeleteClient(int id)
        {
            var result = await _ClientService.SoftDeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}/undo")]
        public async Task<IActionResult> UnSoftDeleteClient(int id)
        {
            var result = await _ClientService.UndoSoftDeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("clientdetails/{id}")]
        public async Task<ActionResult<List<ClientDetailDto>>> GetClientProject(int id)
        {
            var result = await clientService.GetClientProjectAsync(id);
            if (result == null || !result.Any())
            {
                return NotFound($"No details found for departmentId: {id}");
            }
            return Ok(result);
        }
    }
}