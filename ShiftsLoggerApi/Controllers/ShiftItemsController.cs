using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Models;

namespace ShiftsLoggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftItemsController : ControllerBase
    {
        private readonly ShiftContext _context;

        public ShiftItemsController(ShiftContext context)
        {
            _context = context;
        }

        // GET: api/ShiftItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftItem>>> GetShiftItems()
        {
            return await _context.ShiftItems.ToListAsync();
        }

        // GET: api/ShiftItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftItem>> GetShiftItem(int id)
        {
            var shiftItem = await _context.ShiftItems.FindAsync(id);

            if (shiftItem == null)
            {
                return NotFound();
            }

            return shiftItem;
        }

        // PUT: api/ShiftItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftItem(int id, ShiftItem shiftItem)
        {
            if (id != shiftItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShiftItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftItemDTO>> PostShiftItem(ShiftItemDTO shiftItemDTO)
        {

            var shiftItem = new ShiftItem
            {
                Name = shiftItemDTO.Name,
                StartShift = shiftItemDTO.StartShift,
                EndShift = shiftItemDTO.EndShift,
                Duration = shiftItemDTO.Duration
            };

            _context.ShiftItems.Add(shiftItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftItem), new { id = shiftItem.Id }, ItemToDTO(shiftItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftItem(int id)
        {
            Console.WriteLine($"Received DELETE request for ID: {id}");

            var shiftItem = await _context.ShiftItems.FindAsync(id);
            if (shiftItem == null)
            {
                Console.WriteLine($"Shift item with ID {id} not found.");
                return NotFound();
            }

            _context.ShiftItems.Remove(shiftItem);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Shift item with ID {id} deleted successfully.");
            return NoContent();
        }


        private bool ShiftItemExists(int id)
        {
            return _context.ShiftItems.Any(e => e.Id == id);
        }

        public static ShiftItemDTO ItemToDTO(ShiftItem shiftItem) =>
            new ShiftItemDTO
            {
                Name = shiftItem.Name,
                StartShift = shiftItem.StartShift,
                EndShift = shiftItem.EndShift,
                Duration = shiftItem.Duration
            };
    }
}
