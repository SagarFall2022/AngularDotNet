using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext=cardsDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards=await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get a single card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null) { 
                return Ok(card); 
            }
            return NotFound("Card not found");
            
        }

        //Add a card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();

            await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id },card);
        }


        //Updated a card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null) {
                existingCard.CardholderName = card.CardholderName;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CardNumber = card.CardNumber;
                existingCard.CVC= card.CVC;
                await cardsDbContext.SaveChangesAsync();

                return Ok(existingCard);
            }

            return NotFound("Card Not Found");


        }

        //Delete a card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardsDbContext.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }

            return NotFound("Card Not Found");


        }


    }
}
