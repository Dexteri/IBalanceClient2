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
using System.Globalization;
using System.Threading;

namespace LabelPrint
{
    public partial class mainForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        GeneratorForm gf;
        PrintTemplate pt;
        public mainForm1()
        {
            InitializeComponent();
            XtraTabbedMdiManager mdiManager = new XtraTabbedMdiManager();
            mdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            mdiManager.MdiParent = this;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private void GenerateButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gf == null)
            {
                gf = new GeneratorForm();
                gf.MdiParent = this;
                gf.Disposed += Gf_Disposed;
                gf.Show();
            }
            gf.Focus();
        }

        private void Gf_Disposed(object sender, EventArgs e)
        {
            gf = null;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (pt == null)
            {
                pt = new PrintTemplate();
                pt.MdiParent = this;
                pt.Disposed += Pt_Disposed;
                pt.Show();
            }
            pt.Focus();
        }

        private void Pt_Disposed(object sender, EventArgs e)
        {
            pt = null;
        }
    }
}