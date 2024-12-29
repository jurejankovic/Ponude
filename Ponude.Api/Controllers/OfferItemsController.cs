using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ponude.Api.Data;
using Ponude.Api.Models;

namespace Ponude.Api.Controllers
{
    [ApiController]
    [Route("api/offers/{offerId}/items")]
    public class OfferItemsController : Controller
    {
        private readonly PonudeDbContext _context;

        public OfferItemsController(PonudeDbContext context)
        {
            _context = context;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetOfferItem(int offerId, int itemId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if (offer == null) return NotFound();

            var offerItem = await _context.OfferItems.FirstOrDefaultAsync(i => i.Id == itemId);
            if (offerItem == null) return NotFound();

            return Ok(offerItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfferItem(int offerId, [FromBody] OfferItem offerItem)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer == null) return NotFound();

            offer.Items.Add(offerItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOfferItem), new { offerId, itemId = offerItem.Id }, offerItem);
        }

        [HttpPut]
        [Route("{itemId}/Edit")]
        public async Task<IActionResult> EditItem(int offerId, int itemId, [FromBody] OfferItem updatedItem)
        {
            var offer = await _context.Offers.FindAsync(offerId);

            if (offer == null) return NotFound();

            var offerItem = offer.Items.FirstOrDefault(i => i.Id == itemId);
            if (offerItem == null) return NotFound();

            // Update the offer item with the new values
            offerItem.Price = updatedItem.Price;
            // Other properties to update
            
            // updating an item updates the offer date
            offer.Date = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOfferItem(int offerId, [FromBody] OfferItem updatedOffer)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if (offer == null) return NotFound();

            // updating an item updates the offer date
            offer.Date = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfferItem(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null) return NotFound();

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
