using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Services;
using LabelPrint.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPrint
{
    public partial class PrintTemplate : Form
    {
        private readonly string path = "Templates\\" + DefaultSettings.Get(XmlNodeName.LAST_SELECTED_TEMPLATE);
        public PrintTemplate()
        {
            InitializeComponent();
            this.richEditControl1.EmptyDocumentCreated += RichEditControl1_EmptyDocumentCreated;
            if (File.Exists(path))
                this.richEditControl1.LoadDocument(path);
            else
                FillTemplateDefault();
        }

        private void RichEditControl1_EmptyDocumentCreated(object sender, EventArgs e)
        {
            FillTemplateDefault();
        }

        private void FillTemplateDefault()
        {
            string text = "Model \n\r" + "ProductionDate \n\r" + "SerialKey \n\r";
            richEditControl1.Document.Text = text;
            richEditControl1.Document.AppendImage(DrawText());

            this.richEditControl1.Options.DocumentSaveOptions.DefaultFormat = DocumentFormat.OpenXml;
            this.richEditControl1.Options.DocumentSaveOptions.DefaultFileName = @"Templates\Template";
        }

        private Image DrawText()
        {
            Image img = new Bitmap(100, 100);
            Graphics drawing = Graphics.FromImage(img);
            drawing.Save();
            return img;

        }

        private void fileNewItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
