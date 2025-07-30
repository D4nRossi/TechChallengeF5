using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using menu_service.Data;
using menu_service.Models;

namespace menu_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly MenuDbContext _db;

        public MenuController(MenuDbContext db)
        {
            _db = db;
        }

        // GET: /api/menu?name=pizza&category=lanches
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? category)
        {
            var query = _db.MenuItems.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(i => i.Name.Contains(name));
            if (!string.IsNullOrEmpty(category))
                query = query.Where(i => i.Category == category);

            var items = await query.ToListAsync();
            return Ok(items);
        }

        // GET: /api/menu/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _db.MenuItems.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [Authorize(Roles = "Gerente")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MenuItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            item.Id = Guid.NewGuid();
            _db.MenuItems.Add(item);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [Authorize(Roles = "Gerente")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MenuItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = await _db.MenuItems.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Name = item.Name;
            existing.Description = item.Description;
            existing.Price = item.Price;
            existing.IsAvailable = item.IsAvailable;
            existing.Category = item.Category;
            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        [Authorize(Roles = "Gerente")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _db.MenuItems.FindAsync(id);
            if (existing == null) return NotFound();
            _db.MenuItems.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
