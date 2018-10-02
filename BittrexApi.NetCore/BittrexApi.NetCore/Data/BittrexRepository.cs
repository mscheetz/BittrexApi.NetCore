using BittrexApi.NetCore.Core;
using BittrexApi.NetCore.Data.Interfaces;
using BittrexApi.NetCore.Entities;
using DateTimeHelpers;
using FileRepository;
using RESTApiAccess;
using RESTApiAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BittrexApi.NetCore.Data
{
    public class BittrexRepository : IBittrexRepository
    {
        private Security security;
        private IRESTRepository _restRepo;
        private DateTimeHelper _dtHelper;
        private ApiInformation _apiInfo = null;
        private Helper _helper;
        private string baseUrl;
        private string _version = "1.1";

        /// <summary>
        /// Constructor for non-signed endpoints
        /// </summary>
        public BittrexRepository()
        {
            LoadRepository();
        }

        /// <summary>
        /// Constructor for signed endpoints
        /// </summary>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiSecret">Api secret</param>
        public BittrexRepository(string apiKey, string apiSecret)
        {
            _apiInfo = new ApiInformation
            {
                apiKey = apiKey,
                apiSecret = apiSecret
            };
            LoadRepository();
        }

        /// <summary>
        /// Constructor for signed endpoints
        /// </summary>
        /// <param name="configPath">String of path to configuration file</param>
        public BittrexRepository(string configPath)
        {
            IFileRepository _fileRepo = new FileRepository.FileRepository();

            if (_fileRepo.FileExists(configPath))
            {
                _apiInfo = _fileRepo.GetDataFromFile<ApiInformation>(configPath);
                LoadRepository();
            }
            else
            {
                throw new Exception("Config file not found");
            }
        }

        /// <summary>
        /// Load repository
        /// </summary>
        private void LoadRepository()
        {
            security = new Security();
            _restRepo = new RESTRepository();
            _dtHelper = new DateTimeHelper();
            baseUrl = $"https://bittrex.com/api/v{_version}";
            _helper = new Helper();
        }

        /// <summary>
        /// Check if the Exchange Repository is ready for trading
        /// </summary>
        /// <returns>Boolean of validation</returns>
        public bool ValidateExchangeConfigured()
        {
            var ready = _apiInfo == null || string.IsNullOrEmpty(_apiInfo.apiKey) ? false : true;
            if (!ready)
                return false;

            return string.IsNullOrEmpty(_apiInfo.apiSecret) ? false : true;
        }

        #region Public

        /// <summary>
        /// Get the open and available trading markets at Bittrex
        /// </summary>
        /// <returns>Array of Market objects</returns>
        public async Task<Market[]> GetMarkets()
        {
            var endpoint = "/public/getmarkets";

            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.GetApiStream<Response<Market[]>>(url);

                return response.result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all supported currencies at Bittrex
        /// </summary>
        /// <returns>Array of Currency objects</returns>
        public async Task<Currency[]> GetCurrencies()
        {
            var endpoint = "/public/getcurrencies";

            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.GetApiStream<Response<Currency[]>>(url);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the current tick values for a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker object</returns>
        public async Task<Ticker> GetTicker(string pair)
        {
            var endpoint = "/public/getticker";

            var parameters = new Dictionary<string, object>();
            parameters.Add("market", pair);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            try
            {
                var response = await _restRepo.GetApiStream<Response<Ticker>>(url);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the last 24 hour summary of all active markets
        /// </summary>
        /// <returns>Array of MarketSummary objects</returns>
        public async Task<MarketSummary[]> GetMarketSummaries()
        {
            var endpoint = "/public/getmarketsummaries";
            
            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.GetApiStream<Response<MarketSummary[]>>(url);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the last 24 hour summary of a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>MarketSummary object</returns>
        public async Task<MarketSummary> GetMarketSummary(string pair)
        {
            var endpoint = "/public/getmarketsummary";

            var parameters = new Dictionary<string, object>();
            parameters.Add("market", pair);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            try
            {
                var response = await _restRepo.GetApiStream<Response<MarketSummary[]>>(url);

                return response.result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>OrderBook object</returns>
        public async Task<OrderBook> GetOrderBook(string pair)
        {
            return await OnGetOrderBook(pair);
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (BUY | SELL | BOTH)</param>
        /// <returns>OrderBook object</returns>
        public async Task<OrderBook> GetOrderBook(string pair, OrderType type)
        {
            if (type == OrderType.BOTH)
            {
                return await OnGetOrderBook(pair);
            }
            else
            {
                var response = await OnGetOrderBookBuySell(pair, type);

                var orderBook = new OrderBook();
                if (type == OrderType.BUY)
                {
                    orderBook.buy = response;
                }
                else if (type == OrderType.SELL)
                {
                    orderBook.sell = response;
                }

                return orderBook;
            }
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>OrderBook object</returns>
        private async Task<OrderBook> OnGetOrderBook(string pair)
        {
            var endpoint = "/public/getorderbook";

            var parameters = new Dictionary<string, object>();
            parameters.Add("market", pair);
            parameters.Add("type", OrderType.BOTH.ToString().ToLower());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            try
            {
                var response = await _restRepo.GetApiStream<Response<OrderBook>>(url);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return</param>
        /// <returns>Array of OrderInterval objects</returns>
        private async Task<OrderInterval[]> OnGetOrderBookBuySell(string pair, OrderType type)
        {
            var endpoint = "/public/getorderbook";

            var parameters = new Dictionary<string, object>();
            parameters.Add("market", pair);
            parameters.Add("type", type.ToString().ToLower());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            try
            {
                var response = await _restRepo.GetApiStream<Response<OrderInterval[]>>(url);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the latest trades that have occured for a specific market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (default = both)</param>
        /// <returns>Array of MarketHistory objects</returns>
        public async Task<MarketHistory[]> GetMarketHistory(string pair)
        {
            var endpoint = "/public/getmarkethistory";

            var parameters = new Dictionary<string, object>();
            parameters.Add("market", pair);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            try
            {
                var response = await _restRepo.GetApiStream<Response<MarketHistory[]>>(url);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public async Task<string> PlaceOrder(string pair, Side side, decimal quantity, decimal price)
        {
            var endpoint = side == Side.BUY 
                ? "/market/buylimit"
                : "/market/selllimit";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("market", pair);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());
            parameters.Add("quantity", quantity);
            parameters.Add("rate", price);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<Dictionary<string, string>>>(url, headers);

                return response.result["uuid"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Cancel an order.
        /// </summary>
        /// <param name="id">OpenOrder id</param>
        /// <returns>Boolean of cancel attempt</returns>
        public async Task<bool> CancelOrder(string id)
        {
            var endpoint = "/market/cancel";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("uuid", id);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<string>>(url, headers);

                return response.success;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get open orders.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Array of OpenOrder objects</returns>
        public async Task<OpenOrder[]> GetOpenOrders(string pair)
        {
            var endpoint = "/market/getopenorders";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("market", pair);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<OpenOrder[]>>(url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Market

        #region Account

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <returns>Array of Balance objects</returns>
        public async Task<Balance[]> GetBalances()
        {
            var endpoint = "/account/getbalances";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<Balance[]>>(url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get account balance of a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Balance object</returns>
        public async Task<Balance> GetBalance(string symbol)
        {
            var endpoint = "/account/getbalance";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("currency", symbol);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<Balance>>(url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get an address for a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>String of address</returns>
        public async Task<string> GetDepositAddress(string symbol)
        {
            var endpoint = "/account/getdepositaddress";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("currency", symbol);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<Dictionary<string, string>>>(url, headers);

                return response.result["Address"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get an order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns>OrderDetail object</returns>
        public async Task<OrderDetail> GetOrder(string id)
        {
            var endpoint = "/account/getorder";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            parameters.Add("uuid", id);
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream< Response<OrderDetail>> (url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get order history
        /// </summary>
        /// <param name="pair">Trading pair to find (optional)</param>
        /// <returns>Array of Order objects</returns>
        public async Task<Order[]> GetOrderHistory(string pair = "")
        {
            var endpoint = "/account/getorderhistory";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            if (pair != "")
            {
                parameters.Add("market", pair);
            }
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<Order[]>>(url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        public async Task<DepositWithdrawal[]> GetDeposits(string symbol = "")
        {
            var endpoint = "/account/getdeposithistory";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            if (symbol != "")
            {
                parameters.Add("currency", symbol);
            }
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<DepositWithdrawal[]>>(url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        public async Task<DepositWithdrawal[]> GetWithdrawals(string symbol = "")
        {
            var endpoint = "/account/getwithdrawalhistory";

            var parameters = new Dictionary<string, object>();
            parameters.Add("apikey", _apiInfo.apiKey);
            if (symbol != "")
            {
                parameters.Add("currency", symbol);
            }
            parameters.Add("nonce", _dtHelper.UTCtoUnixTimeMilliseconds());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(parameters);

            var headers = GetHeaders(url);

            try
            {
                var response = await _restRepo.GetApiStream<Response<DepositWithdrawal[]>>(url, headers);

                return response.result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Account

        #region Helpers

        /// <summary>
        /// Get headers for a secured request
        /// </summary>
        /// <param name="url">Url to access</param>
        /// <returns>Dictionary of headers</returns>
        private Dictionary<string, string> GetHeaders(string url)
        {
            var headers = new Dictionary<string, string>();
            headers.Add("apisign", GetSignature(url));

            return headers;
        }

        /// <summary>
        /// Get signature for endpoint
        /// </summary>
        /// <param name="message">Message to sign</param>
        /// <returns>String of signature</returns>
        private string GetSignature(string message)
        {
            return security.GetHMACSignature(message, _apiInfo.apiSecret);
        }

        #endregion Helpers
    }
}
