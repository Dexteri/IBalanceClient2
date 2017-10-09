using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
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
            richEditControl1.Document.Text = string.Empty;
            richEditControl1.Document.AppendText("ProdDate \n\r");
            richEditControl1.Document.AppendText("ModelKey \n\r");
            richEditControl1.Document.Images.Append(DrawTextTmp("ModelKey"));
            richEditControl1.Document.Paragraphs.Append();
            richEditControl1.Document.AppendText("SerialKey \n\r");
            richEditControl1.Document.Images.Append(DrawTextTmp("SerialKey"));
            this.richEditControl1.Options.DocumentSaveOptions.DefaultFormat = DocumentFormat.OpenXml;
            this.richEditControl1.Options.DocumentSaveOptions.DefaultFileName = @"Templates\Template";
        }
        private Image DrawTextTmp(String text)
        {
            Image img = new Bitmap(1,1);
            Graphics drawing = Graphics.FromImage(img);
            Font font = new Font("Arial", 10);
            Color textColor = Color.Yellow;
            Color backColor = Color.Black;
            SizeF textSize = drawing.MeasureString(text, font);
            img.Dispose();
            drawing.Dispose();
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);
            Brush textBrush = new SolidBrush(textColor);
            
            drawing.DrawString(text, font, textBrush, 0, 0);
            
            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();
            return img;
        }
    }
}
