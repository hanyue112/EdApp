using EdAppAuctionRepo.Models;
using Microsoft.EntityFrameworkCore;

//Run dotnet ef migrations add InitialCreate to scaffold a migration and create the initial set of tables for the model.
//Run dotnet ef database update to apply the new migration to the database.This command creates the database before applying migrations.

namespace EdAppAuctionRepo.DbContext
{
    public class RepoContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Item> Items { get; set; }

        public RepoContext(DbContextOptions<RepoContext> options) : base(options)
        {
        }
    }
}