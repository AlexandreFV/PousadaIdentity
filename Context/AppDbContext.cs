using PousadaIdentity.Entities;
using Microsoft.EntityFrameworkCore;
namespace PousadaIdentity.Context;

public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : 
            base(options)
        {   
        }

        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Quarto> Quartos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        
    }   

