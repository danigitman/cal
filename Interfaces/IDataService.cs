using apical.Models.Transactions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace apical.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// appsettings provider
        /// </summary>
        /// <returns></returns>

        AppSettings Setting();
        /// <summary>
        /// httpcontext accessor
        /// </summary>
        /// <returns></returns>

        HttpContext Context();
        Task<string> Authenticate();

        /// <summary>
        /// response builder
        /// </summary> 
        /// <param name="totalpages"></param>
        /// <param name="totalitems"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>

        dynamic Response(dynamic items, List<string> fields, int totalpages = 1, int totalitems = 1, int currentPage = 1);
 
        Task<List<GetTransactionsClientReposnse>> GetTransactions(string token);
    }
}
