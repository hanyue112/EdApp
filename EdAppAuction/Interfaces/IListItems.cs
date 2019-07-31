using EdAppAuctionRepo.Models;
using System.Collections.Generic;

namespace EdAppAuctionRepo.Interfaces
{
    public interface IListItems
    {
        List<Item> ListAllItem();
    }
}