using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rusu_Denisa_Proiect.Models;

namespace Rusu_Denisa_Proiect.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Bouquet> Bouquets { get; set; }
        public DbSet<FlowerBouquet> FlowerBouquets { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}