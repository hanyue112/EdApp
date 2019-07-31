using EdAppAuctionBid.Interfaces;
using EdAppAuctionBid.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EdAppAuctionBid.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidding _iBidding;
        private readonly IListBiddings _listBiddings;

        public BidController(IBidding iBidding, IListBiddings listBiddings)
        {
            _iBidding = iBidding;
            _listBiddings = listBiddings;
        }

        [Route("[action]")]
        [HttpPost]
        [EnableCors]
        public ActionResult Bid([FromForm]BidRequest bid)
        {
            return Ok(_iBidding.Bid(bid));
        }

        [Route("[action]/{itemId}")]
        [HttpGet]
        public ActionResult GetBiddings(int itemId)
        {
           return Ok( _listBiddings.GetList(itemId));
        }
    }
}