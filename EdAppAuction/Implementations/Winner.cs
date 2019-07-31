using EdAppAuctionRepo.DbContext;
using EdAppAuctionRepo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EdAppAuctionRepo.Implementations
{
    public class Winner : IWinner
    {
        private readonly RepoContext _context;

        public Winner(RepoContext context)
        {
            _context = context;
        }

        public string Calculate(int itemId)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"select Email,Price from Bids where Bidid = (select MAX(t.BidId) from (select BidId from Bids where Price = (select MAX(Price) from Bids inner join Items on Bids.ItemId=Items.ItemId where Bids.ItemId={itemId} and Items.EndBid > Bids.[TimeStamp])) as t)";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            return result.GetString(0) + " with Price of $" + result.GetDecimal(1);
                        }
                    }
                    else
                    {
                        return "PASSED";
                    }
                }
            }
            return string.Empty;
        }
    }
}
