using BittrexApi.NetCore.Data;
using BittrexApi.NetCore.Data.Interfaces;
using BittrexApi.NetCore.Entities;
using FileRepository;
using System;
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

        #region Public

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

        #endregion

        #region Market

        [Fact]
        public void PlaceAndGetOrderAndGetOpenAndCancelOrderTest()
        {
            var pair = "BTC-TRX";
            var quantity = 100.0M;
            var price = 0.00009999M;
            var side = Side.SELL;

            var id = _repo.PlaceOrder(pair, side, quantity, price).Result;

            Assert.NotNull(id);

            var order = _repo.GetOrder(id).Result;

            Assert.NotNull(order);

            var orders = _repo.GetOpenOrders(pair).Result;

            Assert.NotNull(orders);

            var status = _repo.CancelOrder(id).Result;

            Assert.True(status);
        }

        #endregion

        #region Account 

        [Fact]
        public void GetBalancesTest()
        {
            var balances = _repo.GetBalances().Result;

            Assert.NotNull(balances);
        }

        [Fact]
        public void GetBalanceTest()
        {
            var symbol = "TRX";

            var balance = _repo.GetBalance(symbol).Result;

            Assert.NotNull(balance);
        }

        [Fact]
        public void GetDepositAddressTest()
        {
            var symbol = "XLM";

            var address = _repo.GetDepositAddress(symbol).Result;

            Assert.NotNull(address);
        }

        [Fact]
        public void WithdrawFundsTest()
        {
            var symbol = "XLM";
            var quantity = 99.99M;
            var address = "GAHK7EEG2WWHVKDNT4CEQFZGKF2LGDSW2IVM4S5DP42RBW3K6BTODB4A";
            var memo = "1046303265";

            var withdrawId = _repo.Withdraw(symbol, quantity, address, memo).Result;

            Assert.NotNull(withdrawId);
        }

        [Fact]
        public void GetOrderHistoryTest()
        {
            var pair = "BTC-TRX";

            var orders = _repo.GetOrderHistory(pair).Result;

            Assert.NotNull(orders);
        }

        [Fact]
        public void GetDepositHistoryTest()
        {
            var symbol = "ZCL";

            var deposits = _repo.GetDeposits(symbol).Result;

            Assert.NotNull(deposits);
        }

        [Fact]
        public void GetWithdrawalHistoryTest()
        {
            var symbol = "ZCL";

            var withdrawals = _repo.GetWithdrawals(symbol).Result;

            Assert.NotNull(withdrawals);
        }

        #endregion
    }
}
