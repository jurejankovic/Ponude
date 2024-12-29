using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ponude.Api.Data;
using Ponude.Api.Models;

namespace Ponude.Api.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OffersController : ControllerBase
    {
        private readonly PonudeDbContext _context;

        public OffersController(PonudeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffersPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            var offers = await _context.Offers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffer(int id)
        {
            var offer = await _context.Offers
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (offer == null) return NotFound();

            return Ok(offer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOffer), new { id = offer.Id }, offer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(int id, [FromBody] Offer updatedOffer)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null) return NotFound();

            offer.Date = updatedOffer.Date;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null) return NotFound();

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
