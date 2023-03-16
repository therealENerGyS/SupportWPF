using Microsoft.EntityFrameworkCore;
using SupportWPF.Models.Entities;

namespace SupportWPF.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\anton\source\repos\SupportWPF\SupportWPF\Contexts\local_db.mdf;Integrated Security=True;Connect Timeout=30";

        #region Constructors

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion

        #region Overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        #endregion

        public DbSet<AddressEntity> Addresses { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<OrderRowEntity> OrderRows { get; set; } = null!;
    }
}
