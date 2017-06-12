using LabelPrint.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPrint.Forms
{
    public partial class ConnectParametrs : Form
    {
        string path = "Settings//Url.txt";
        public ConnectParametrs()
        {
            InitializeComponent();
            textBox1.Text = DefaultSettings.Get(XmlNodeName.URL); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DefaultSettings.Set(XmlNodeName.URL, textBox1.Text);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientIbalance.Url = textBox1.Text;
            MessageBox.Show(ClientIbalance.Check() ? " Подключено." : "Ошибка.");
            ClientIbalance.Url = string.Empty;
        }
    }
}
