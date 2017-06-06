using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;

namespace LabelPrint
{
    public partial class mainForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private PrintTemplate form;
        private PrintManager _printManager;
        public mainForm1()
        {
            InitializeComponent();
            form = new PrintTemplate();
            form.LoadRichEditControl(ref _printManager);
            XtraTabbedMdiManager mdiManager = new XtraTabbedMdiManager();
            mdiManager.MdiParent = this;
        }

        private void GenerateButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            GeneratorForm form = new GeneratorForm();
            form.MdiParent = this;
            form.Show();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            form.MdiParent = this;
            form.Show();
        }
    }
}