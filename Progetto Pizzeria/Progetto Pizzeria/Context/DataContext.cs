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
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=Pizzeria;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Roles");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Users");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_UserRoles");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            modelBuilder.Entity<Ordine>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Ordini");

                entity.HasMany(o => o.ProdottiOrdinati)
                    .WithOne(po => po.Ordine)
                    .HasForeignKey(po => po.OrdineId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProdottoOrdinato>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ProdottiOrdinati");

                entity.HasOne(po => po.Prodotto)
                    .WithMany() 
                    .HasForeignKey(po => po.ProdottoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(po => po.Ordine)
                    .WithMany(o => o.ProdottiOrdinati)
                    .HasForeignKey(po => po.OrdineId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
