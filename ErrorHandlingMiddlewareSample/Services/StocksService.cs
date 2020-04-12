using System;
using System.Collections.Generic;
using System.Linq;
using ErrorHandlingMiddlewareSample.Exceptions;
using ErrorHandlingMiddlewareSample.Model;

namespace ErrorHandlingMiddlewareSample.Services
{
    public class StocksService : IStocksService
    {
        private static readonly Stock[] Stocks =
        {
            new Stock { StockId = Guid.NewGuid(), Ticker = "MSFT", Name = "Microsoft" },
            new Stock { StockId = Guid.NewGuid(), Ticker = "AAPL", Name = "Apple" },
            new Stock { StockId = Guid.NewGuid(), Ticker = "GOOG", Name = "Google" },
            new Stock { StockId = Guid.NewGuid(), Ticker = "FB", Name = "Facebook" }
        };

        public IEnumerable<Stock> GetAll()
        {
            return Stocks;
        }

        public Stock Get(Guid stockId)
        {
            var stock = Stocks.FirstOrDefault(s => s.StockId == stockId);
            if (stock == null)
                throw new NotFoundException($"Stock with ID {stockId} not found.");

            return stock;
        }
    }
}
