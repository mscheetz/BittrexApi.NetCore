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
        private Dictionary<int, string> errorCodes;
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
        public BittrexRepository(string apiKey, string apiSecret, string version = "")
        {
            _apiInfo = new ApiInformation
            {
                apiKey = apiKey,
                apiSecret = apiSecret
            };
            LoadRepository(version);
        }

        /// <summary>
        /// Constructor for signed endpoints
        /// </summary>
        /// <param name="configPath">String of path to configuration file</param>
        public BittrexRepository(string configPath, string version = "")
        {
            IFileRepository _fileRepo = new FileRepository.FileRepository();

            if (_fileRepo.FileExists(configPath))
            {
                _apiInfo = _fileRepo.GetDataFromFile<ApiInformation>(configPath);
                LoadRepository(version);
            }
            else
            {
                throw new Exception("Config file not found");
            }
        }

        /// <summary>
        /// Load repository
        /// </summary>
        private void LoadRepository(string version = "")
        {
            security = new Security();
            _restRepo = new RESTRepository();
            _dtHelper = new DateTimeHelper();
            if (version != "")
                _version = version;

            baseUrl = $"https://bittrex.com/api/v{_version}";
            _helper = new Helper();
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

            var paramters = new Dictionary<string, object>();
            paramters.Add("market", pair);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(paramters);

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

            var paramters = new Dictionary<string, object>();
            paramters.Add("market", pair);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(paramters);

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

            var paramters = new Dictionary<string, object>();
            paramters.Add("market", pair);
            paramters.Add("type", OrderType.BOTH.ToString().ToLower());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(paramters);

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

            var paramters = new Dictionary<string, object>();
            paramters.Add("market", pair);
            paramters.Add("type", type.ToString().ToLower());

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(paramters);

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
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (default = both)</param>
        /// <returns>Array of MarketHistory objects</returns>
        public async Task<MarketHistory[]> GetMarketHistory(string pair)
        {
            var endpoint = "/public/getmarkethistory";

            var paramters = new Dictionary<string, object>();
            paramters.Add("market", pair);

            var url = baseUrl + endpoint + "?" + _helper.StringifyDictionary(paramters);

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
        #endregion Market

        #region Account
        #endregion Account
    }
}
