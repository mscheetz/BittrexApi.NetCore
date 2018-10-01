using BittrexApi.NetCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BittrexApi.NetCore.Data.Interfaces
{
    public interface IBittrexRepository
    {
        #region Public

        /// <summary>
        /// Get the open and available trading markets at Bittrex
        /// </summary>
        /// <returns>Array of Market objects</returns>
        Task<Market[]> GetMarkets();

        /// <summary>
        /// Get all supported currencies at Bittrex
        /// </summary>
        /// <returns>Array of Currency objects</returns>
        Task<Currency[]> GetCurrencies();

        /// <summary>
        /// Get the current tick values for a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Ticker object</returns>
        Task<Ticker> GetTicker(string pair);

        /// <summary>
        /// Get the last 24 hour summary of all active markets
        /// </summary>
        /// <returns>Array of MarketSummary objects</returns>
        Task<MarketSummary[]> GetMarketSummaries();

        /// <summary>
        /// Get the last 24 hour summary of a market
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>MarketSummary object</returns>
        Task<MarketSummary> GetMarketSummary(string pair);

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>OrderBook object</returns>
        Task<OrderBook> GetOrderBook(string pair);

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (BUY | SELL | BOTH)</param>
        /// <returns>OrderBook object</returns>
        Task<OrderBook> GetOrderBook(string pair, OrderType type);

        /// <summary>
        /// Get the orderbook for a market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (default = both)</param>
        /// <returns>Array of MarketHistory objects</returns>
        Task<MarketHistory[]> GetMarketHistory(string pair);

        #endregion Public

        #region Market
        #endregion Market

        #region Account
        #endregion Account
    }
}
