using System;
using System.Linq;
using ErrorHandlingMiddlewareSample.Exceptions;
using ErrorHandlingMiddlewareSample.Model;
using ErrorHandlingMiddlewareSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlingMiddlewareSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stocksService;

        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var stocks = _stocksService.GetAll();
            return new OkObjectResult(stocks);
        }

        [HttpGet]
        [Route("{stockId}")]
        public IActionResult GetById(Guid stockId)
        {
            var stock = _stocksService.Get(stockId);
            return new OkObjectResult(stock);
        }
    }
}
