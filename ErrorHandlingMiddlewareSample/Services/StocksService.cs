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

        /// <summary>
        /// Returns all <see cref="Stock"/>s
        /// </summary>
        /// <returns>An enumerable of <see cref="Stock"/>s</returns>
        public IEnumerable<Stock> GetAllStocks()
        {
            return Stocks;
        }

        /// <summary>
        /// Returns the <see cref="Stock"/> with the requested <see cref="stockId"/>
        /// </summary>
        /// <param name="stockId">The ID of the <see cref="Stock"/> to return</param>
        /// <returns>The <see cref="Stock"/> with the requested <see cref="stockId"/></returns>
        /// <exception cref="NotFoundException"></exception>
        public Stock GetStock(Guid stockId)
        {
            throw new ApplicationException("Test ApplicationException");
            var stock = Stocks.FirstOrDefault(s => s.StockId == stockId);
            if (stock == null)
                throw new NotFoundException($"Stock with ID {stockId} not found.");

            return stock;
        }
    }
}
