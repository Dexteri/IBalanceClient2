using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint.Models
{
    [JsonObject("data")]
    public class JsonObjectHelper<T>
    {
        public string message;
        public T[] data;
        [JsonIgnore]
        public bool isSuccessfull { get { return message == null ? true : message.Equals("success"); } }
    }
}
