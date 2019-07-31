namespace EdAppAuctionBid.Models
{
    public class BidRequest
    {
        public int ItemId { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
    }
}
