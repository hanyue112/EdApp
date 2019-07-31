using EdAppAuctionBid.Models;
using Microsoft.EntityFrameworkCore;

namespace EdAppAuctionBid.DbContext
{
    public class BidContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<BidEntity> Bids { get; set; }

        public BidContext(DbContextOptions<BidContext> options) : base(options)
        {
        }
    }
}
