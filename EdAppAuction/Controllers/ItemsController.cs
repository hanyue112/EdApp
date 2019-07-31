using System.Collections.Generic;
using EdAppAuctionRepo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EdAppAuction.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IListItems _listItems;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IWinner _winner;

        public ItemsController(IListItems listItems, ILoggerFactory loggerFactory, IWinner winner)
        {
            _listItems = listItems;
            _loggerFactory = loggerFactory;
            _winner = winner;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //var logger = _loggerFactory.CreateLogger("itemsGet");
            //logger.LogInformation("Calling itemsGet");
            return Ok(_listItems.ListAllItem());
        }

        [Route("[action]/{itemId}")]
        [HttpGet]
        public ActionResult<string> GetWinner(int itemId)
        {
            return Ok(_winner.Calculate(itemId));
        }
    }
}