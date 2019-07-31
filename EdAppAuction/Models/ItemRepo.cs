using System;

namespace EdAppAuctionRepo.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime EndBid { get; set; }
    }
}