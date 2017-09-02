using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LabelPrint.Models;
using System.Drawing.Printing;
using LabelPrint.Setup;
using System.Net.Http;
using Newtonsoft.Json;

namespace LabelPrint
{
    public partial class GeneratorForm : DevExpress.XtraEditors.XtraForm
    {
        #region members
        private PrintManager _printManager;
        List<ProductGenerationRequestVM> products = new List<ProductGenerationRequestVM>();
        List<CounterpartyGenerationRequestVM> counterparty = new List<CounterpartyGenerationRequestVM>();
        List<ConsignmentRequestVM> consignment = new List<ConsignmentRequestVM>();
        List<TemplateVM> templates = new List<TemplateVM>();
        #endregion

        #region constructor
        public GeneratorForm()
        {
            InitializeComponent();
            _printManager = new PrintManager();
            FillPrintersAndTemplates();
            FillLookUp();
        }
        #endregion

        #region fill date
        private void ConnectToServer()
        {
            bool conn = true;
            while (conn)
            {
                try
                {
                    counterparty = ClientIbalance.GetCounterparty();
                    conn = false;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(this, ex.Message + "\r\nДля проверки соедениния перейдите в настроки.", "Что-то пошло не так!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        conn = true;
                    }
                    else
                    {
                        conn = false;
                    }
                }
            }
        }

        private void FillPrintersAndTemplates()
        {
            List<String> printers = new List<String>();
            foreach (String printer in PrinterSettings.InstalledPrinters)
                printers.Add(printer);
            lookUpEdit4.Properties.DataSource = printers;

            this.lookUpEdit3.EditValue = _printManager.templates
                .FirstOrDefault(x => x.Name.Equals(DefaultSettings.Get(XmlNodeName.LAST_SELECTED_TEMPLATE)))?.Name;

        }

        private void FillLookUp()
        {
            lookUpEdit5.Properties.DataSource = new List<string>() { CategoryName.Product, CategoryName.Offer };
            ConnectToServer();
            foreach (var item in counterparty)
                counterpartyGenerationRequestVMBindingSource.Add(item);
            foreach (var item in _printManager.templates)
                templateVMBindingSource.Add(item);
        }
        #endregion

        #region events
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GenerateRequestVM vm = new GenerateRequestVM();
            if (lookUpEdit1.EditValue == null)
            {
                MessageBox.Show("Продукт не выбран!");
                return;
            }
            vm.ProductId = lookUpEdit1.EditValue.ToString();
            if (lookUpEdit2.EditValue == null)
            {
                MessageBox.Show("Агент не выбран!");
                return;
            }
            vm.UserId = lookUpEdit2.EditValue.ToString();
            if (int.Parse(numericUpDown1.Value.ToString()) < 1)
            {
                MessageBox.Show("Количество должно быть больше 0!");
                return;
            }
            vm.Quantity = numericUpDown1.Value.ToString();
            List<ConsignmentRequestVM> codes = null;
            GetFromApi:
            try
            {
                codes = ClientIbalance.Generate(vm);
                //codes = new List<ConsignmentRequestVM>() { new ConsignmentRequestVM() { Category = "1", Code = "ASS123", Date = "2017/08/16 12:00", Product = "Certificat"} };
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
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach (int i in list)
            {
                consignments.Add((ConsignmentRequestVM)consignmentRequestVMBindingSource.List[i]);
            }
            if (lookUpEdit3.EditValue == null)
            {
                MessageBox.Show("Шаблон не выбран! Если их нет, то создайте его в конструкторе.");
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
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());
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
            _printManager.LoadListTemplate();
            foreach (var item in _printManager.templates)
            {
                templateVMBindingSource.Add(item);
            }
        }
        #endregion

        private void lookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                Data content = new Data() { type = lookUpEdit5.ItemIndex.ToString() };
                products = ClientIbalance.GetProducts(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message + "\r\nДля проверки соедениния перейдите в настроки.", "Что-то пошло не так!", MessageBoxButtons.OK);
            }
            finally
            {
                productGenerationRequestVMBindingSource.Clear();
                foreach (var item in products)
                    productGenerationRequestVMBindingSource.Add(item);
            }
        }
        class Data
        {
            [JsonProperty("type")]
            public string type;
        }
    }
}