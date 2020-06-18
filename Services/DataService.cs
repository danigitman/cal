using apical.Helpers;
using apical.Interfaces;
using apical.Models.Authorization;
using apical.Models.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace apical.Services
{/// <inheritdoc />
 /// <summary>
 /// Data Service Response and stuff
 /// </summary>
    public class DataService : IDataService
    {
        private readonly HttpContext _httpContext;
        private readonly AppSettings _settings;



        /// <inheritdoc />
        /// <summary>
        /// context initializator
        /// </summary>
        /// <returns></returns>
        public HttpContext Context()
        {
            return _httpContext;
        }

        /// <inheritdoc />
        /// <summary>
        /// settings initializator
        /// </summary>
        /// <returns></returns>
        public AppSettings Setting()
        {
            return _settings;
        }

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="httpContextAccessor"></param>
        public DataService(IOptions<AppSettings> settings, IHttpContextAccessor httpContextAccessor)
        {
            _settings = settings.Value;
            _httpContext = httpContextAccessor.HttpContext;

        }

        //its better to putn auth requests to separate service
        public async Task<string> Authenticate()
        {
            var auth = new AuthRequestModel
            {
                UserName = _settings.Credentials.UserName,
                Password = _settings.Credentials.Password
            };

            AuthResponseModel authResponse;
            var json = JsonConvert.SerializeObject(auth);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{_settings.ApiUrls.BaseUrl}{_settings.ApiUrls.Authorization}";
            string apiResult = null;
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);

                apiResult = response.Content.ReadAsStringAsync().Result;
               
                authResponse = JsonConvert.DeserializeObject<AuthResponseModel>(apiResult);
            }
            return $"Bearer {authResponse.Token}";
        }

        public async Task<List<GetTransactionsClientReposnse>> GetTransactions(string token)
        {
            List<TransactionModel> transactionsList = new List<TransactionModel>();
            using (HttpClient httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                using (var response = await httpClient.GetAsync($"{_settings.ApiUrls.BaseUrl}{_settings.ApiUrls.GetTransactions}"))
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;



                    var results = JsonConvert.DeserializeObject<GetTransactionsResponseModel>(apiResponse);
                    transactionsList = results.Transactions;
                }
            }

            var cluentResponse = ResponseHelper.ConvertModelToProperResponse(transactionsList);

            return cluentResponse;
        }

        /// <summary>
        /// response data
        /// </summary>
        /// <param name="items"></param>
        /// <param name="fields"></param>
        /// <param name="totalpages"></param>
        /// <param name="totalitems"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public dynamic Response(dynamic items, List<string> fields, int totalpages = 1, int totalitems = 1, int currentPage = 1)
        {
            var jsonapi = new { version = "1.0" };
            var meta = new { totalpages, totalitems, currentPage, copyright = "theCopyright", authors = new[] { "daniel gitman" } };
            var links = new { self = _httpContext.Request.Path.ToUriComponent().ToLower() };
            var results = new List<dynamic>();
            foreach (var item in items)
            {
                var attributes = new Dictionary<string, dynamic>();
                foreach (var attr in fields)
                {
                    var field = item.GetType().GetProperty(attr, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (field == null) continue;
                    var value = field.GetValue(item, null);
                    attributes.Add(attr.ToLower(), value);
                }

                var fieldId = item.GetType().GetProperty("id", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var objectTypeName = item.GetType().Name;
                string id ="";
                var self = "";
                if (fieldId != null)
                {
                    id = item.Id;
                    self = _httpContext.Request.Host.Value + "/v1/" + objectTypeName + "/" + id;
                    //attributes.Remove("id");
                }
                var itemObj = new { type = objectTypeName, attributes, links = new { self = self.ToLower() } };
                results.Add(itemObj);
            }
            var jsonData = LowercaseJsonSerializer.SerializeObject(results);
            var data = JsonConvert.DeserializeObject(jsonData);

            var res = new { meta, data, jsonapi, links };
            return res;
        }

     

        /// <summary>
        /// serialize json with smallercase
        /// </summary>
        public class LowercaseJsonSerializer
        {
            private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                ContractResolver = new LowercaseContractResolver()
            };

            /// <summary>
            /// serialization of an object
            /// </summary>
            /// <param name="o"></param>
            /// <returns></returns>
            public static string SerializeObject(object o)
            {
                return JsonConvert.SerializeObject(o, Formatting.Indented, Settings);
            }

            /// <inheritdoc />
            /// <summary>
            /// lower case resolver
            /// </summary>
            public class LowercaseContractResolver : DefaultContractResolver
            {
                /// <inheritdoc />
                /// <summary>
                /// ResolvePropertyName
                /// </summary>
                /// <param name="propertyName"></param>
                /// <returns></returns>
                protected override string ResolvePropertyName(string propertyName)
                {
                    return propertyName.ToLower();
                }
            }
        }

    }

}
