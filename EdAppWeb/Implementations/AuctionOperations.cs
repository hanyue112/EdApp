using EdAppWeb.Interfaces;
using EdAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EdAppWeb.Implementations
{
    public class AuctionOperations : IAuctionOperations
    {
        private readonly HttpClient _httpClient;

        public AuctionOperations(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Item> Render()
        {
            List<Item> item = ListAllItems();
            foreach (Item i in item)
            {
                i.Winner = CalculateWinner(i.ItemId);
            }
            return item;
        }

        public string CalculateWinner(int itemId)
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/GetWinner/" + itemId).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new InvalidOperationException($"Unable acquire item winner, {response.StatusCode}");
            }
        }

        public List<Item> ListAllItems()
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/get").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Item>>().Result;
            }
            else
            {
                throw new InvalidOperationException($"Unable acquire item list, {response.StatusCode}");
            }
        }
    }
}
