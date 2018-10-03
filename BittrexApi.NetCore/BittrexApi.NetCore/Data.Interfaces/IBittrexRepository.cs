using BittrexApi.NetCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BittrexApi.NetCore.Data.Interfaces
{
    public interface IBittrexRepository
    {
        /// <summary>
        /// Check if the Exchange Repository is ready for trading
        /// </summary>
        /// <returns>Boolean of validation</returns>
        bool ValidateExchangeConfigured();

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
        /// Get the latest trades that have occured for a specific market.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <param name="type">Type of orderbook to return (default = both)</param>
        /// <returns>Array of MarketHistory objects</returns>
        Task<MarketHistory[]> GetMarketHistory(string pair);

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
        Task<string> PlaceOrder(string pair, Side side, decimal quantity, decimal price);

        /// <summary>
        /// Cancel an order.
        /// </summary>
        /// <param name="id">OpenOrder id</param>
        /// <returns>Boolean of cancel attempt</returns>
        Task<bool> CancelOrder(string id);

        /// <summary>
        /// Get open orders.
        /// </summary>
        /// <param name="pair">Trading pair</param>
        /// <returns>Array of OpenOrder objects</returns>
        Task<OpenOrder[]> GetOpenOrders(string pair);

        #endregion Market

        #region Account

        /// <summary>
        /// Get account balances
        /// </summary>
        /// <returns>Array of Balance objects</returns>
        Task<Balance[]> GetBalances();

        /// <summary>
        /// Get account balance of a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Balance object</returns>
        Task<Balance> GetBalance(string symbol);

        /// <summary>
        /// Get an address for a currency
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>String of address</returns>
        Task<string> GetDepositAddress(string symbol);

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Address to send to</param>
        /// <returns>String of withdraw id</returns>
        Task<string> WithdrawFunds(string symbol, decimal quantity, string address);

        /// <summary>
        /// Withdraw funds from exchange
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Address to send to</param>
        /// <param name="memo">memo/message/tag/paymentid option (optional)</param>
        /// <returns>String of withdraw id</returns>
        Task<string> WithdrawFunds(string symbol, decimal quantity, string address, string memo);

        /// <summary>
        /// Get an order
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns>OrderDetail object</returns>
        Task<OrderDetail> GetOrder(string id);

        /// <summary>
        /// Get order history
        /// </summary>
        /// <param name="pair">Trading pair to find (optional)</param>
        /// <returns>Array of Order objects</returns>
        Task<Order[]> GetOrderHistory(string pair = "");

        /// <summary>
        /// Get deposit history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        Task<DepositWithdrawal[]> GetDeposits(string symbol = "");

        /// <summary>
        /// Get withdrawal history
        /// </summary>
        /// <param name="symbol">Symbol to find (optional)</param>
        /// <returns>Array of DepositWithdrawal objects</returns>
        Task<DepositWithdrawal[]> GetWithdrawals(string symbol = "");

        #endregion Account
    }
}
