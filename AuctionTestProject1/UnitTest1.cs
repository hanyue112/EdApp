using EdAppAuctionBid.Models;
using EdAppAuctionRepo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace AuctionTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private HttpClient _httpClient = new HttpClient();

        [TestMethod]
        public void TestGetItems1() //Get Item List
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/get").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Item> itemList = response.Content.ReadAsAsync<List<Item>>().Result;
            Assert.IsNotNull(itemList);
            Assert.IsTrue(itemList.Count > 0);
        }

        [TestMethod]
        public void TestGetWinner1() //Get the highest price of this item
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/GetWinner/2").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string result = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(result, "test1@test.com.au with Price of $61.33");
        }

        [TestMethod]
        public void TestPostBid1() //Bidding
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ItemId", "1"),
                new KeyValuePair<string, string>("Email", "89@1c.com"),
                new KeyValuePair<string, string>("Price", "663.22"),
                new KeyValuePair<string, string>("Comment", "Unit Testing")
            };

            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44322/Bid/Bid") { Content = new FormUrlEncodedContent(nvc) };

            HttpResponseMessage response = _httpClient.SendAsync(req).Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestListBiddings1() //Verify Bid record added, this operation is not possible from UI
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44322/Bid/GetBiddings/1").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<BidEntity> biddingList = response.Content.ReadAsAsync<List<BidEntity>>().Result;
            Assert.IsNotNull(biddingList);
            Assert.IsTrue(biddingList.Count > 0);
            int count = biddingList.Where(b => b.ItemId == 1).Where(b => b.Email == "89@1c.com").Where(b => b.Price == (decimal)663.22).Where(b => b.Comment == "Unit Testing").Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void TestGetWinner2() // SOLD item, result won't change
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/GetWinner/1").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string result = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(result, "1@1.com with Price of $15.22");
        }

        [TestMethod]
        public void TestPostBid2() //Bid to a PASSED item
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ItemId", "4"),
                new KeyValuePair<string, string>("Email", "89@1c.com"),
                new KeyValuePair<string, string>("Price", "663.22"),
                new KeyValuePair<string, string>("Comment", "Unit Testing")
            };

            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44322/Bid/Bid") { Content = new FormUrlEncodedContent(nvc) };

            HttpResponseMessage response = _httpClient.SendAsync(req).Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetWinner3() // PASSED item
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/GetWinner/4").Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string result = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(result, "PASSED");
        }

        [TestMethod]
        public void DataBase1() // PASSED item
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SqlExpress; Initial catalog=EdApp; Integrated security=true;"))
            {
                SqlCommand command = new SqlCommand("delete Items where Title = 'Unit Testing Item'", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();

                command = new SqlCommand("insert into Items values('Unit Testing Item','Unit Testing Cargo',null,'3000-01-01 00:00:00')", connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TestPostBid3() //Bid to a new item
        {
            int ItemID = 0;
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SqlExpress; Initial catalog=EdApp; Integrated security=true;"))
            {
                SqlCommand command = new SqlCommand("select itemid from items where Title = 'Unit Testing Item'", connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ItemID = (int)reader[0];
                }
                reader.Close();
            }

            Assert.AreNotEqual(0, ItemID);

            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ItemId", ItemID.ToString()),
                new KeyValuePair<string, string>("Email", "89@1c.com"),
                new KeyValuePair<string, string>("Price", "1663.22"),
                new KeyValuePair<string, string>("Comment", "Unit Testing")
            };

            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44322/Bid/Bid") { Content = new FormUrlEncodedContent(nvc) };

            HttpResponseMessage response = _httpClient.SendAsync(req).Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetWinner4() // new item, result shoud be same with TestPostBid3
        {
            int ItemID = 0;
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SqlExpress; Initial catalog=EdApp; Integrated security=true;"))
            {
                SqlCommand command = new SqlCommand("select itemid from items where Title = 'Unit Testing Item'", connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ItemID = (int)reader[0];
                }
                reader.Close();
            }

            Assert.AreNotEqual(0, ItemID);
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:44399/items/GetWinner/"+ ItemID).Result;
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            string result = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(result, "89@1c.com with Price of $1663.22");
        }
    }
}
