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
        static HttpClient client = new HttpClient();

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
            List<Consignment> list = new List<Consignment>();
            var response = client.GetAsync(Url + "api/client/get-consignments", 0).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            return response.Content.ReadAsAsync<List<Consignment>>(new[] { new JsonMediaTypeFormatter() }).Result;
        }

        static public List<ProductGenerationRequestVM> GetProducts()
        {
            List<ProductGenerationRequestVM> list = new List<ProductGenerationRequestVM>();
            var response = client.GetAsync(Url + "api/client/get-products", 0).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            return response.Content.ReadAsAsync<List<ProductGenerationRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result;
        }
        static public List<CounterpartyGenerationRequestVM> GetCounterparty()
        {
            var response = client.GetAsync(Url + "api/client/get-counterparties", 0).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            return response.Content.ReadAsAsync<List<CounterpartyGenerationRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result; ;
        }
        static public List<ConsignmentRequestVM> Generate(GenerateRequestVM generateVM)
        {
            var response = client.PostAsJsonAsync(Url + "api/client/generate-code", generateVM).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);
            return response.Content.ReadAsAsync<List<ConsignmentRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result;
        }
        static public bool DeleteConsignments(List<int> idList)
        {
            var response = client.PostAsJsonAsync(Url + "api/client/delete-consignments", idList).Result;
            if (!response.IsSuccessStatusCode)
                return true;
            return true;
        }
        public static bool Check()
        {
            try
            {
                var response = client.GetAsync(Url + "api/client/get-products", 0).Result;
                if (!response.IsSuccessStatusCode)
                    return false;
                response = client.GetAsync(Url + "api/client/get-counterparties", 0).Result;
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
}
