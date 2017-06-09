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
            textBox1.Text = GetUrl(); 
        }

        private string GetUrl()
        {
            string url = string.Empty;
            try
            {
                var stream = File.OpenText(path);
                url = stream.ReadLine();
                stream.Close();
            }
            catch
            {
                var stream = File.Create("Settings//Url.txt");
                stream.Close();
            }
            return url;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(path, textBox1.Text);
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
