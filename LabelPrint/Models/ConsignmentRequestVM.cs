using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint.Models
{
    public class ConsignmentRequestVM
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("product")]
        public string Product { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("created_at")]
        public string Date { get; set; }

        public DateTime ConsignmentDate
        {
            get
            {
                return !string.IsNullOrEmpty(this.Date) ?
                      Convert.ToDateTime(this.Date.Replace(" -", ""))
                      :
                      DateTime.MinValue;
            }
        }
    }
}
