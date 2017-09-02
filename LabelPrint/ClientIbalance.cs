using LabelPrint.Models;
using LabelPrint.Setup;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace LabelPrint
{
    class ClientIbalance
    {
        private static string url = String.Empty;
        static HttpClient client;
        private static string key = String.Empty;

        public static string Url
        {
            get
            {
                if (String.IsNullOrEmpty(url))
                {
                    try
                    {
                        url = DefaultSettings.Get(XmlNodeName.URL);
                    }
                    catch { }
                }
                return url;
            }

            set
            {
                url = value;
            }
        }
        public static string Key
        {
            get
            {
                if (String.IsNullOrEmpty(key))
                {
                    try
                    {
                        key = DefaultSettings.Get(XmlNodeName.KEY);
                    }
                    catch { }
                }
                return key;
            }

            set
            {
                key = value;
            }
        }

        internal static List<Consignment> GetConsignments()
        {
            return GetObjects<Consignment>("api/order/get-codes");
        }
        static public List<ProductGenerationRequestVM> GetProducts(object type)
        {
            return GetObjects<ProductGenerationRequestVM>("api/order/get-products", type);
        }
        static public List<CounterpartyGenerationRequestVM> GetCounterparty()
        {
            return GetObjects<CounterpartyGenerationRequestVM>("api/order/get-contractors");
        }
        static public List<ConsignmentRequestVM> Generate(GenerateRequestVM data)
        {
            List<ConsignmentRequestVM> result = new List<ConsignmentRequestVM>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("key", Key);
                var response = client.PostAsJsonAsync(Url + "api/order/create-order", data);
                var temp = response.Result.Content.ReadAsAsync<JsonObjectHelper<ConsignmentRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (temp != null && temp.data != null)
                {
                    result.AddRange(temp.data as ConsignmentRequestVM[]);
                    return result;
                }
            }
            return result;
        }
        static public bool DeleteConsignments(List<int> idList)
        {
            var response = client.PostAsJsonAsync(Url + "api/client/delete-consignments", idList).Result;
            if (!response.IsSuccessStatusCode)
                return true;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"/>
        /// <returns></returns>
        static public List<T> GetObjects<T>(string api)
        {
            List<T> result = new List<T>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("key", Key);
                var response = client.GetAsync(Url + api, 0).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
                dynamic temp = response.Content.ReadAsAsync<JsonObjectHelper<T>>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (temp != null && temp.data != null)
                {
                    result.AddRange(temp.data as T[]);
                    return result;
                }
            }
            return result;
        }
        static public List<T> GetObjects<T>(string api, object data)
        {
            List<T> result = new List<T>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("key", Key);
                var response = client.PostAsJsonAsync(Url + api, data).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
                var temp = response.Content.ReadAsAsync<JsonObjectHelper<T>>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (temp != null && temp.data != null)
                {
                    result.AddRange(temp.data as T[]);
                    return result;
                }
            }
            return result;
        }
        public static bool Check()
        {
            try
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("key", Key);
                    var response = client.GetAsync(Url + "api/order/get-codes", 0).Result;
                    if (!response.IsSuccessStatusCode)
                        throw new Exception(response.ReasonPhrase);
                    var temp = response.Content.ReadAsAsync<JsonObjectHelper<ProductGenerationRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    if (temp != null && temp.isSuccessfull)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
   public class Data
    {
        public string userId { get; set; }
        public string productId { get; set; }
        public string quantity { get; set; }
    }
}
