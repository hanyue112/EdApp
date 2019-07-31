using EdAppAuctionRepo.DbContext;
using EdAppAuctionRepo.Interfaces;
using EdAppAuctionRepo.Models;
using System.Collections.Generic;
using System.Linq;

namespace EdAppAuctionRepo.Implementations
{
    public class ListItems : IListItems
    {
        private readonly RepoContext _context;

        public ListItems(RepoContext context)
        {
            _context = context;
        }

        public List<Item> ListAllItem()
        {
            return _context.Items.ToList();
        }
    }
}