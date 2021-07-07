namespace testing.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Promotional> Promotionals { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.Details)
                .WithOptional(e => e.Bill)
                .HasForeignKey(e => e.DOderID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Foods)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CID);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Gmail)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.Details)
                .WithOptional(e => e.Food)
                .HasForeignKey(e => e.DFoodID);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Accounts)
                .WithOptional(e => e.Position1)
                .HasForeignKey(e => e.Position);
        }
    }
}
