using PousadaIdentity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PousadaIdentity.Context;

public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : 
            base(options)
        {   

        }


         public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        

    }   

