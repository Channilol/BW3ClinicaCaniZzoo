using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClinicaCaniZzoo.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Hospitalizations> Hospitalizations { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animals>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .Property(e => e.Tipologia)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .Property(e => e.ColoreMantello)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .Property(e => e.Microchip)
                .IsUnicode(false);

            modelBuilder.Entity<Hospitalizations>()
                .Property(e => e.StatoRicovero)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.NomeProdotto)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.TipoProdotto)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.UsiProdotto)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Armadietto)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Cassetto)
                .IsUnicode(false);

            modelBuilder.Entity<Suppliers>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Suppliers>()
                .Property(e => e.Indirizzo)
                .IsUnicode(false);

            modelBuilder.Entity<Suppliers>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Cognome)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.CodiceFiscale)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Ruolo)
                .IsUnicode(false);

            modelBuilder.Entity<Visits>()
                .Property(e => e.Prezzo)
                .HasPrecision(10, 2);
        }
    }
}
