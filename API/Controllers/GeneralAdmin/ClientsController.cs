namespace API.Controllers.GeneralAdmin
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Application.Interfaces.GenericInterfaces;
  using Application.Services.Auth;
  using Application.Services.GenericServices;
  using Domain.Dtos.Account;
  using Domain.Dtos.GeneralAdmin;
  using Domain.Enties;
  using Domain.Enties.Leaves;
  using Domain.Enties.TimeSheets;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
  [Route("api/[controller]")]
  public class ClientController : ControllerBase
  {
    private readonly IGenericService<Client, ClientDto> _ClientService;
    private readonly IClientService clientService;

    public ClientController(
        IGenericService<Client, ClientDto> ClientService,
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
    public async Task<IActionResult> CreateClient([FromBody] ClientDto ClientDto)
    {
      var result = await _ClientService.CreateAsync(ClientDto);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto updateClientDto)
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
    public async Task<IActionResult> SoftDeleteClient(int id)
    {
      var result = await _ClientService.SoftDelete(id);
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
    [Route("ClientProjectDetails/{id}")]
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