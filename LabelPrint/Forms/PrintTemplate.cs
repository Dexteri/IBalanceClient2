using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPrint
{
    public partial class PrintTemplate : Form
    {
        public PrintTemplate()
        {
            InitializeComponent();
            FillTemplateDefault();
        }
        private void FillTemplateDefault()
        {
            string text = "Model \n\r" + "ProductionDate \n\r" + "SerialKey \n\r";
            richEditControl1.Document.Text = text;
        }
    }
}
