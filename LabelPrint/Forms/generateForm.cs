using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LabelPrint.Models;

namespace LabelPrint
{
    public partial class GeneratorForm : DevExpress.XtraEditors.XtraForm
    {
        private PrintManager _printManager;
        private List<ConsignmentRequestVM> codes;

        List<ProductGenerationRequestVM> products = new List<ProductGenerationRequestVM>();
        List<CounterpartyGenerationRequestVM> counterparty = new List<CounterpartyGenerationRequestVM>();
        List<ConsignmentRequestVM> consignment = new List<ConsignmentRequestVM>();

        public GeneratorForm()
        {
            InitializeComponent();
            products = ClientIbalance.GetProducts();
            counterparty = ClientIbalance.GetCounterparty();
            FillLookUp();
        }


        private void FillLookUp()
        {
            foreach (var item in products)
                productGenerationRequestVMBindingSource.Add(item);
            foreach (var item in counterparty)
                counterpartyGenerationRequestVMBindingSource.Add(item);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GenerateRequestVM vm = new GenerateRequestVM();
            vm.ProductId = int.Parse(lookUpEdit1.EditValue.ToString());
            vm.CounterpartyId = int.Parse(lookUpEdit2.EditValue.ToString());
            vm.CodesNumber = int.Parse(numericUpDown1.Value.ToString());
            vm.ConsignmentNumber = textBox1.Text;
            codes = ClientIbalance.Generate(vm);
            this._printManager.SetCodes(codes);
        }

        private void print_btn_Click(object sender, EventArgs e)
        {
            this._printManager.PrintCollection();
        }

        private void print_prev_btn_Click(object sender, EventArgs e)
        {
            if (codes != null && codes.Count > 0)
                this._printManager.ShowPrintPreview(codes.FirstOrDefault());
        }
    }
}