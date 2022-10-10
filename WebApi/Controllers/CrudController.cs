using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    public class CrudController : Controller
    {
        CrudDbContext _context;
        public CrudController(CrudDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/api/cruds")]
        public async Task<IActionResult> GetCruds()
        {
            var cruds = await _context.Cruds.ToListAsync();
            return Ok(cruds);
        }
        [HttpGet]
        [Route("/api/cruds/{id}")]
        public async Task<IActionResult> GetCrud(Guid id)
        {
            var crud = await _context.Cruds.SingleOrDefaultAsync(x => x.Id == id);
            if (crud == null)
                return NotFound();
            else
                return Ok(crud);
        }
        [HttpPost]
        [Route("/api/cruds")]
        public async Task<IActionResult> AddCrud([FromBody] Crud crud)
        {
            crud.Id = Guid.NewGuid();
            await _context.Cruds.AddAsync(crud);
            await _context.SaveChangesAsync();
            return Ok(crud);
        }
        [HttpPut]
        [Route("/api/cruds/{id}")]
        public async Task<IActionResult> UpdateCrud([FromRoute] Guid id, [FromBody] Crud newCrud)
        {
            var crud = await _context.Cruds.SingleOrDefaultAsync(x => x.Id == id);

            if (crud == null)
                return NotFound();
            crud.Name = newCrud.Name;
            crud.Phone = newCrud.Phone;
            crud.Department = newCrud.Department;
            crud.Salary = newCrud.Salary;
            crud.Email = newCrud.Email;
            await _context.SaveChangesAsync();
            return Ok(crud);
        }
        [HttpDelete]
        [Route("/api/cruds/{id}")]
        public async Task<IActionResult> DeleteCrud([FromRoute] Guid id)
        {
            var crud = await _context.Cruds.FindAsync(id);
            if (crud == null)
                return NotFound();
            _context.Cruds.Remove(crud);
            await _context.SaveChangesAsync();
            return Ok(crud);
        }
    }
}
