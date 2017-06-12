using LabelPrint.Models;
using LabelPrint.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint
{
    class ClientIbalance
    {
        static string path = "Settings//Url.txt";
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
