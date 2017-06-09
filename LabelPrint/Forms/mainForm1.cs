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
using System.IO;
using LabelPrint.Forms;
using DevExpress.LookAndFeel;
namespace LabelPrint
{
    public partial class mainForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        GeneratorForm gf;
        PrintTemplate pt;
        ConnectParametrs cp;
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
            if(!String.IsNullOrEmpty(LoadSkin()))
            UserLookAndFeel.Default.SkinName = LoadSkin();
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
            string path = "Settings//Skin.txt";
            try
            {
                File.WriteAllText(path, UserLookAndFeel.Default.SkinName);
            }
            catch { }
        }

        private string LoadSkin()
        {
            string path = "Settings//Skin.txt";
            string skin = string.Empty;
            try
            {
                if (!Directory.Exists("Settings"))
                    Directory.CreateDirectory("Settings");
                if (!File.Exists(path))
                {
                    File.Create("Settings//Url.txt").Close();
                }
                var stream = File.OpenText(path);
                skin = stream.ReadLine();
                stream.Close();
            }
            catch { }
            return skin;
        }
    }
}