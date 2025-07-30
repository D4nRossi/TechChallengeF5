using cliente_service.Data;
using cliente_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteDbContext _db;

    public ClientesController(ClienteDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cliente cliente)
    {
        if (await _db.Clientes.AnyAsync(c => c.Cpf == cliente.Cpf))
            return BadRequest("CPF já cadastrado.");

        cliente.Id = Guid.NewGuid();
        cliente.DataCadastro = DateTime.UtcNow;
        _db.Clientes.Add(cliente);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    [HttpGet]
    [Authorize(Roles = "Gerente")]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _db.Clientes.ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        return cliente == null ? NotFound() : Ok(cliente);
    }
}
