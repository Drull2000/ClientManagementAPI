using AutoMapper;
using ClientManagementAPI.DTOs;
using ClientManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMapper _mapper;

    private static readonly List<Client> _clients = new()
    {
        new Client { Id = 1, FullName = "Іван Петренко", Email = "ivan@example.com", Phone = "+380501112233", RegistrationDate = DateTime.Now.AddDays(-30) },
        new Client { Id = 2, FullName = "Олена Коваленко", Email = "olena@example.com", Phone = "+380672223344", RegistrationDate = DateTime.Now.AddDays(-15) },
        new Client { Id = 3, FullName = "Максим Сидоренко", Email = "maksym@example.com", Phone = "+380933334455", RegistrationDate = DateTime.Now.AddDays(-5) }
    };

    public ClientsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClientReadDto>> GetClients()
    {
        var clientDtos = _mapper.Map<IEnumerable<ClientReadDto>>(_clients);
        return Ok(clientDtos);
    }

    // POST: api/clients
    [HttpPost]
    public ActionResult<ClientReadDto> CreateClient([FromBody] ClientCreateDto clientCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var clientModel = _mapper.Map<Client>(clientCreateDto);

        clientModel.Id = _clients.Max(c => c.Id) + 1;
        clientModel.RegistrationDate = DateTime.Now;

        _clients.Add(clientModel);

        var clientReadDto = _mapper.Map<ClientReadDto>(clientModel);

        return CreatedAtAction(nameof(GetClients), new { id = clientReadDto.Id }, clientReadDto);
    }
}