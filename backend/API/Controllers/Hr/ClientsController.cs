namespace API.Controllers.Hr
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Dtos.Hr.Clients;
    using Application.Dtos.Hr.Teams;
    using Application.Interfaces;
    using Application.Interfaces.Hr;
    using Application.Services.Auth;
    using Domain.Constants;
    using Domain.Enties;
    using Domain.Enties.Hr;
    using Domain.Enties.Leaves;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IGenericService<Client, CreateUpdateClientDto> _ClientService;
        private readonly IGenericService<Client, ClientDto> ClientList;
        private readonly IClientService clientService;

        public ClientController(
            IGenericService<Client, CreateUpdateClientDto> ClientService,
            IClientService clientService,
            IGenericService<Client,ClientDto> ClientList)
        {
            _ClientService = ClientService;
            this.clientService = clientService;
            this.ClientList = ClientList;
        }

        [HttpGet]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await ClientList.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await clientService.GetClientDetailsAsync(id);
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
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        public async Task<IActionResult> CreateClient([FromBody] CreateUpdateClientDto client)
        {
            var result = await _ClientService.CreateAsync(client);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
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

        [HttpDelete("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        public async Task<IActionResult> SoftDeleteClient(int id)
        {
            var result = await _ClientService.SoftDeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}