using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using LabelPrint.Models;
using OnBarcode.Barcode;
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
        DevExpress.XtraRichEdit.RichEditControl richEditControl1;

        public List<TemplateVM> templates;

        private String currentTemplate;

        private const string folderName = "Templates";

        private string[] tags = new string[] { "Model\n", "ProductionDate\n", "SerialKey\n"};
        

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

        public PrintManager()
        {
            richEditControl1 = new RichEditControl();
            templates = LoadListTemplate();
            if (templates.Count > 0)
                LoadTemplate(templates[0].Name);
            richEditControl1 = new RichEditControl();
        }

        public void LoadTemplate(string nameFile)
        {
            string path = folderName + "\\" + (nameFile);
            richEditControl1.LoadDocument(path);
        }

        public string DataTemplate(ConsignmentRequestVM data)
        {
            string result = this.richEditControl1.WordMLText;
            string start = ">iVBORw0";
            string barCode = GenerateBacode(data.SerialKey);
            if (data != null)
            {
                if (result.Contains("Model"))
                    result = result.Replace("Model", data.Model);
                if (result.Contains("ProductionDate"))
                    result = result.Replace("ProductionDate", data.ProductionDate);
                if (result.Contains("SerialKey"))
                    result = result.Replace("SerialKey", data.SerialKey);
                int index = result.IndexOf(start);
                bool checkImageString = false;
                string defaultImage = string.Empty;
                for(int i = index; i< result.Length; i++)
                {
                    if (result[i].ToString().Equals("<"))
                    {
                        break;
                    }
                    if (checkImageString)
                    {
                        defaultImage += result[i].ToString();
                    }
                    if (result[i].ToString().Equals(">"))
                    {
                        checkImageString = !checkImageString;
                    }   
                }
                if (result.Contains(defaultImage))
                    result = result.Replace(defaultImage, barCode);
            }
            return result;
        }

        private string GenerateBacode(string _data)
        {
            Linear barcode = new Linear();
            barcode.Type = BarcodeType.CODE11;
            barcode.Data = _data;
            byte[] array = barcode.drawBarcodeAsBytes();
            return Convert.ToBase64String(array);
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
            if (this.richEditControl1.IsPrintingAvailable)
            {
                ConsignmentRequestVM data = datas.FirstOrDefault();

                richEditControl1.Document.WordMLText = DataTemplate(data);
                richEditControl1.ShowPrintPreview();
            }
        }
        public void Print(ConsignmentRequestVM data, String printerName)
        {
            richEditControl1.Document.WordMLText = DataTemplate(data);
            PrintableComponentLink pcl = new PrintableComponentLink(new PrintingSystem());
            pcl.Component = richEditControl1;
            pcl.Print(printerName);
        }
        public void PrintCollection(List<ConsignmentRequestVM> datas, String printerName)
        {
            foreach (var data in datas)
            {
                Print(data, printerName);
            }
        }
    }

}
