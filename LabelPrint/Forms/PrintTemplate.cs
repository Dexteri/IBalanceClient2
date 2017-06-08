﻿using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Services;
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

namespace LabelPrint
{
    public partial class PrintTemplate : Form
    {
        private const string folderName = "Templates";
        public PrintTemplate()
        {
            InitializeComponent();
            FillTemplateDefault();
        }
        private void FillTemplateDefault()
        {
            string text = "Model \n\r" + "ProductionDate \n\r" + "SerialKey \n\r";
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            richEditControl1.Document.Text = text;
            this.richEditControl1.Options.DocumentSaveOptions.DefaultFormat = DocumentFormat.OpenXml;
            this.richEditControl1.Options.DocumentSaveOptions.DefaultFileName = @"Templates\Template";
        }


    }
}
