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

        public List<TemplateVM> templates;

        private String currentTemplate;

        private const string folderName = "Templates";
        private const string format = ".xml";

        private string[] tags = new string[] { "Model\n", "ProductionDate\n", "SerialKey\n" };

        private string htmlDocument;

        public string CurrentTemplate
        {
            get
            {
                return currentTemplate;
            }

            set
            {
                if (currentTemplate != value)
                {
                    LoadTemplate(value);
                    currentTemplate = value;
                }
            }
        }

        private PrintManager()
        {
            templates = LoadListTemplate();
            if (templates.Count > 0)
                LoadTemplate(templates[0].Name);
        }
        public static PrintManager Instance()
        {
            if (_printManager == null) _printManager = new PrintManager();
            return _printManager;
        }

        public void LoadTemplate(string nameFile)
        {
            string path = folderName + "\\" + (nameFile) + format;
            if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);

            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();

            richEditControl1.LoadDocument(path, DocumentFormat.OpenXml);
            
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

        public void ShowPrintPreview(List<ConsignmentRequestVM> datas)
        {
            ConsignmentRequestVM data = datas.FirstOrDefault();

            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();
            richEditControl1.Document.HtmlText = DataTemplate(data);
            richEditControl1.ShowPrintPreview();
        }
        public void Print(ConsignmentRequestVM data)
        {
            DevExpress.XtraRichEdit.RichEditControl richEditControl1 = new RichEditControl();
            richEditControl1.Document.HtmlText = DataTemplate(data);
            richEditControl1.Print();
        }
        public void PrintCollection(List<ConsignmentRequestVM> datas)
        {
            foreach (var data in datas)
            {
                Print(data);
            }
        }
    }
}
