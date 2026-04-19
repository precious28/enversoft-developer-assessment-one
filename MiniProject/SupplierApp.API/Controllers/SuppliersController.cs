using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupplierApp.API.Models;
using SupplierApp.API.Services;

namespace SupplierApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SuppliersController(ISupplierService service) => _service = service;

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");
            return Ok(await _service.SearchByNameAsync(name));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _service.GetByIdAsync(id);
            return supplier is null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name) || string.IsNullOrWhiteSpace(supplier.Telephone))
                return BadRequest("Name and Telephone are required.");
            var newId = await _service.AddAsync(supplier);
            return CreatedAtAction(nameof(GetById), new { id = newId }, supplier);
        }
    }
}
