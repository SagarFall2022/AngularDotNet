using Cards.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }

        //Dbset is the property used by EF core and it acts as a table
        public DbSet<Card> Cards { get; set; }
    }
}
