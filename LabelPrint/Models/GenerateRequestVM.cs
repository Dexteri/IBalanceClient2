using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint.Models
{
    public class GenerateRequestVM
    {
        public int CodesNumber { get; set; }
        public string ConsignmentNumber { get; set; }
        public int CounterpartyId { get; set; }
        public int ProductId { get; set; }
    }
}
