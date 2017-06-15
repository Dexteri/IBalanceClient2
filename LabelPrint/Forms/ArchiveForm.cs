using LabelPrint.Models;
using LabelPrint.Setup;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace LabelPrint.Forms
{
    public partial class ArchiveForm : Form
    {
        private PrintManager _printManager;
        List<Consignment> consignments;
        public ArchiveForm()
        {
            consignments = new List<Consignment>();
            InitializeComponent();
            FillDate();
        }

        private void FillDate()
        {
            ConnectToServer();
            foreach (var item in consignments)
                consignmentBindingSource.Add(item);
            _printManager = new PrintManager();
            FillPrintersAndTemplates();
            foreach (var item in _printManager.templates)
                templateVMBindingSource.Add(item);
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
        private void ConnectToServer()
        {
            bool conn = true;
            while (conn)
            {
                try
                {
                    consignments = ClientIbalance.GetConsignments();
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach (int i in list)
            {
                ConsignmentRequestVM vm = new ConsignmentRequestVM();
                vm.Model = ((Consignment)consignmentBindingSource.List[i]).Model;
                vm.ProductionDate = ((Consignment)consignmentBindingSource.List[i]).ConsignmentDate.ToShortDateString();
                vm.SerialKey = ((Consignment)consignmentBindingSource.List[i]).SerialKey;
                consignments.Add(vm);
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _printManager.LoadTemplate(lookUpEdit3.EditValue.ToString());
            List<ConsignmentRequestVM> consignments = new List<ConsignmentRequestVM>();
            var list = gridView1.GetSelectedRows();
            foreach (int i in list)
            {
                ConsignmentRequestVM vm = new ConsignmentRequestVM();
                vm.Model = ((Consignment)consignmentBindingSource.List[i]).Model;
                vm.ProductionDate = ((Consignment)consignmentBindingSource.List[i]).ConsignmentDate.ToShortDateString();
                vm.SerialKey = ((Consignment)consignmentBindingSource.List[i]).SerialKey;
                consignments.Add(vm);
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выбранные объекты", "Внимания", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<int> idList = new List<int>();

                var list = gridView1.GetSelectedRows();
                foreach (int i in list)
                    idList.Add(((Consignment)consignmentBindingSource.List[i]).Id);
                if (ClientIbalance.DeleteConsignments(idList))
                {
                    ConnectToServer();
                    foreach (var item in consignments)
                        consignmentBindingSource.Add(item);
                    gridView1.RefreshData();
                }
            }
            
        }
    }
}
