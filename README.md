# BittrexApi.NetCore
.Net Core library for accessing the [Bittrex Exchange](https://www.bittrex.com) api  
  
This library is available on NuGet for download: https://www.nuget.org/packages/BittrexApi.NetCore  
```
PM> Install-Package BittrexApi.NetCore
```

  
To trade, log into your Bittrex account and create an api key with trading permissions:  
Settings -> API Keys -> Create (with Read Info & Limit and Market Trading)  
** if you wish to use withdraw endpoint, you need to enable withdraws on your API key  
Store your API Key & Secret Key  
  
Initialization:  
Non-secured endpoints only:  
```
var bittrex = new BittrexClient();
```  
  
Secure & non-secure endpoints:  
```
var bittrex = new BittrexClient("api-key", "api-secret");
```  
or
```
create config file config.json
{
  "apiKey": "api-key",
  "apiSecret": "api-secret"
}
var bittrex = new BittrexClient("/path-to/config.json");
```

Using an endpoint:  
```  
var balances = await bittrex.GetBalancesAsync();
```  
or  
```
var balances = bittrex.GetBalances();
```

Non-secure endpoints:  
GetMarkets() | GetMarketsAsync() - Get the open and available trading markets at Bittrex  
GetCurrencies() | GetCurrenciesAsync() - Get all supported currencies at Bittrex  
GetTicker() | GetTickerAsync() - Get the current tick values for a market  
GetMarketSummaries() | GetMarketSummariesAsync() - Get the last 24 hour summary of all active markets  
GetMarketSummary() | GetMarketSummaryAsync() - Get the last 24 hour summary of a market  
GetOrderBook() | GetOrderBookAsync() - Get the orderbook for a market.  
GetMarketHistory() | GetMarketHistoryAsync() - Get the open and available trading markets at Bittrex  

Secure endpoints:  
GetBalance() | GetBalanceAsync() - Get current balances for an asset  
GetBalances() | GetBalancesAsync() - Get current balances for all assets  
GetDepositAddress() | GetDepositAddressAsync() - Get deposit address  
GetDeposits() | GetDepositsAsync() - Get deposit history  
GetOrder() | GetOrderAsync() - Get information for an order  
GetOrders() | GetOrdersAsync() - Get all current user order information  
GetOrderHistory() | GetOrderHistoryAsync() - Get order history for account  
GetWithdrawals() | GetWithdrawalsAsync() - Get withdrawal history  
WithdrawFunds() | WithdrawFundsAsync() - Withdraw funds from exchange  
PlaceOrder() | PlaceOrderAsync() - Place a new trade  
CancelOrder() | CancelOrderAsync() - Cancel a current open trade  
GetOpenOrders()  | GetOpenOrdersAsync() - Get all current user open orders  

ETH:  
0x3c8e741c0a2Cb4b8d5cBB1ead482CFDF87FDd66F  
BTC:  
1MGLPvTzxK9argeNRTHJ9EZ3WtGZV6nxit  
XLM:  
GA6JNJRSTBV54W3EGWDAWKPEGGD3QCXIGEHMQE2TUYXUKKTNKLYWEXVV  
