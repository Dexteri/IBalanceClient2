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
        List<ProductGenerationRequestVM> products = new List<ProductGenerationRequestVM>();
        List<CounterpartyGenerationRequestVM> counterparty = new List<CounterpartyGenerationRequestVM>();
        List<ConsignmentRequestVM> consignment = new List<ConsignmentRequestVM>();
        List<TemplateVM> templates = new List<TemplateVM>();
        public GeneratorForm()
        {
            InitializeComponent();
            products = ClientIbalance.GetProducts();
            counterparty = ClientIbalance.GetCounterparty();
            _printManager = PrintManager.Instance();
            GetFromApi:
            try
            {
                products = ClientIbalance.GetProducts();
                counterparty = ClientIbalance.GetCounterparty();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(this, "Не могу подключиться к серверу.", "Что-то пошло не так!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    goto GetFromApi;
                }
            }
            FillLookUp();
        }

        private void FillLookUp()
        {
            foreach (var item in products)
                productGenerationRequestVMBindingSource.Add(item);
            foreach (var item in counterparty)
                counterpartyGenerationRequestVMBindingSource.Add(item);
            foreach (var item in _printManager.templates)
                templateVMBindingSource.Add(item);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GenerateRequestVM vm = new GenerateRequestVM();
            vm.ProductId = int.Parse(lookUpEdit1.EditValue.ToString());
            vm.CounterpartyId = int.Parse(lookUpEdit2.EditValue.ToString());
            vm.CodesNumber = int.Parse(numericUpDown1.Value.ToString());
            vm.ConsignmentNumber = textBox1.Text;
            List<ConsignmentRequestVM> codes = ClientIbalance.Generate(vm);
            GetFromApi:
                try
                {
                    codes = ClientIbalance.Generate(vm);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(this, "Не могу подключиться к серверу.", "Что-то пошло не так!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        goto GetFromApi;
                    }
                }
            foreach (var item in codes)
                consignmentRequestVMBindingSource.Add(item);
        }

        private void printButton2_Click(object sender, EventArgs e)
        {
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach(int i in list)
            {
                consignments.Add((ConsignmentRequestVM)consignmentRequestVMBindingSource.List[i]);
            }
            this._printManager.PrintCollection(consignments);
        }

        private void previewButton3_Click(object sender, EventArgs e)
        {
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach (int i in list)
            {
                consignments.Add((ConsignmentRequestVM)consignmentRequestVMBindingSource.List[i]);
            }
            this._printManager.ShowPrintPreview(consignments);
        }
        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());
        }
        private void lookUpEdit3_Popup(object sender, EventArgs e)
        {
            while (templateVMBindingSource.Count > 0)
                templateVMBindingSource.RemoveAt(0);

            foreach (var item in _printManager.templates)
            {
                templateVMBindingSource.Add(item);
            }
        }
    }
}