using BittrexApi.NetCore.Data;
using BittrexApi.NetCore.Data.Interfaces;
using BittrexApi.NetCore.Entities;
using FileRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BittrexApi.NetCore.Tests
{
    public class BittrexRepoTests : IDisposable
    {
        private ApiInformation _exchangeApi = null;
        private IBittrexRepository _repo;
        private string configPath = "config.json";
        private string apiKey = string.Empty;
        private string apiSecret = string.Empty;

        /// <summary>
        /// Constructor for tests
        /// </summary>
        public BittrexRepoTests()
        {
            IFileRepository _fileRepo = new FileRepository.FileRepository();
            if (_fileRepo.FileExists(configPath))
            {
                _exchangeApi = _fileRepo.GetDataFromFile<ApiInformation>(configPath);
            }
            if (_exchangeApi != null || !string.IsNullOrEmpty(apiKey))
            {
                _repo = new BittrexRepository(_exchangeApi.apiKey, _exchangeApi.apiSecret);
            }
            else
            {
                _repo = new BittrexRepository();
            }
        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetMarketsTest()
        {
            var markets = _repo.GetMarkets().Result;

            Assert.NotNull(markets);
        }

        [Fact]
        public void GetCurrenciesTest()
        {
            var currencies = _repo.GetCurrencies().Result;

            Assert.NotNull(currencies);
        }

        [Fact]
        public void GetTickerTest()
        {
            var pair = "BTC-XLM";

            var ticker = _repo.GetTicker(pair).Result;

            Assert.NotNull(ticker);
        }

        [Fact]
        public void GetMarketSummariesTest()
        {
            var marketSummaries = _repo.GetMarketSummaries().Result;

            Assert.NotNull(marketSummaries);
        }

        [Fact]
        public void GetMarketSummaryTest()
        {
            var pair = "BTC-XLM";

            var marketSummary = _repo.GetMarketSummary(pair).Result;

            Assert.NotNull(marketSummary);
        }

        [Fact]
        public void GetOrderBookBothTest()
        {
            var pair = "BTC-XLM";

            var orderBook = _repo.GetOrderBook(pair).Result;

            Assert.NotNull(orderBook);
        }

        [Fact]
        public void GetOrderBookBuysTest()
        {
            var pair = "BTC-XLM";
            var type = OrderType.BUY;

            var orderBook = _repo.GetOrderBook(pair, type).Result;

            Assert.NotNull(orderBook);
        }

        [Fact]
        public void GetOrderBookSellsTest()
        {
            var pair = "BTC-XLM";
            var type = OrderType.SELL;

            var orderBook = _repo.GetOrderBook(pair, type).Result;

            Assert.NotNull(orderBook);
        }

        [Fact]
        public void GetMarketHistoryTest()
        {
            var pair = "BTC-XLM";

            var marketHistory = _repo.GetMarketHistory(pair).Result;

            Assert.NotNull(marketHistory);
        }

        [Fact]
        public void GetBalancesTest()
        {
            var balances = _repo.GetBalances();

            Assert.NotNull(balances);
        }
    }
}
