using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pedido_service;
using pedido_service.Data;
using pedido_service.Events;
using pedido_service.Models;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly PedidoDbContext _db;

    public PedidoController(PedidoDbContext db)
    {
        _db = db;
    }


    [HttpPost]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> CriarPedido([FromBody] Pedido pedido)
    {
        pedido.Id = Guid.NewGuid();
        pedido.DataCriacao = DateTime.UtcNow;
        pedido.Status = "Pendente";
        _db.Pedidos.Add(pedido);
        await _db.SaveChangesAsync();

        // Mapeia para o evento
        var evento = new PedidoCriadoEvent
        {
            PedidoId = pedido.Id,
            ClienteId = pedido.ClienteId, // Substitua por lógica real!
            ItensResumo = string.Join(", ", pedido.Itens.Select(i => $"{i.Quantidade}x {i.Nome}")),
            DataPedido = pedido.DataCriacao
        };

        RabbitMqPublisher.PublicarPedidoCriado(evento);

        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}/cancelar")]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> CancelarPedido(Guid id, [FromBody] CancelamentoDTO cancelamento)
    {
        var pedido = await _db.Pedidos.FindAsync(id);
        if (pedido == null) return NotFound();
        if (pedido.Status != "Pendente") return BadRequest("Não é possível cancelar após aceite/preparo.");
        pedido.Status = "Cancelado";
        pedido.JustificativaCancelamento = cancelamento.Justificativa;
        await _db.SaveChangesAsync();
        return Ok(pedido);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pedido = await _db.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id);
        return pedido == null ? NotFound() : Ok(pedido);
    }

    [HttpGet("pendentes")]
    [Authorize(Roles = "Gerente,Atendente")]
    public async Task<IActionResult> GetPendentes()
    {
        var pendentes = await _db.Pedidos.Include(p => p.Itens)
            .Where(p => p.Status == "Pendente")
            .ToListAsync();
        return Ok(pendentes);
    }

    [HttpPut("{id}/aprovar")]
    [Authorize(Roles = "Gerente,Atendente")]
    public async Task<IActionResult> AprovarPedido(Guid id)
    {
        var pedido = await _db.Pedidos.FindAsync(id);
        if (pedido == null) return NotFound();
        pedido.Status = "Aprovado";
        await _db.SaveChangesAsync();
        return Ok(pedido);
    }

    [HttpPut("{id}/rejeitar")]
    [Authorize(Roles = "Gerente,Atendente")]
    public async Task<IActionResult> RejeitarPedido(Guid id)
    {
        var pedido = await _db.Pedidos.FindAsync(id);
        if (pedido == null) return NotFound();
        pedido.Status = "Rejeitado";
        await _db.SaveChangesAsync();
        return Ok(pedido);
    }
}
