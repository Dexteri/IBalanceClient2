using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using LabelPrint.Models;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Barcode;

namespace LabelPrint
{
    public class PrintManager
    {
        DevExpress.XtraRichEdit.RichEditControl richEditControl1;

        public List<TemplateVM> templates;

        private String currentTemplate;

        private const string folderName = "Templates";

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
            LoadListTemplate();
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

            if (data != null)
            {
                if (result.Contains("ModelKey"))
                    result = result.Replace("ModelKey", data.Model);
                if (result.Contains("ProdDate"))
                    result = result.Replace("ProdDate", data.ProductionDate);
                if (result.Contains("SerialKey"))
                    result = result.Replace("SerialKey", data.SerialKey);

                string image1 = GetDefaultStringImage(result, 1);
                string image2 = GetDefaultStringImage(result, 0);

                if (result.Contains(image1))
                {
                    //result = result.Replace(image1, GenerateBacode(data.Model, sizeIm1));
                    result = result.Replace(image1, getBarcode(data.Model));
                }

                if (result.Contains(image2))
                {
                   // result = result.Replace(image2, GenerateBacode(data.SerialKey, sizeIm2));
                    result = result.Replace(image2, getBarcode(data.SerialKey));
                }
            }
            return result;
        }

        private string GetDefaultStringImage(string text, int number_image)
        {
            string start = ".png\">";
            int index = text.LastIndexOf(start);
            if (number_image == 1)
                index = text.Remove(index).LastIndexOf(start);
            while (true)
            {
                index--;
                if (text[index] == 'i')
                    break;
            }
            bool checkImageString = false;
            string defaultImage = string.Empty;
            for (int i = index; i < text.Length; i++)
            {
                if (text[i].ToString().Equals("<"))
                {
                    break;
                }
                if (checkImageString)
                {
                    defaultImage += text[i].ToString();
                }
                if (text[i].ToString().Equals(">"))
                {
                    checkImageString = !checkImageString;
                }
            }
            return defaultImage;
        }
       
        private string GenerateBacode(string _data, string size)
        {
            Linear barcode = new Linear();
            barcode.Type = BarcodeType.CODE128;
            barcode.Data = _data;
            barcode.TextFont = new Font("Arial", 12);
            //barcode.BarcodeWidth = 100f;
            //barcode.BarcodeHeight = 100f;
            byte[] array = barcode.drawBarcodeAsBytes();

            return Convert.ToBase64String(array);
        }

        private string getBarcode(string text)
        {
            BarcodeSymbology s = BarcodeSymbology.Code128;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            var barcodeImage = drawObject.Draw(text, metrics);

            using (MemoryStream ms = new MemoryStream())
            {
                barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
        }


        public void LoadListTemplate()
        {
            List<TemplateVM> result = new List<TemplateVM>();
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            foreach (var item in Directory.GetFiles(folderName))
                result.Add(new TemplateVM() { Name = item.Split('\\')[1] });
            templates = result;
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
