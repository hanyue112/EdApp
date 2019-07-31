using EdAppAuctionBid.Models;
using System.Collections.Generic;

namespace EdAppAuctionBid.Interfaces
{
    public interface IListBiddings
    {
        List<BidEntity> GetList(int ItemId);
    }
}