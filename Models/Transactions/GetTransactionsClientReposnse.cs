using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apical.Models.Transactions
{
    public class GetTransactionsClientReposnse
    {
        public int TotalTransactions { get; set; }
        public string MerchantName { get; set; }
    }
}
