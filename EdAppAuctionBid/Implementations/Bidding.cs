using EdAppAuctionBid.DbContext;
using EdAppAuctionBid.Interfaces;
using EdAppAuctionBid.Models;
using System.Collections.Generic;
using System.Linq;

namespace EdAppAuctionBid.Implementations
{
    public class Bidding : IBidding, IListBiddings
    {
        private readonly BidContext _context;

        public Bidding(BidContext context)
        {
            _context = context;
        }

        public bool Bid(BidRequest bid)
        {
            _context.Bids.Add(new BidEntity(bid));
            _context.SaveChanges();
            return true;
        }

        public List<BidEntity> GetList(int ItemId)
        {
            return _context.Bids.Where(b => b.ItemId == ItemId).ToList();
        }
    }
}
