﻿using DevExpress.XtraPrinting;
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
using System.Windows.Forms;
using Zen.Barcode;
//using Zen.Barcode;

namespace LabelPrint
{
    public class PrintManager
    {
        DevExpress.XtraRichEdit.RichEditControl richEditControl1;

        public List<TemplateVM> templates;

        private String currentTemplate;

        private const string folderName = "Templates";
        string TemplateText;
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
            TemplateText = this.richEditControl1.WordMLText;
        }

        public string DataTemplate(ConsignmentRequestVM data)
        {
            string result = TemplateText;

            if (data != null)
            {
                string image = "iVBORw0KGgoAAAANSUhEUgAAAEEAAAAQCAYAAABJJRIXAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwwAADsMBx2+oZAAAAaxJREFUWEftVcFxhDAMpDwKohx6oRU6IVrFixchcczcPTJcMrMTS7JW8triBvvb/pE7vw2p89vQjWUbtm0dtlF8xDhbzOLzeI7dBfhf5lt8tX2TrGPOtLzfS0A3XISCfDZx/oIIvAyPfw7dQJOLqbxaIfUPkxU2/61DXOBtEdAHY59FN7xJK7SGkcDzW8wfD8HX4UjGiM8WgLBX+fvBKhFeCJBxeX2D7isuohvcAELdCD+IlcCLSoFoxwbiHGvcnzj3WvwkAi7G/uMiyKcouaJwsK3P5JvXDR4SRPtIILGR7iJoo0TwqWAap+8Q19xkjYOs1oPmK0ouAy6H4kGs06j/ohuHQzbFkEiSSxGSeGxYm8Waz5c4cbc1G68+iiUXc+IlSm5DN3QTRwI+Ft3jhpMIwZcVVF8WdyhPW+u+OHZAyQWQrx4FoBtK5iMRCmr81jdAirot+Yf9aBCNYs2mZX04YPPpsy65GrzXkBPQjYOirZh+jKLiJHfIgQkenA3EfNiMx7qlCEA7qB4q5WqoxkiQOp8FiMaXkiN1Pgp4kdXPa0PqfAba2Fy/gmH7AecRrSgvUrLyAAAAAElFTkSuQmCC";

                if (result.Contains(image))
                {
                    result = result.Replace(image, getBarcode(data.Product));
                }
                string image2 = "iVBORw0KGgoAAAANSUhEUgAAAD8AAAAQCAYAAAChpac8AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwwAADsMBx2+oZAAAAYxJREFUWEfllVGShCAMRD2eB/I43oWreBM2YRLsxEatqdkPy616NRAQukNgJ/mrL4YG3wINvoUYKHWqFShLHL/NPNVNvl/YGILzrL3KL85ZykdLjv+AvaPGtxUGTczXCbjDhflZ9KjxyyR+hzVQBNA2l8xj7KecmZek/6NxZe/oyd855XX7iArCVLjEi40VSVpIphlxeoWNzF8YZxra9UgHpZ5Orgt0kkCWCNwgVIUJp6aEkFg0hvOsvcq4/o4OYqgB1/W+JGn2/hEa7I9MTkLIJBPOxjLkO2zrnpvsH9YDhhoErQjXqx7CG3aEBjv5wdGNPSlOE5JEHPoClqqChrHtgkeP3VCDf2OVEJLEscaoRJKJ4YJpHjPVK4iMYRvXbwnzsjZOTfl61yWv7J2W0bRRfkRCXzdIwql5nCf4lWoGcJ61gzGLYfkONRheYRclr8RALk22AJZdF4omSB/X1QroJ4rzrH04VTOIWqgGY3RdCDT4bDRZXhnn0OCj0Wsx+jeZoMFnYtfj3qlP9Q8dQWO8dEG+kAAAAABJRU5ErkJggg==";

                if (result.Contains(image2))
                {
                    result = result.Replace(image2, getBarcode(data.Code));
                }

                if (result.Contains("ModelKey"))
                    result = result.Replace("ModelKey", data.Product);
                if (result.Contains("ProdDate"))
                    result = result.Replace("ProdDate", data.Date);
                if (result.Contains("SerialKey"))
                    result = result.Replace("SerialKey", data.Code);
            }
            return result;
        }

        private string GetDefaultStringImage(string text, int number_image)
        {
            string start = ".tif\">";
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

        private string getBarcode(string text)
        {
            BarcodeSymbology s = BarcodeSymbology.Code128;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);
            var metrics = drawObject.GetDefaultMetrics(60);
            metrics.Scale = 2;
            try
            {
                var barcodeImage = drawObject.Draw(text, metrics);
                using (MemoryStream ms = new MemoryStream())
                {
                    barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Штрих код может быт сгенерирован только для латинских букв!");
            }
            return String.Empty;
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
