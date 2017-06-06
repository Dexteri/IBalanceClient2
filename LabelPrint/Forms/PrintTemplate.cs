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
            _printManager = new PrintManager(this.richEditControl1);
        }
        public void LoadRichEditControl(ref PrintManager _printManager)
        {
            _printManager = this._printManager;
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
    }
}
