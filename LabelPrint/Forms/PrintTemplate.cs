using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPrint
{
    public partial class PrintTemplate : Form
    {
        private PrintManager _printManager;
        public PrintTemplate()
        {
            InitializeComponent();
            _printManager = PrintManager.Instance();
            this.richEditControl1.Document.HtmlText = _printManager.Template;
        }
        private void richEditControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _printManager.PrintCollection();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _printManager.ShowPrintPreview();
        }

        private void richEditControl1_Click_1(object sender, EventArgs e)
        {

        }

        private void fileSaveItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        
        }
    }
}
