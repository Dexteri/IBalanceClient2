using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint.Models
{
    public class Consignment
    {
        public int Id { get; set; }
        public DateTime ConsignmentDate { get; set; }
        public string ConsignmentNumber { get; set; }
        public string CounterpartyName { get; set; }
        public string SerialKey { get; set; }
        public string Model { get; set; }
    }
}
