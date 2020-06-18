using apical.Models.Transactions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apical.Helpers
{
    public static class ResponseHelper
    { 
        internal static List<GetTransactionsClientReposnse> ConvertModelToProperResponse(List<TransactionModel> response)
        {
            var convertedResult = new List<GetTransactionsClientReposnse>();
            foreach (var item in response)
            {
                if (!convertedResult.Any(x => x.MerchantName == item.MerchantName))
                {
                    convertedResult.Add(new GetTransactionsClientReposnse()
                    {
                        MerchantName = item.MerchantName,
                        TotalTransactions = response.Count(x => x.MerchantName == item.MerchantName)
                    });
                }
            }
            return convertedResult;
        }
    }
}
