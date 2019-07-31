using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EdAppAuctionBid.Models
{
    public class BidEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BidId { get; set; }
        public int ItemId { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
        public DateTime? TimeStamp { get; set; } = DateTime.Now;

        public BidEntity()
        {

        }

        public BidEntity(BidRequest bidRequest)
        {
            ItemId = bidRequest.ItemId;
            Email = bidRequest.Email;
            Price = bidRequest.Price;
            Comment = bidRequest.Comment;
        }
    }
}