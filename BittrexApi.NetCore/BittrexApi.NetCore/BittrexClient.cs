using BittrexApi.NetCore.Data;
using BittrexApi.NetCore.Data.Interfaces;
using BittrexApi.NetCore.Entities;
using System.Threading.Tasks;

namespace BittrexApi.NetCore
{
    public class BittrexClient
    {
        private IBittrexRepository _repository;

        /// <summary>
        /// Constructor - no authentication
        /// </summary>
        public BittrexClient()
        {
            _repository = new BittrexRepository();
        }

        /// <summary>
        /// Constructor - with authentication
        /// </summary>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiSecret">Api secret</param>
        public BittrexClient(string apiKey, string apiSecret)
        {
            _repository = new BittrexRepository(apiKey, apiSecret);
        }

        /// <summary>
        /// Constructor - with authentication
        /// </summary>
        /// <param name="configPath">Path to config file</param>
        public BittrexClient(string configPath)
        {
            _repository = new BittrexRepository(configPath);
        }

        /// <summary>
        /// Check if the Exchange Repository is ready for trading
        /// </summary>
        /// <returns>Boolean of validation</returns>
        public bool ValidateExchangeConfigured()
        {
            return _repository.ValidateExchangeConfigured();
        }

        #region Public

        /// <summary>
        /// Get the open and available trading markets at Bittrex
        /// </summary>
        /// <returns>Array of Market objects</returns>
        public Market[] GetMarkets()
        {
            return _repository.GetMarkets().Result;
        }

        /// <summary>
        /// Get all supported currencies at Bittrex
        /// </summary>
        /// <returns>Array of Currency objects</returns>
        public Currency[] GetCurrencies()
        {
            return _repository.GetCurrencies().Result;
        }

        /// <summary>
        /// Get the current tick values for a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker object</returns>
        public Ticker GetTicker(string pair)
        {
            return _repository.GetTicker(pair).Result;
        }

        /// <summary>
        /// Get the last 24 hour summary of all active markets
        /// </summary>
        /// <returns>Array of MarketSummary objects</returns>
        public MarketSummary[] GetMarketSummaries()
        {
            return _repository.GetMarketSummaries().Result;
        }

        /// <summary>
        /// Get the last 24 hour summary of a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>MarketSummary object</returns>
        public MarketSummary GetMarketSummary(string pair)
        {
            return _repository.GetMarketSummary(pair).Result;
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>OrderBook object</returns>
        public OrderBook GetOrderBook(string pair)
        {
            return _repository.GetOrderBook(pair).Result;
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (BUY | SELL | BOTH)</param>
        /// <returns>OrderBook object</returns>
        public OrderBook GetOrderBook(string pair, OrderType type)
        {
            return _repository.GetOrderBook(pair, type).Result;
        }

        /// <summary>
        /// Get the latest trades that have occured for a specific market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (default = both)</param>
        /// <returns>Array of MarketHistory objects</returns>
        public MarketHistory[] GetMarketHistory(string pair)
        {
            return _repository.GetMarketHistory(pair).Result;
        }

        /// <summary>
        /// Get the open and available trading markets at Bittrex
        /// </summary>
        /// <returns>Array of Market objects</returns>
        public async Task<Market[]> GetMarketsAsync()
        {
            return await _repository.GetMarkets();
        }

        /// <summary>
        /// Get all supported currencies at Bittrex
        /// </summary>
        /// <returns>Array of Currency objects</returns>
        public async Task<Currency[]> GetCurrenciesAsync()
        {
            return await _repository.GetCurrencies();
        }

        /// <summary>
        /// Get the current tick values for a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker object</returns>
        public async Task<Ticker> GetTickerAsync(string pair)
        {
            return await _repository.GetTicker(pair);
        }

        /// <summary>
        /// Get the last 24 hour summary of all active markets
        /// </summary>
        /// <returns>Array of MarketSummary objects</returns>
        public async Task<MarketSummary[]> GetMarketSummariesAsync()
        {
            return await _repository.GetMarketSummaries();
        }

        /// <summary>
        /// Get the last 24 hour summary of a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>MarketSummary object</returns>
        public async Task<MarketSummary> GetMarketSummaryAsync(string pair)
        {
            return await _repository.GetMarketSummary(pair);
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>OrderBook object</returns>
        public async Task<OrderBook> GetOrderBookAsync(string pair)
        {
            return await _repository.GetOrderBook(pair);
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (BUY | SELL | BOTH)</param>
        /// <returns>OrderBook object</returns>
        public async Task<OrderBook> GetOrderBookAsync(string pair, OrderType type)
        {
            return await _repository.GetOrderBook(pair, type);
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (default = both)</param>
        /// <returns>Array of MarketHistory objects</returns>
        public async Task<MarketHistory[]> GetMarketHistoryAsync(string pair)
        {
            return await _repository.GetMarketHistory(pair);
        }

        #endregion Public

        #region Market

        /// <summary>
        /// Place a limit order.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="side">Side of order</param>
        /// <param name="quantity">Quantity to trade</param>
        /// <param name="price">Price of trade</param>
        /// <returns>String of order id</returns>
        public string PlaceOrder(string pair, Side side, decimal quantity, decimal price)
        {
            return _repository.PlaceOrder(pair, side, quantity, price).Result;
        }

        /// <summary>
        /// Cancel an order.
        /// </summary>
        /// <param name="id">OpenOrder id</param>
        /// <returns>Boolean of cancel attempt</returns>
        public bool CancelOrder(string id)
        {
            return _repository.CancelOrder(id).Result;
        }

        /// <summary>
        /// Get open orders.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Array of OpenOrder objects</returns>
        public OpenOrder[] GetOpenOrders(string pair)
        {
            return _repository.GetOpenOrders(pair).Result;
        }

        /// <summary>
        /// Place a limit order.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="side">Side of order</param>
        /// <param name="quantity">Quantity to trade</param>
        /// <param name="price">Price of trade</param>
        /// <returns>String of order id</returns>
        public async Task<string> PlaceOrderAsync(string pair, Side side, decimal quantity, decimal price)
        {
            return await _repository.PlaceOrder(pair, side, quantity, price);
        }

        /// <summary>
        /// Cancel an order.
        /// </summary>
        /// <param name="id">OpenOrder id</param>
        /// <returns>Boolean of cancel attempt</returns>
        public async Task<bool> CancelOrderAsync(string id)
        {
            return await _repository.CancelOrder(id);
        }

        /// <summary>
        /// Get open orders.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Array of OpenOrder objects</returns>
        public async Task<OpenOrder[]> GetOpenOrdersAsync(string pair)
        {
            return await _repository.GetOpenOrders(pair);
        }

        #endregion Market

        #region Account

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <returns>Array of Balance objects</returns>
        public Balance[] GetBalances()
        {
            return _repository.GetBalances().Result;
        }

        /// <summary>
        /// Get account balance of a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Balance object</returns>
        public Balance GetBalance(string symbol)
        {
            return _repository.GetBalance(symbol).Result;
        }

        /// <summary>
        /// Get an address for a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>String of address</returns>
        public string GetDepositAddress(string symbol)
        {
            return _repository.GetDepositAddress(symbol).Result;
        }

        /// <summary>
        /// Get an order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns>OrderDetail object</returns>
        public OrderDetail GetOrder(string id)
        {
            return _repository.GetOrder(id).Result;
        }

        /// <summary>
        /// Get order history
        /// </summary>
        /// <param name="pair">Trading pair to find (optional)</param>
        /// <returns>Array of Order objects</returns>
        public Order[] GetOrderHistory(string pair = "")
        {
            return _repository.GetOrderHistory(pair).Result;
        }

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        public DepositWithdrawal[] GetDeposits(string symbol = "")
        {
            return _repository.GetDeposits(symbol).Result;
        }

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        public DepositWithdrawal[] GetWithdrawals(string symbol = "")
        {
            return _repository.GetWithdrawals(symbol).Result;
        }

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <returns>Array of Balance objects</returns>
        public async Task<Balance[]> GetBalancesAsync()
        {
            return await _repository.GetBalances();
        }

        /// <summary>
        /// Get account balance of a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Balance object</returns>
        public async Task<Balance> GetBalanceAsync(string symbol)
        {
            return await _repository.GetBalance(symbol);
        }

        /// <summary>
        /// Get an address for a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>String of address</returns>
        public async Task<string> GetDepositAddressAsync(string symbol)
        {
            return await _repository.GetDepositAddress(symbol);
        }

        /// <summary>
        /// Get an order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns>OrderDetail object</returns>
        public async Task<OrderDetail> GetOrderAsync(string id)
        {
            return await _repository.GetOrder(id);
        }

        /// <summary>
        /// Get order history
        /// </summary>
        /// <param name="pair">Trading pair to find (optional)</param>
        /// <returns>Array of Order objects</returns>
        public async Task<Order[]> GetOrderHistoryAsync(string pair = "")
        {
            return await _repository.GetOrderHistory(pair);
        }

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        public async Task<DepositWithdrawal[]> GetDepositsAsync(string symbol = "")
        {
            return await _repository.GetDeposits(symbol);
        }

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        public async Task<DepositWithdrawal[]> GetWithdrawalsAsync(string symbol = "")
        {
            return await _repository.GetWithdrawals(symbol);
        }

        #endregion Account
    }
}
