﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint.Models
{
    public class ProductGenerationRequestVM
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ProductionDate { get; set; }
    }
}
