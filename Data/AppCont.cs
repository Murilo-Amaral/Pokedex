using Login01.Models;
using Microsoft.EntityFrameworkCore;

namespace Login01.Data
{
    public class AppCont : DbContext
    {
        public AppCont(DbContextOptions<AppCont> options) : base(options)
        {

        }

        public DbSet<Pokemon> Pokemons { get; set; }
    }
}
