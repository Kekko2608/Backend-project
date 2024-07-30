using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Prodotto> Prodotti { get; set; }
        public virtual DbSet<Ingrediente> Ingredienti { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Ordine> Ordini { get; set; }
        public virtual DbSet<ProdottoOrdinato> Prodottiordinati { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }


    }
}
