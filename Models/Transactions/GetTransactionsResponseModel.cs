using System;
using System.Collections.Generic;
using System.Text;

namespace apical.Models.Transactions
{
    public class GetTransactionsResponseModel
    {
        public List<TransactionModel> Transactions = new List<TransactionModel>();
    }
    public class TransactionModel
    {
        public string Id { get; set; }
        public string MerchantName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public long Amount { get; set; }
        public string Symbol { get; set; }
        public DateTime DebitDate { get; set; }
        public string MerchantAddress { get; set; }
        public long NumOfPayments { get; set; }
        public object TransTypeComment { get; set; }
        public string TransactionType { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
