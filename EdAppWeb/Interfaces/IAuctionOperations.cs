using EdAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdAppWeb.Interfaces
{
    public interface IAuctionOperations
    {
        List<Item> ListAllItems();
        string CalculateWinner(int itemId);
        List<Item> Render();
    }
}