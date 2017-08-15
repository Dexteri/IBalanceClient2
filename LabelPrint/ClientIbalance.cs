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

        internal static List<Consignment> GetConsignments()
        {
            return GetObjects<Consignment>("api/order/get-codes");
        }

        static public List<ProductGenerationRequestVM> GetProducts()
        {
            Generate1();
            return GetObjects<ProductGenerationRequestVM>("api/order/get-products");
        }
        static public List<CounterpartyGenerationRequestVM> GetCounterparty()
        {
            return GetObjects<CounterpartyGenerationRequestVM>("api/order/get-contractors");
        }
        static public List<ConsignmentRequestVM> Generate(GenerateRequestVM generateVM)
        {
            generateVM = new GenerateRequestVM() { CodesNumber = 87, ConsignmentNumber = "21w", CounterpartyId = 658, ProductId = 0 };

            var response = client.PostAsJsonAsync(Url + "api/client/generate-code", generateVM).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            return response.Content.ReadAsAsync<List<ConsignmentRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result;
        }
        static public List<ConsignmentRequestVM> Generate1()
        {
            Data data = new Data() { productId = "123", userId = "111", quantity = "2222" };

            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("key", "Basic hxG7VJVlJZD4F4YZMhGsPNb0J6uxpOVut1LH3VL4Fy5JTIBGUOjxu3o3QB1dk08Q:yzF3WjxDV8UtgZyQSeNLMzbcNgbEF6QmMw99eIwCdC1s3nHhLwOPl2z5HcZaC9IK");
                var response = client.PostAsJsonAsync(Url + "api/order/create-order", data);
                if (!response.IsCompleted)
                    throw new Exception(response.Status.ToString());
                var temp = response.Result.Content.ReadAsAsync<object>(new[] { new JsonMediaTypeFormatter() }).Result;
            }
            return null;
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
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("key", "Basic hxG7VJVlJZD4F4YZMhGsPNb0J6uxpOVut1LH3VL4Fy5JTIBGUOjxu3o3QB1dk08Q:yzF3WjxDV8UtgZyQSeNLMzbcNgbEF6QmMw99eIwCdC1s3nHhLwOPl2z5HcZaC9IK");
                var response = client.GetAsync(Url + api, 0).Result;
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);
                var temp = response.Content.ReadAsAsync<JsonObjectHelper<T>>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (temp != null && temp.data != null)
                {
                    List<T> result = new List<T>();
                    result.AddRange(temp.data as T[]);
                    return result;
                }
            }
            return null;
        }
        public static bool Check()
        {
            try
            {
                var response = client.GetAsync(Url + "api/order/get-products", 0).Result;
                if (!response.IsSuccessStatusCode)
                    return false;
                response = client.GetAsync(Url + "api/order/get-contractors", 0).Result;
                if (!response.IsSuccessStatusCode)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
   public class Data
    {
        public string userId { get; set; }
        public string productId { get; set; }
        public string quantity { get; set; }
    }
}
