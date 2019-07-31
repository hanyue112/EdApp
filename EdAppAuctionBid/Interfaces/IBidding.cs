using EdAppAuctionBid.Models;

namespace EdAppAuctionBid.Interfaces
{
    public interface IBidding
    {
        bool Bid(BidRequest bid);
    }
}