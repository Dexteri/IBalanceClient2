using LabelPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint
{
    class ClientIbalance
    {
        static string Url = "http://ibalance-001-site1.etempurl.com/api/client/";
        static HttpClient client = new HttpClient();

        static public List<ProductGenerationRequestVM> GetProducts()
        {
            List<ProductGenerationRequestVM> list = new List<ProductGenerationRequestVM>();
            var response = client.GetAsync(Url + "get-products", 0).Result;
            return response.Content.ReadAsAsync<List<ProductGenerationRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result;
        }
        static public List<CounterpartyGenerationRequestVM> GetCounterparty()
        {
            var response = client.GetAsync(Url + "get-counterparties", 0).Result;
            return response.Content.ReadAsAsync<List<CounterpartyGenerationRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result; ;
        }
        static public List<ConsignmentRequestVM> Generate(GenerateRequestVM generateVM)
        {
            var response = client.PostAsJsonAsync(Url + "generate-code", generateVM).Result;
            return response.Content.ReadAsAsync<List<ConsignmentRequestVM>>(new[] { new JsonMediaTypeFormatter() }).Result; ;
        }
    }
}
