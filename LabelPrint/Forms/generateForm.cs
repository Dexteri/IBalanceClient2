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
            _printManager = PrintManager.Instance();
            GetFromApi:
            try
            {
                products = ClientIbalance.GetProducts();
                counterparty = ClientIbalance.GetCounterparty();
            }
            catch (Exception ex)
            {
                if(MessageBox.Show(this, "Не могу подключиться к серверу.", "Что-то пошло не так!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
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
                counterpartyTemplateBindingSource.Add(item);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GenerateRequestVM vm = new GenerateRequestVM();
            vm.ProductId = int.Parse(lookUpEdit1.EditValue.ToString());
            vm.CounterpartyId = int.Parse(lookUpEdit2.EditValue.ToString());
            vm.CodesNumber = int.Parse(numericUpDown1.Value.ToString());
            vm.ConsignmentNumber = textBox1.Text;
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
        }

       /* private void print_btn_Click(object sender, EventArgs e)
        {
            //this._printManager.PrintCollection();
            //this._printManager.ShowPrintDialog();
        }

        private void print_prev_btn_Click(object sender, EventArgs e)
        {
            this._printManager.LoadRtfTemplate();
            if (codes != null && codes.Count > 0)
                this._printManager.ShowPrintPreview(codes.FirstOrDefault());
        }

       private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
       {
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());
        }
        private void lookUpEdit3_Popup(object sender, EventArgs e)
        {
            while (counterpartyTemplateBindingSource.Count > 0)
                counterpartyTemplateBindingSource.RemoveAt(0);

            foreach (var item in _printManager.LoadListTemplate())
            {
                counterpartyTemplateBindingSource.Add(item);
            }
        }*/
    }
}