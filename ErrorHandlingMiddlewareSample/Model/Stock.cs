using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandlingMiddlewareSample.Model
{
    public class Stock
    {
        public Guid StockId { get; set; }

        public string Ticker { get; set; }

        public string Name { get; set; }
    }
}
