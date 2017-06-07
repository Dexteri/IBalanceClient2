using System;

namespace LabelPrint
{
    partial class GeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.consignmentRequestVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductionDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.productGenerationRequestVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.counterpartyTemplateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.counterpartyGenerationRequestVMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.print_btn = new System.Windows.Forms.Button();
            this.print_prev_btn = new System.Windows.Forms.Button();
            this.lookUpEdit3 = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentRequestVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productGenerationRequestVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterpartyTemplateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterpartyGenerationRequestVMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Продукт ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(172, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Контрагент";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(341, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество кодов";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(490, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Номер партии ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.Location = new System.Drawing.Point(345, 36);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 27);
            this.numericUpDown1.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(494, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 27);
            this.textBox1.TabIndex = 8;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.consignmentRequestVMBindingSource;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(16, 69);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1083, 468);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // consignmentRequestVMBindingSource
            // 
            this.consignmentRequestVMBindingSource.DataSource = typeof(LabelPrint.Models.ConsignmentRequestVM);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colModel,
            this.colProductionDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            // 
            // colModel
            // 
            this.colModel.FieldName = "Model";
            this.colModel.Name = "colModel";
            this.colModel.Visible = true;
            this.colModel.VisibleIndex = 1;
            // 
            // colProductionDate
            // 
            this.colProductionDate.FieldName = "ProductionDate";
            this.colProductionDate.Name = "colProductionDate";
            this.colProductionDate.Visible = true;
            this.colProductionDate.VisibleIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(614, 15);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(129, 48);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "Запустить генерацию";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(16, 37);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 38, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Model", 52, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProductionDate", "Production Date", 123, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lookUpEdit1.Properties.DataSource = this.productGenerationRequestVMBindingSource;
            this.lookUpEdit1.Properties.DisplayMember = "Model";
            this.lookUpEdit1.Properties.ValueMember = "Id";
            this.lookUpEdit1.Size = new System.Drawing.Size(149, 26);
            this.lookUpEdit1.TabIndex = 11;
            // 
            // productGenerationRequestVMBindingSource
            // 
            this.productGenerationRequestVMBindingSource.DataSource = typeof(LabelPrint.Models.ProductGenerationRequestVM);
            // 
            // counterpartyTemplateBindingSource
            // 
            this.counterpartyTemplateBindingSource.DataSource = typeof(LabelPrint.Models.TemplateVM);
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.Location = new System.Drawing.Point(176, 37);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lookUpEdit2.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Properties.DataSource = this.counterpartyGenerationRequestVMBindingSource;
            this.lookUpEdit2.Properties.DisplayMember = "Name";
            this.lookUpEdit2.Properties.ValueMember = "Id";
            this.lookUpEdit2.Size = new System.Drawing.Size(149, 26);
            this.lookUpEdit2.TabIndex = 12;
            this.lookUpEdit2.EditValueChanged += new System.EventHandler(this.lookUpEdit2_EditValueChanged);
            // 
            // counterpartyGenerationRequestVMBindingSource
            // 
            this.counterpartyGenerationRequestVMBindingSource.DataSource = typeof(LabelPrint.Models.CounterpartyGenerationRequestVM);
            // 
            // print_btn
            // 
            this.print_btn.Location = new System.Drawing.Point(760, 15);
            this.print_btn.Name = "print_btn";
            this.print_btn.Size = new System.Drawing.Size(81, 48);
            this.print_btn.TabIndex = 13;
            this.print_btn.Text = "Print";
            this.print_btn.UseVisualStyleBackColor = true;
            this.print_btn.Click += new System.EventHandler(this.print_btn_Click);
            // 
            // print_prev_btn
            // 
            this.print_prev_btn.Location = new System.Drawing.Point(858, 15);
            this.print_prev_btn.Name = "print_prev_btn";
            this.print_prev_btn.Size = new System.Drawing.Size(79, 48);
            this.print_prev_btn.TabIndex = 14;
            this.print_prev_btn.Text = "Print Preview";
            this.print_prev_btn.UseVisualStyleBackColor = true;
            this.print_prev_btn.Click += new System.EventHandler(this.print_prev_btn_Click);
            // 
            // lookUpEdit3
            // 
            this.lookUpEdit3.Location = new System.Drawing.Point(944, 36);
            this.lookUpEdit3.Name = "lookUpEdit3";
            this.lookUpEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit3.Properties.DataSource = this.counterpartyTemplateBindingSource;
            this.lookUpEdit3.Properties.DisplayMember = "Name";
            this.lookUpEdit3.Properties.ValueMember = "Name";
            this.lookUpEdit3.Size = new System.Drawing.Size(155, 20);
            this.lookUpEdit3.TabIndex = 15;
            this.lookUpEdit3.EditValueChanged += new System.EventHandler(this.lookUpEdit3_EditValueChanged);
            this.lookUpEdit3.Popup += new System.EventHandler(this.lookUpEdit3_Popup);
            // 
            // GeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 549);
            this.Controls.Add(this.lookUpEdit3);
            this.Controls.Add(this.print_prev_btn);
            this.Controls.Add(this.print_btn);
            this.Controls.Add(this.lookUpEdit2);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GeneratorForm";
            this.Text = "Генерация";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentRequestVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productGenerationRequestVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterpartyTemplateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterpartyGenerationRequestVMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        public System.Windows.Forms.BindingSource consignmentRequestVMBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colModel;
        private DevExpress.XtraGrid.Columns.GridColumn colProductionDate;
        public System.Windows.Forms.BindingSource productGenerationRequestVMBindingSource;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit2;
        public System.Windows.Forms.BindingSource counterpartyGenerationRequestVMBindingSource;
        private System.Windows.Forms.Button print_btn;
        private System.Windows.Forms.Button print_prev_btn;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit3;
        public System.Windows.Forms.BindingSource counterpartyTemplateBindingSource;
    }
}