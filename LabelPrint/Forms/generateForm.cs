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
using System.Drawing.Printing;
using LabelPrint.Setup;

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
            _printManager = new PrintManager();
            FillPrinters();
            GetFromApi:
            try
            {
                products = ClientIbalance.GetProducts();
                counterparty = ClientIbalance.GetCounterparty();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(this, ex.Message + "\r\nДля проверки соедениния перейдите в настроки.", "Что-то пошло не так!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    goto GetFromApi;
                }
            }
            FillLookUp();

            this.lookUpEdit3.EditValue = _printManager.templates
                .FirstOrDefault(x => x.Name.Equals(DefaultSettings.Get(XmlNodeName.LAST_SELECTED_TEMPLATE)))?.Name;
        }

        private void FillPrinters()
        {
            List<String> printers = new List<String>();
            foreach (String printer in PrinterSettings.InstalledPrinters)
                printers.Add(printer);
            lookUpEdit4.Properties.DataSource = printers;
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
            if (lookUpEdit1.EditValue == null)
            {
                MessageBox.Show("Продукт не выбран!");
                return;
            }
            vm.ProductId = int.Parse(lookUpEdit1.EditValue.ToString());
            if (lookUpEdit2.EditValue == null)
            {
                MessageBox.Show("Агент не выбран!");
                return;
            }
            vm.CounterpartyId = int.Parse(lookUpEdit2.EditValue.ToString());
            if (int.Parse(numericUpDown1.Value.ToString()) < 1)
            {
                MessageBox.Show("Количество должно быть больше 1!");
                return;
            }
            vm.CodesNumber = int.Parse(numericUpDown1.Value.ToString());
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Партия не выбрана!");
                return;
            }
            vm.ConsignmentNumber = textBox1.Text;
            List<ConsignmentRequestVM> codes = null;
            GetFromApi:
            try
            {
                codes = ClientIbalance.Generate(vm);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(this, ex.Message + "\r\nДля проверки соедениния перейдите в настроки.", "Что-то пошло не так!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    goto GetFromApi;
                }
            }
            consignmentRequestVMBindingSource.Clear();
            foreach (var item in codes)
                consignmentRequestVMBindingSource.Add(item);
        }

        private void printButton2_Click(object sender, EventArgs e)
        {
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach (int i in list)
            {
                consignments.Add((ConsignmentRequestVM)consignmentRequestVMBindingSource.List[i]);
            }
            if (lookUpEdit3.EditValue == null)
            {
                MessageBox.Show("Шаблон не выбран! Если их нет, то создайте тх в конструкторе.");
                return;
            }
            else if (consignments.Count < 1)
            {
                MessageBox.Show("Выберите коды!");
                return;
            }
            else if (lookUpEdit4.EditValue == null)
            {
                MessageBox.Show("Выберите принтер!");
                return;
            }
            this._printManager.PrintCollection(consignments, lookUpEdit4.EditValue.ToString());
        }

        private void previewButton3_Click(object sender, EventArgs e)
        {
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach (int i in list)
            {
                consignments.Add((ConsignmentRequestVM)consignmentRequestVMBindingSource.List[i]);
            }
            if (lookUpEdit3.EditValue == null)
            {
                MessageBox.Show("Шаблон не выбран! Если их нет, то создайте тх в конструкторе.");
                return;
            }
            else if (consignments.Count < 1)
            {
                MessageBox.Show("Выберите коды!");
                return;
            }
            this._printManager.ShowPrintPreview(consignments);
        }
        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());

            DefaultSettings.Set(XmlNodeName.LAST_SELECTED_TEMPLATE, lookUpEdit3.EditValue.ToString());
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