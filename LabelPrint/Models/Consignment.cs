using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint.Models
{
    public class Consignment
    {
        //[JsonProperty("id")]
        //[JsonIgnore]
        //public int Id { get; set; }
        [JsonProperty("created_at")]
        public string DateString { get; set; }
        [JsonIgnore]
        public DateTime ConsignmentDate
        {
            get
            {
                return !string.IsNullOrEmpty(this.DateString) ?
                      Convert.ToDateTime(this.DateString.Replace(" -", ""))
                      :
                      DateTime.MinValue;
            }
        }
        [JsonProperty("product_type")]
        public string Category { get; set; }
        [JsonProperty("user_name")]
        public string CounterpartyName { get; set; }
        [JsonProperty("code")]
        public string SerialKey { get; set; }
        [JsonProperty("product_name")]
        public string Model { get; set; }
    }
}
