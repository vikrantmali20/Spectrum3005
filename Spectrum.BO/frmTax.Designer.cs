namespace Spectrum.BO
{
    partial class frmTax
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTax));
            this.TaxActionButton = new Spectrum.Controls.ActionButtons();
            this.dgTax = new Spectrum.Controls.FlexGrid(this.components);
            this.rdoTaxExclusive = new System.Windows.Forms.RadioButton();
            this.lblTaxValue = new Spectrum.Controls.LabelMandatory();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoTaxInclusive = new System.Windows.Forms.RadioButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtTaxName = new Spectrum.Controls.TextBox(this.components);
            this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdoTVTPercent = new System.Windows.Forms.RadioButton();
            this.rdoTVTValue = new System.Windows.Forms.RadioButton();
            this.lblTaxName = new Spectrum.Controls.LabelMandatory();
            this.chkInterStateTax = new System.Windows.Forms.CheckBox();
            this.lblTaxValueType = new Spectrum.Controls.LabelMandatory();
            this.cmbAppliedon = new System.Windows.Forms.ComboBox();
            this.lblAppliedOn = new Spectrum.Controls.LabelMandatory();
            this.txtTaxValue = new Spectrum.Controls.TextBox(this.components);
            this.cmbDocumentType = new System.Windows.Forms.ComboBox();
            this.lbldocumenttype = new Spectrum.Controls.LabelMandatory();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnArticleDelete = new Spectrum.Controls.Button(this.components);
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new Spectrum.Controls.Label(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgTax)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
            this.c1Sizer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // TaxActionButton
            // 
            this.TaxActionButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TaxActionButton.Location = new System.Drawing.Point(552, 14);
            this.TaxActionButton.Margin = new System.Windows.Forms.Padding(4);
            this.TaxActionButton.Name = "TaxActionButton";
            this.TaxActionButton.Size = new System.Drawing.Size(349, 30);
            this.TaxActionButton.TabIndex = 3;
            // 
            // dgTax
            // 
            this.dgTax.ColumnInfo = resources.GetString("dgTax.ColumnInfo");
            this.dgTax.ExtendLastCol = true;
            this.dgTax.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgTax.Location = new System.Drawing.Point(60, 33);
            this.dgTax.Name = "dgTax";
            this.dgTax.Rows.Count = 1;
            this.dgTax.Rows.DefaultSize = 22;
            this.dgTax.Size = new System.Drawing.Size(908, 288);
            this.dgTax.StyleInfo = resources.GetString("dgTax.StyleInfo");
            this.dgTax.TabIndex = 2;
            this.dgTax.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.dgTax.DoubleClick += new System.EventHandler(this.dgTax_DoubleClick);
            // 
            // rdoTaxExclusive
            // 
            this.rdoTaxExclusive.AutoSize = true;
            this.rdoTaxExclusive.Checked = true;
            this.rdoTaxExclusive.Location = new System.Drawing.Point(91, 7);
            this.rdoTaxExclusive.Name = "rdoTaxExclusive";
            this.rdoTaxExclusive.Size = new System.Drawing.Size(86, 20);
            this.rdoTaxExclusive.TabIndex = 1;
            this.rdoTaxExclusive.TabStop = true;
            this.rdoTaxExclusive.Text = "Exclusive";
            this.rdoTaxExclusive.UseVisualStyleBackColor = true;
            // 
            // lblTaxValue
            // 
            this.lblTaxValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTaxValue.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxValue.Location = new System.Drawing.Point(516, 14);
            this.lblTaxValue.MandatoryLabelText = "*";
            this.lblTaxValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTaxValue.Name = "lblTaxValue";
            this.lblTaxValue.NormalLabelText = "Tax Value";
            this.lblTaxValue.Size = new System.Drawing.Size(105, 21);
            this.lblTaxValue.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoTaxInclusive);
            this.panel1.Controls.Add(this.rdoTaxExclusive);
            this.panel1.Location = new System.Drawing.Point(14, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 36);
            this.panel1.TabIndex = 11;
            // 
            // rdoTaxInclusive
            // 
            this.rdoTaxInclusive.AutoSize = true;
            this.rdoTaxInclusive.Location = new System.Drawing.Point(1, 5);
            this.rdoTaxInclusive.Name = "rdoTaxInclusive";
            this.rdoTaxInclusive.Size = new System.Drawing.Size(84, 20);
            this.rdoTaxInclusive.TabIndex = 101;
            this.rdoTaxInclusive.Text = "Inclusive";
            this.rdoTaxInclusive.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(66, 37);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 10;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtTaxName
            // 
            this.txtTaxName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtTaxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxName.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtTaxName.Location = new System.Drawing.Point(135, 11);
            this.txtTaxName.MaxLength = 15;
            this.txtTaxName.Name = "txtTaxName";
            this.txtTaxName.Size = new System.Drawing.Size(170, 21);
            this.txtTaxName.TabIndex = 97;
            this.txtTaxName.Tag = null;
            this.txtTaxName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtTaxName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtTaxName.Click += new System.EventHandler(this.txtTaxName_Click);
            // 
            // c1Sizer1
            // 
            this.c1Sizer1.Controls.Add(this.panel3);
            this.c1Sizer1.Controls.Add(this.lblTaxName);
            this.c1Sizer1.Controls.Add(this.chkInterStateTax);
            this.c1Sizer1.Controls.Add(this.lblTaxValueType);
            this.c1Sizer1.Controls.Add(this.cmbAppliedon);
            this.c1Sizer1.Controls.Add(this.lblAppliedOn);
            this.c1Sizer1.Controls.Add(this.txtTaxValue);
            this.c1Sizer1.Controls.Add(this.lblTaxValue);
            this.c1Sizer1.Controls.Add(this.txtTaxName);
            this.c1Sizer1.Controls.Add(this.panel1);
            this.c1Sizer1.GridDefinition = "94.1605839416058:False:False;\t99.1061452513967:False:False;";
            this.c1Sizer1.Location = new System.Drawing.Point(6, 22);
            this.c1Sizer1.Name = "c1Sizer1";
            this.c1Sizer1.Size = new System.Drawing.Size(895, 137);
            this.c1Sizer1.TabIndex = 99;
            this.c1Sizer1.Text = "c1Sizer1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdoTVTPercent);
            this.panel3.Controls.Add(this.rdoTVTValue);
            this.panel3.Location = new System.Drawing.Point(624, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(221, 36);
            this.panel3.TabIndex = 112;
            // 
            // rdoTVTPercent
            // 
            this.rdoTVTPercent.AutoSize = true;
            this.rdoTVTPercent.Location = new System.Drawing.Point(3, 7);
            this.rdoTVTPercent.Name = "rdoTVTPercent";
            this.rdoTVTPercent.Size = new System.Drawing.Size(101, 20);
            this.rdoTVTPercent.TabIndex = 101;
            this.rdoTVTPercent.Text = "Percentage";
            this.rdoTVTPercent.UseVisualStyleBackColor = true;
            // 
            // rdoTVTValue
            // 
            this.rdoTVTValue.AutoSize = true;
            this.rdoTVTValue.Checked = true;
            this.rdoTVTValue.Location = new System.Drawing.Point(110, 7);
            this.rdoTVTValue.Name = "rdoTVTValue";
            this.rdoTVTValue.Size = new System.Drawing.Size(62, 20);
            this.rdoTVTValue.TabIndex = 1;
            this.rdoTVTValue.TabStop = true;
            this.rdoTVTValue.Text = "Value";
            this.rdoTVTValue.UseVisualStyleBackColor = true;
            // 
            // lblTaxName
            // 
            this.lblTaxName.AutoSize = true;
            this.lblTaxName.BackColor = System.Drawing.Color.Transparent;
            this.lblTaxName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxName.ForeColor = System.Drawing.Color.Black;
            this.lblTaxName.Location = new System.Drawing.Point(13, 14);
            this.lblTaxName.MandatoryLabelText = "*";
            this.lblTaxName.Margin = new System.Windows.Forms.Padding(0);
            this.lblTaxName.Name = "lblTaxName";
            this.lblTaxName.NormalLabelText = "Tax Name";
            this.lblTaxName.Size = new System.Drawing.Size(98, 21);
            this.lblTaxName.TabIndex = 110;
            // 
            // chkInterStateTax
            // 
            this.chkInterStateTax.AutoSize = true;
            this.chkInterStateTax.Location = new System.Drawing.Point(345, 97);
            this.chkInterStateTax.Name = "chkInterStateTax";
            this.chkInterStateTax.Size = new System.Drawing.Size(125, 20);
            this.chkInterStateTax.TabIndex = 109;
            this.chkInterStateTax.Text = "InterState Tax";
            this.chkInterStateTax.UseVisualStyleBackColor = true;
            // 
            // lblTaxValueType
            // 
            this.lblTaxValueType.BackColor = System.Drawing.Color.Transparent;
            this.lblTaxValueType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxValueType.Location = new System.Drawing.Point(516, 56);
            this.lblTaxValueType.MandatoryLabelText = "*";
            this.lblTaxValueType.Margin = new System.Windows.Forms.Padding(0);
            this.lblTaxValueType.Name = "lblTaxValueType";
            this.lblTaxValueType.NormalLabelText = "Tax Value Type";
            this.lblTaxValueType.Size = new System.Drawing.Size(121, 24);
            this.lblTaxValueType.TabIndex = 101;
            // 
            // cmbAppliedon
            // 
            this.cmbAppliedon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppliedon.FormattingEnabled = true;
            this.cmbAppliedon.Location = new System.Drawing.Point(624, 95);
            this.cmbAppliedon.Name = "cmbAppliedon";
            this.cmbAppliedon.Size = new System.Drawing.Size(198, 24);
            this.cmbAppliedon.TabIndex = 109;
            // 
            // lblAppliedOn
            // 
            this.lblAppliedOn.BackColor = System.Drawing.Color.Transparent;
            this.lblAppliedOn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppliedOn.Location = new System.Drawing.Point(516, 95);
            this.lblAppliedOn.MandatoryLabelText = "*";
            this.lblAppliedOn.Margin = new System.Windows.Forms.Padding(0);
            this.lblAppliedOn.Name = "lblAppliedOn";
            this.lblAppliedOn.NormalLabelText = "Applied On";
            this.lblAppliedOn.Size = new System.Drawing.Size(105, 22);
            this.lblAppliedOn.TabIndex = 105;
            // 
            // txtTaxValue
            // 
            this.txtTaxValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtTaxValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxValue.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtTaxValue.Location = new System.Drawing.Point(620, 14);
            this.txtTaxValue.MaxLength = 15;
            this.txtTaxValue.Name = "txtTaxValue";
            this.txtTaxValue.Size = new System.Drawing.Size(198, 21);
            this.txtTaxValue.TabIndex = 111;
            this.txtTaxValue.Tag = null;
            this.txtTaxValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtTaxValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtTaxValue.Click += new System.EventHandler(this.txtTaxValue_Click);
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocumentType.FormattingEnabled = true;
            this.cmbDocumentType.Location = new System.Drawing.Point(201, 449);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Size = new System.Drawing.Size(170, 24);
            this.cmbDocumentType.TabIndex = 108;
            this.cmbDocumentType.TextChanged += new System.EventHandler(this.cmbDocumentType_TextChanged);
            // 
            // lbldocumenttype
            // 
            this.lbldocumenttype.BackColor = System.Drawing.Color.Transparent;
            this.lbldocumenttype.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldocumenttype.Location = new System.Drawing.Point(81, 452);
            this.lbldocumenttype.MandatoryLabelText = "*";
            this.lbldocumenttype.Margin = new System.Windows.Forms.Padding(0);
            this.lbldocumenttype.Name = "lbldocumenttype";
            this.lbldocumenttype.NormalLabelText = "Document Type";
            this.lbldocumenttype.Size = new System.Drawing.Size(119, 24);
            this.lbldocumenttype.TabIndex = 102;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.c1Sizer1);
            this.groupBox1.Location = new System.Drawing.Point(60, 331);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 174);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Tax";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TaxActionButton);
            this.groupBox2.Location = new System.Drawing.Point(60, 511);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(908, 53);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            // 
            // btnArticleDelete
            // 
            this.btnArticleDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArticleDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnArticleDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArticleDelete.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArticleDelete.ForeColor = System.Drawing.Color.Black;
            this.btnArticleDelete.Image = global::Spectrum.BO.Properties.Resources.del_icon;
            this.btnArticleDelete.Location = new System.Drawing.Point(86, 36);
            this.btnArticleDelete.MaximumSize = new System.Drawing.Size(15, 15);
            this.btnArticleDelete.MinimumSize = new System.Drawing.Size(15, 15);
            this.btnArticleDelete.Name = "btnArticleDelete";
            this.btnArticleDelete.Size = new System.Drawing.Size(15, 15);
            this.btnArticleDelete.TabIndex = 104;
            this.btnArticleDelete.UseVisualStyleBackColor = false;
            this.btnArticleDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnArticleDelete.Click += new System.EventHandler(this.btnArticleDelete_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(62, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(157, 23);
            this.txtSearch.TabIndex = 109;
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(254, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(58, 16);
            this.lblSearch.TabIndex = 110;
            this.lblSearch.Tag = null;
            this.lblSearch.Text = "Search";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearch.TextDetached = true;
            this.lblSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // frmTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 579);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbDocumentType);
            this.Controls.Add(this.lbldocumenttype);
            this.Controls.Add(this.btnArticleDelete);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.dgTax);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTax";
            this.Text = "Tax Master";
            this.Load += new System.EventHandler(this.frmTax_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTax_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgTax)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
            this.c1Sizer1.ResumeLayout(false);
            this.c1Sizer1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lblSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ActionButtons TaxActionButton;
        private Controls.FlexGrid dgTax;
        //private System.Windows.Forms.RadioButton radioButton3;
        //private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton rdoTaxExclusive;
        //private System.Windows.Forms.RadioButton rdoTVTPercent1;
        //private System.Windows.Forms.RadioButton rdoTVTValue1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Controls.LabelMandatory lblTaxValue;
        private Controls.TextBox txtTaxName;
        private C1.Win.C1Sizer.C1Sizer c1Sizer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.LabelMandatory lblTaxValueType;
        private System.Windows.Forms.RadioButton rdoTaxInclusive;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.Button btnArticleDelete;
        private Controls.LabelMandatory lbldocumenttype;
        private System.Windows.Forms.ComboBox cmbDocumentType;
        private System.Windows.Forms.ComboBox cmbAppliedon;
        private Controls.LabelMandatory lblAppliedOn;
        private System.Windows.Forms.CheckBox chkInterStateTax;
        private Controls.LabelMandatory lblTaxName;
        private Controls.TextBox txtTaxValue;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdoTVTPercent;
        private System.Windows.Forms.RadioButton rdoTVTValue;
        private System.Windows.Forms.TextBox txtSearch;
        private Controls.Label lblSearch;
    }
}