using DevExpress.XtraRichEdit;
using LabelPrint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrint
{
    public class PrintManager
    {
        private static PrintManager _printManager;

        private const string nameTmp = "Template";
        private const string format = ".rtf";
        private string[] tags = new string[] { "Model\n", "ProductionDate\n", "SerialKey\n" };
        private List<ConsignmentRequestVM> listCodes;

        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;

        public PrintManager(DevExpress.XtraRichEdit.RichEditControl richEditControl)
        {
            this.richEditControl1 = richEditControl;
            LoadTemplate();
        }
        public void SetCodes(List<ConsignmentRequestVM> codes)
        {
            this.listCodes = codes;
        }

        public void DataTemplate(ConsignmentRequestVM data)
        {
            if (this.richEditControl1.Document.HtmlText.Contains("Model"))
                this.richEditControl1.Document.HtmlText = this.richEditControl1.Document.HtmlText.Replace("Model", data.Model);
            if (this.richEditControl1.Document.HtmlText.Contains("ProductionDate"))
                this.richEditControl1.Document.HtmlText = this.richEditControl1.Document.HtmlText.Replace("ProductionDate", data.ProductionDate);
            if (this.richEditControl1.Document.HtmlText.Contains("SerialKey"))
                this.richEditControl1.Document.HtmlText = this.richEditControl1.Document.HtmlText.Replace("SerialKey", data.ProductionDate);
        }
        public bool CheckExist()
        {
            string path = nameTmp + format;
            bool result = File.Exists(path);
            if (!result)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    foreach (string element in this.tags)
                    {
                        sw.WriteLine(element);
                    }
                }
            }
            return result;
        }
        public bool CreateTemplateWithData()
        {
            bool result = false;
            string path = nameTmp + format;
            string newPath = path + "0" + format;
            if (File.Exists(newPath))
                File.Delete(newPath);
            CheckExist();
            File.Copy(path, newPath);
            this.richEditControl1.LoadDocument(newPath, DocumentFormat.Rtf);
            return result;
        }
        public void LoadTemplate()
        {
            string path = nameTmp + format;
            CheckExist();
            this.richEditControl1.LoadDocument(path, DocumentFormat.Rtf);
        }
        public void ShowPrintPreview(ConsignmentRequestVM data = null)
        {
            if (this.richEditControl1.IsPrintingAvailable)
            {
                if (this.listCodes == null || this.listCodes.Count < 0) return;
                if (data == null)
                    data = this.listCodes.FirstOrDefault();
                CreateTemplateWithData();
                DataTemplate(data);
                richEditControl1.ShowPrintPreview();
                LoadTemplate();
            }
        }
        public void ShowPrintDialog(ConsignmentRequestVM data)
        {
            if (this.richEditControl1.IsPrintingAvailable)
            {
                CreateTemplateWithData();
                DataTemplate(data);
                richEditControl1.ShowPrintDialog();
                LoadTemplate();
            }
        }
        public void Print(ConsignmentRequestVM data)
        {
            if (this.richEditControl1.IsPrintingAvailable)
            {
                CreateTemplateWithData();
                DataTemplate(data);
                richEditControl1.Print();
                LoadTemplate();
            }
        }
        public void PrintCollection()
        {
            if (this.listCodes != null)
                foreach (var data in this.listCodes)
                {
                    Print(data);
                }
        }
        public void ShowPrintPreviewCollection()
        {
            if (this.listCodes != null)
                foreach (var data in this.listCodes)
                {
                    ShowPrintPreview(data);
                }
        }
    }
}
