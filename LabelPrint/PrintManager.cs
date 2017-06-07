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

        private const string folderName = "Template";
        private string nameTmp = "Template";
        private const string format = ".rtf";

        private string[] tags = new string[] { "Model\n", "ProductionDate\n", "SerialKey\n" };

        private string htmlDocument;
        public string Template
        {
            get
            {
                this.LoadRtfTemplate();
                return htmlDocument;
            }
        }
        public string Path { get { return /*folderName + "//" + */nameTmp + format; } }

        private List<ConsignmentRequestVM> listCodes;

        private PrintManager()
        {
            LoadRtfTemplate();
            //LoadRtfTemplate2();
        }
        public static PrintManager Instance()
        {
            if (_printManager == null) _printManager = new PrintManager();
            return _printManager;
        }
        public void SetCodes(List<ConsignmentRequestVM> codes)
        {
            this.listCodes = codes;
        }
        public void LoadRtfTemplate(string nameFile = null)
        {
            string path = folderName + "\\" + (nameFile == null ? nameTmp : nameFile) + format;
            if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    foreach (string element in this.tags)
                    {
                        sw.WriteLine(element);
                    }
                    sw.Close();
                }
            }

            if (!string.IsNullOrEmpty(nameFile)) this.nameTmp = nameFile;
            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();
            richEditControl1.LoadDocument(path, DocumentFormat.Rtf);
            //"Model\n", "ProductionDate\n", "SerialKey\n"
            string text = "Model \n\r" + "ProductionDate \n\r" + "SerialKey \n\r";
            if (richEditControl1.Document.Text == "")
            {
                richEditControl1.Document.Text = text;
                htmlDocument = richEditControl1.Document.Text;
            }
            else
                htmlDocument = richEditControl1.Document.HtmlText;
        }

        public string DataTemplate(ConsignmentRequestVM data)
        {
            string result = this.htmlDocument;

            if (result.Contains("Model"))
                result = result.Replace("Model", data.Model);
            if (result.Contains("ProductionDate"))
                result = result.Replace("ProductionDate", data.ProductionDate);
            if (result.Contains("SerialKey"))
                result = result.Replace("SerialKey", data.ProductionDate);
            return result;
        }
        public List<TemplateVM> LoadListTemplate()
        {
            List<TemplateVM> result = new List<TemplateVM>();
            if (Directory.Exists(folderName + "\\"))
            {
                foreach (var item in Directory.GetFiles(folderName + "\\"))
                    result.Add(new TemplateVM() { Name = item.Split('\\')[1].Split('.')[0] });
            }
            return result;
        }
        public void ShowPrintPreview(ConsignmentRequestVM data = null)
        {
            if (this.listCodes == null || this.listCodes.Count < 0) return;
            if (data == null)
                data = this.listCodes.FirstOrDefault();

            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();
            richEditControl1.Document.HtmlText = DataTemplate(data);
            richEditControl1.ShowPrintPreview();
        }
        public void ShowPrintDialog(ConsignmentRequestVM data = null)
        {
            if (this.listCodes == null || this.listCodes.Count < 0) return;
            if (data == null)
                data = this.listCodes.FirstOrDefault();

            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();
            richEditControl1.Document.HtmlText = DataTemplate(data);
            richEditControl1.ShowPrintDialog();
        }
        public void Print(ConsignmentRequestVM data)
        {
            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();
            richEditControl1.Document.HtmlText = DataTemplate(data);
            richEditControl1.Print();
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
            ShowPrintPreview();
        }
    }
}
