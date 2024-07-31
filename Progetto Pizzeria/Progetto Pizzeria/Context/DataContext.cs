
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Context
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<Prodotto> Prodotti { get; set; }
        public virtual DbSet<Ingrediente> Ingredienti { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Ordine> Ordini { get; set; }
        public virtual DbSet<ProdottoOrdinato> Prodottiordinati { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=Pizzeria;Integrated Security=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0797369540");
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0798BFEE9C");
            });

            modelBuilder.Entity<UserRole>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__UserRol__3214EC07D639C897");

                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
