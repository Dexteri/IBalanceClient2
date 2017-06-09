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
        DevExpress.XtraRichEdit.RichEditControl richEditControl1;

        public List<TemplateVM> templates;

        private String currentTemplate;

        private const string folderName = "Templates";
        private const string format = ".xml";

        private string[] tags = new string[] { "Model\n", "ProductionDate\n", "SerialKey\n" };
        

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
            richEditControl1 = new RichEditControl();
            templates = LoadListTemplate();
            if (templates.Count > 0)
                LoadTemplate(templates[0].Name);
            richEditControl1 = new RichEditControl();
        }
        public static PrintManager Instance()
        {
            if (_printManager == null) _printManager = new PrintManager();
            return _printManager;
        }

        public void LoadTemplate(string nameFile)
        {
            string path = folderName + "\\" + (nameFile);
            richEditControl1.LoadDocument(path);
        }

        public string DataTemplate(ConsignmentRequestVM data)
        {
            string result = this.richEditControl1.WordMLText;
            if (data != null)
            {
                if (result.Contains("Model"))
                    result = result.Replace("Model", data.Model);
                if (result.Contains("ProductionDate"))
                    result = result.Replace("ProductionDate", data.ProductionDate);
                if (result.Contains("SerialKey"))
                    result = result.Replace("SerialKey", data.SerialKey);
            }
            return result;
        }

        public List<TemplateVM> LoadListTemplate()
        {
            List<TemplateVM> result = new List<TemplateVM>();
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            foreach (var item in Directory.GetFiles(folderName))
                result.Add(new TemplateVM() { Name = item.Split('\\')[1] });
            return result;
        }

        public void ShowPrintPreview(List<ConsignmentRequestVM> datas)
        {
            ConsignmentRequestVM data = datas.FirstOrDefault();
            
            richEditControl1.Document.WordMLText = DataTemplate(data);
            richEditControl1.ShowPrintPreview();
        }
        public void Print(ConsignmentRequestVM data)
        {
            richEditControl1.Document.WordMLText = DataTemplate(data);
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
