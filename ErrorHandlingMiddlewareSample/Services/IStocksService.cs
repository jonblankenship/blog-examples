using System;
using System.Collections.Generic;
using ErrorHandlingMiddlewareSample.Exceptions;
using ErrorHandlingMiddlewareSample.Model;

namespace ErrorHandlingMiddlewareSample.Services
{
    public interface IStocksService
    {
        public IEnumerable<Stock> GetAll();

        /// <summary>
        /// Returns the <see cref="Stock"/> with the requested <see cref="stockId"/>
        /// </summary>
        /// <param name="stockId">The ID of the <see cref="Stock"/> to return</param>
        /// <returns>The <see cref="Stock"/> with the requested <see cref="stockId"/></returns>
        /// <exception cref="NotFoundException"></exception>
        public Stock Get(Guid stockId);
    }
}
