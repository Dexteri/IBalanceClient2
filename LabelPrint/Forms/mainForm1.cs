using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;
using System.Globalization;
using System.Threading;
using System.IO;
using LabelPrint.Forms;
using DevExpress.LookAndFeel;
using LabelPrint.Setup;
using DevExpress.UserSkins;

namespace LabelPrint
{
    public partial class mainForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        GeneratorForm gf;
        PrintTemplate pt;
        ConnectParametrs cp;
        ArchiveForm af;
        public mainForm1()
        {
            InitializeComponent();
            XtraTabbedMdiManager mdiManager = new XtraTabbedMdiManager();
            mdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            mdiManager.MdiParent = this;
            if (gf == null)
            {
                gf = new GeneratorForm();
                gf.MdiParent = this;
                gf.Disposed += Gf_Disposed;
                gf.Show();
            }
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            string skinLoad = DefaultSettings.Get(XmlNodeName.LAST_SELECTED_SKIN);
            BonusSkins.Register();
            if (!String.IsNullOrEmpty(skinLoad))
                UserLookAndFeel.Default.SkinName = skinLoad;
            if (!Directory.Exists("Settings"))
                Directory.CreateDirectory("Settings");
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

        private void barButtonConnectionStrings_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (cp == null)
            {
                cp = new ConnectParametrs();
                cp.MdiParent = this;
                cp.Disposed += Cp_Disposed;
                cp.Show();
            }
            cp.Focus();
        }
        private void Cp_Disposed(object sender, EventArgs e)
        {
            cp = null;
        }

        private void mainForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DefaultSettings.Set(XmlNodeName.LAST_SELECTED_SKIN, UserLookAndFeel.Default.SkinName);
            }
            catch { }
        }

        private void barButtonArchive_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (af == null)
            {
                af = new ArchiveForm();
                af.MdiParent = this;
                af.Disposed += Af_Disposed;
                af.Show();
            }
            af.Focus();
        }

        private void Af_Disposed(object sender, EventArgs e)
        {
            af = null;
        }
    }
}