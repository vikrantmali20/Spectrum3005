namespace Spectrum.BO
{
    partial class frmImportExportItem
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
            this.lblUploadItems = new Spectrum.Controls.Label(this.components);
            this.btnBrowse = new Spectrum.Controls.Button(this.components);
            this.btnUpload = new Spectrum.Controls.Button(this.components);
            this.txtArticleFilePath = new Spectrum.Controls.TextBox(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUploadItemCancel = new Spectrum.Controls.Button(this.components);
            this.grpEAXR = new System.Windows.Forms.GroupBox();
            this.lblSelectItemHierarchy1 = new Spectrum.Controls.Label(this.components);
            this.btnEAXRCancel = new Spectrum.Controls.Button(this.components);
            this.btnEAXRExport = new Spectrum.Controls.Button(this.components);
            this.txtEAXRItemHierarchy = new Spectrum.Controls.TextBox(this.components);
            this.grpEAH = new System.Windows.Forms.GroupBox();
            this.lblSelectItemHierarchy2 = new Spectrum.Controls.Label(this.components);
            this.btnEAHExportLastNode = new Spectrum.Controls.Button(this.components);
            this.txtEAHItemHierarchy = new Spectrum.Controls.TextBox(this.components);
            this.btnEAHExport = new Spectrum.Controls.Button(this.components);
            this.btnEAHCancel = new Spectrum.Controls.Button(this.components);
            this.grpUploadItem = new System.Windows.Forms.GroupBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnSampleUploadFile = new Spectrum.Controls.Button(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpMaterialUpload = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSampleUploadMaterial = new Spectrum.Controls.Button(this.components);
            this.label2 = new Spectrum.Controls.Label(this.components);
            this.txtMaterialFilePath = new Spectrum.Controls.TextBox(this.components);
            this.btnBrowseMaterial = new Spectrum.Controls.Button(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnUploadMaterial = new Spectrum.Controls.Button(this.components);
            this.grpMaterialExport = new System.Windows.Forms.GroupBox();
            this.btnMaterialExport = new Spectrum.Controls.Button(this.components);
            this.txtExportMaterial = new Spectrum.Controls.TextBox(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lblUploadItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArticleFilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpEAXR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectItemHierarchy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEAXRItemHierarchy)).BeginInit();
            this.grpEAH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectItemHierarchy2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEAHItemHierarchy)).BeginInit();
            this.grpUploadItem.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.grpMaterialUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterialFilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.grpMaterialExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExportMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUploadItems
            // 
            this.lblUploadItems.AutoSize = true;
            this.lblUploadItems.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadItems.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblUploadItems.ForeColor = System.Drawing.Color.Black;
            this.lblUploadItems.Location = new System.Drawing.Point(6, 19);
            this.lblUploadItems.Name = "lblUploadItems";
            this.lblUploadItems.Size = new System.Drawing.Size(94, 16);
            this.lblUploadItems.TabIndex = 5;
            this.lblUploadItems.Tag = null;
            this.lblUploadItems.Text = "Upload Items";
            this.lblUploadItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUploadItems.TextDetached = true;
            this.lblUploadItems.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(418, 19);
            this.btnBrowse.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(71, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.Black;
            this.btnUpload.Location = new System.Drawing.Point(342, 46);
            this.btnUpload.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(72, 23);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtArticleFilePath
            // 
            this.txtArticleFilePath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtArticleFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArticleFilePath.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtArticleFilePath.Location = new System.Drawing.Point(106, 19);
            this.txtArticleFilePath.Name = "txtArticleFilePath";
            this.txtArticleFilePath.ReadOnly = true;
            this.txtArticleFilePath.Size = new System.Drawing.Size(308, 21);
            this.txtArticleFilePath.TabIndex = 0;
            this.txtArticleFilePath.Tag = null;
            this.txtArticleFilePath.Value = "";
            this.txtArticleFilePath.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtArticleFilePath.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Spectrum.BO.Properties.Resources.xls;
            this.pictureBox1.Location = new System.Drawing.Point(503, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 17);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnUploadItemCancel
            // 
            this.btnUploadItemCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadItemCancel.ForeColor = System.Drawing.Color.Black;
            this.btnUploadItemCancel.Location = new System.Drawing.Point(418, 46);
            this.btnUploadItemCancel.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnUploadItemCancel.Name = "btnUploadItemCancel";
            this.btnUploadItemCancel.Size = new System.Drawing.Size(72, 23);
            this.btnUploadItemCancel.TabIndex = 3;
            this.btnUploadItemCancel.Text = "&Cancel";
            this.btnUploadItemCancel.UseVisualStyleBackColor = true;
            this.btnUploadItemCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnUploadItemCancel.Click += new System.EventHandler(this.btnUploadItemCancel_Click);
            // 
            // grpEAXR
            // 
            this.grpEAXR.Controls.Add(this.lblSelectItemHierarchy1);
            this.grpEAXR.Controls.Add(this.btnEAXRCancel);
            this.grpEAXR.Controls.Add(this.btnEAXRExport);
            this.grpEAXR.Controls.Add(this.txtEAXRItemHierarchy);
            this.grpEAXR.Location = new System.Drawing.Point(3, 95);
            this.grpEAXR.Name = "grpEAXR";
            this.grpEAXR.Size = new System.Drawing.Size(687, 60);
            this.grpEAXR.TabIndex = 1;
            this.grpEAXR.TabStop = false;
            this.grpEAXR.Text = "Export Article XLS Report";
            // 
            // lblSelectItemHierarchy1
            // 
            this.lblSelectItemHierarchy1.AutoSize = true;
            this.lblSelectItemHierarchy1.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectItemHierarchy1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblSelectItemHierarchy1.ForeColor = System.Drawing.Color.Black;
            this.lblSelectItemHierarchy1.Location = new System.Drawing.Point(9, 19);
            this.lblSelectItemHierarchy1.Name = "lblSelectItemHierarchy1";
            this.lblSelectItemHierarchy1.Size = new System.Drawing.Size(185, 16);
            this.lblSelectItemHierarchy1.TabIndex = 3;
            this.lblSelectItemHierarchy1.Tag = null;
            this.lblSelectItemHierarchy1.Text = "Select the Item Hierarchy:";
            this.lblSelectItemHierarchy1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectItemHierarchy1.TextDetached = true;
            this.lblSelectItemHierarchy1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnEAXRCancel
            // 
            this.btnEAXRCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEAXRCancel.ForeColor = System.Drawing.Color.Black;
            this.btnEAXRCancel.Location = new System.Drawing.Point(502, 16);
            this.btnEAXRCancel.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnEAXRCancel.Name = "btnEAXRCancel";
            this.btnEAXRCancel.Size = new System.Drawing.Size(75, 23);
            this.btnEAXRCancel.TabIndex = 2;
            this.btnEAXRCancel.Text = "&Cancel";
            this.btnEAXRCancel.UseVisualStyleBackColor = true;
            this.btnEAXRCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEAXRCancel.Click += new System.EventHandler(this.btnEAXRCancel_Click);
            // 
            // btnEAXRExport
            // 
            this.btnEAXRExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEAXRExport.ForeColor = System.Drawing.Color.Black;
            this.btnEAXRExport.Location = new System.Drawing.Point(422, 16);
            this.btnEAXRExport.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnEAXRExport.Name = "btnEAXRExport";
            this.btnEAXRExport.Size = new System.Drawing.Size(75, 23);
            this.btnEAXRExport.TabIndex = 1;
            this.btnEAXRExport.Text = "&Export";
            this.btnEAXRExport.UseVisualStyleBackColor = true;
            this.btnEAXRExport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEAXRExport.Click += new System.EventHandler(this.btnEAXRExport_Click);
            // 
            // txtEAXRItemHierarchy
            // 
            this.txtEAXRItemHierarchy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtEAXRItemHierarchy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEAXRItemHierarchy.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtEAXRItemHierarchy.Location = new System.Drawing.Point(200, 17);
            this.txtEAXRItemHierarchy.MaxLength = 15;
            this.txtEAXRItemHierarchy.Name = "txtEAXRItemHierarchy";
            this.txtEAXRItemHierarchy.ReadOnly = true;
            this.txtEAXRItemHierarchy.Size = new System.Drawing.Size(216, 21);
            this.txtEAXRItemHierarchy.TabIndex = 0;
            this.txtEAXRItemHierarchy.Tag = null;
            this.txtEAXRItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtEAXRItemHierarchy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtEAXRItemHierarchy.Enter += new System.EventHandler(this.txtEAXRItemHierarchy_Enter);
            // 
            // grpEAH
            // 
            this.grpEAH.Controls.Add(this.lblSelectItemHierarchy2);
            this.grpEAH.Controls.Add(this.btnEAHExportLastNode);
            this.grpEAH.Controls.Add(this.txtEAHItemHierarchy);
            this.grpEAH.Controls.Add(this.btnEAHExport);
            this.grpEAH.Controls.Add(this.btnEAHCancel);
            this.grpEAH.Location = new System.Drawing.Point(3, 161);
            this.grpEAH.Name = "grpEAH";
            this.grpEAH.Size = new System.Drawing.Size(687, 123);
            this.grpEAH.TabIndex = 2;
            this.grpEAH.TabStop = false;
            this.grpEAH.Text = "Export Article Hierarchy";
            // 
            // lblSelectItemHierarchy2
            // 
            this.lblSelectItemHierarchy2.AutoSize = true;
            this.lblSelectItemHierarchy2.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectItemHierarchy2.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblSelectItemHierarchy2.ForeColor = System.Drawing.Color.Black;
            this.lblSelectItemHierarchy2.Location = new System.Drawing.Point(15, 29);
            this.lblSelectItemHierarchy2.Name = "lblSelectItemHierarchy2";
            this.lblSelectItemHierarchy2.Size = new System.Drawing.Size(185, 16);
            this.lblSelectItemHierarchy2.TabIndex = 4;
            this.lblSelectItemHierarchy2.Tag = null;
            this.lblSelectItemHierarchy2.Text = "Select the Item Hierarchy:";
            this.lblSelectItemHierarchy2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectItemHierarchy2.TextDetached = true;
            this.lblSelectItemHierarchy2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // btnEAHExportLastNode
            // 
            this.btnEAHExportLastNode.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEAHExportLastNode.ForeColor = System.Drawing.Color.Black;
            this.btnEAHExportLastNode.Location = new System.Drawing.Point(258, 57);
            this.btnEAHExportLastNode.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnEAHExportLastNode.Name = "btnEAHExportLastNode";
            this.btnEAHExportLastNode.Size = new System.Drawing.Size(156, 23);
            this.btnEAHExportLastNode.TabIndex = 1;
            this.btnEAHExportLastNode.Text = "&Export Last Nodes";
            this.btnEAHExportLastNode.UseVisualStyleBackColor = true;
            this.btnEAHExportLastNode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEAHExportLastNode.Click += new System.EventHandler(this.btnEAHExportLastNode_Click);
            // 
            // txtEAHItemHierarchy
            // 
            this.txtEAHItemHierarchy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtEAHItemHierarchy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEAHItemHierarchy.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtEAHItemHierarchy.Location = new System.Drawing.Point(206, 27);
            this.txtEAHItemHierarchy.MaxLength = 15;
            this.txtEAHItemHierarchy.Name = "txtEAHItemHierarchy";
            this.txtEAHItemHierarchy.ReadOnly = true;
            this.txtEAHItemHierarchy.Size = new System.Drawing.Size(215, 21);
            this.txtEAHItemHierarchy.TabIndex = 0;
            this.txtEAHItemHierarchy.Tag = null;
            this.txtEAHItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtEAHItemHierarchy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtEAHItemHierarchy.Enter += new System.EventHandler(this.txtEAHItemHierarchy_Enter);
            // 
            // btnEAHExport
            // 
            this.btnEAHExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEAHExport.ForeColor = System.Drawing.Color.Black;
            this.btnEAHExport.Location = new System.Drawing.Point(418, 58);
            this.btnEAHExport.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnEAHExport.Name = "btnEAHExport";
            this.btnEAHExport.Size = new System.Drawing.Size(75, 23);
            this.btnEAHExport.TabIndex = 2;
            this.btnEAHExport.Text = "&Export";
            this.btnEAHExport.UseVisualStyleBackColor = true;
            this.btnEAHExport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEAHExport.Click += new System.EventHandler(this.btnEAHExport_Click);
            // 
            // btnEAHCancel
            // 
            this.btnEAHCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEAHCancel.ForeColor = System.Drawing.Color.Black;
            this.btnEAHCancel.Location = new System.Drawing.Point(502, 58);
            this.btnEAHCancel.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnEAHCancel.Name = "btnEAHCancel";
            this.btnEAHCancel.Size = new System.Drawing.Size(75, 23);
            this.btnEAHCancel.TabIndex = 3;
            this.btnEAHCancel.Text = "&Cancel";
            this.btnEAHCancel.UseVisualStyleBackColor = true;
            this.btnEAHCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEAHCancel.Click += new System.EventHandler(this.btnEAHCancel_Click);
            // 
            // grpUploadItem
            // 
            this.grpUploadItem.Controls.Add(this.lblMsg);
            this.grpUploadItem.Controls.Add(this.btnSampleUploadFile);
            this.grpUploadItem.Controls.Add(this.lblUploadItems);
            this.grpUploadItem.Controls.Add(this.txtArticleFilePath);
            this.grpUploadItem.Controls.Add(this.btnBrowse);
            this.grpUploadItem.Controls.Add(this.pictureBox1);
            this.grpUploadItem.Controls.Add(this.btnUploadItemCancel);
            this.grpUploadItem.Controls.Add(this.btnUpload);
            this.grpUploadItem.Location = new System.Drawing.Point(3, 3);
            this.grpUploadItem.Name = "grpUploadItem";
            this.grpUploadItem.Size = new System.Drawing.Size(687, 86);
            this.grpUploadItem.TabIndex = 0;
            this.grpUploadItem.TabStop = false;
            this.grpUploadItem.Text = "Upload Item";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(55, 53);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 16);
            this.lblMsg.TabIndex = 6;
            // 
            // btnSampleUploadFile
            // 
            this.btnSampleUploadFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSampleUploadFile.FlatAppearance.BorderSize = 0;
            this.btnSampleUploadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSampleUploadFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSampleUploadFile.ForeColor = System.Drawing.Color.Black;
            this.btnSampleUploadFile.Location = new System.Drawing.Point(524, 14);
            this.btnSampleUploadFile.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnSampleUploadFile.Name = "btnSampleUploadFile";
            this.btnSampleUploadFile.Size = new System.Drawing.Size(136, 23);
            this.btnSampleUploadFile.TabIndex = 4;
            this.btnSampleUploadFile.Text = "Sample Upload File";
            this.btnSampleUploadFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSampleUploadFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSampleUploadFile.UseVisualStyleBackColor = true;
            this.btnSampleUploadFile.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSampleUploadFile.Click += new System.EventHandler(this.btnSampleUploadFile_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.grpUploadItem);
            this.flowLayoutPanel1.Controls.Add(this.grpEAXR);
            this.flowLayoutPanel1.Controls.Add(this.grpEAH);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(705, 300);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.grpMaterialUpload);
            this.flowLayoutPanel2.Controls.Add(this.grpMaterialExport);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 309);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(705, 168);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // grpMaterialUpload
            // 
            this.grpMaterialUpload.Controls.Add(this.label1);
            this.grpMaterialUpload.Controls.Add(this.btnSampleUploadMaterial);
            this.grpMaterialUpload.Controls.Add(this.label2);
            this.grpMaterialUpload.Controls.Add(this.txtMaterialFilePath);
            this.grpMaterialUpload.Controls.Add(this.btnBrowseMaterial);
            this.grpMaterialUpload.Controls.Add(this.pictureBox2);
            this.grpMaterialUpload.Controls.Add(this.btnUploadMaterial);
            this.grpMaterialUpload.Location = new System.Drawing.Point(3, 3);
            this.grpMaterialUpload.Name = "grpMaterialUpload";
            this.grpMaterialUpload.Size = new System.Drawing.Size(687, 86);
            this.grpMaterialUpload.TabIndex = 0;
            this.grpMaterialUpload.TabStop = false;
            this.grpMaterialUpload.Text = "Upload Item";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 6;
            // 
            // btnSampleUploadMaterial
            // 
            this.btnSampleUploadMaterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSampleUploadMaterial.FlatAppearance.BorderSize = 0;
            this.btnSampleUploadMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSampleUploadMaterial.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSampleUploadMaterial.ForeColor = System.Drawing.Color.Black;
            this.btnSampleUploadMaterial.Location = new System.Drawing.Point(439, 50);
            this.btnSampleUploadMaterial.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnSampleUploadMaterial.Name = "btnSampleUploadMaterial";
            this.btnSampleUploadMaterial.Size = new System.Drawing.Size(136, 23);
            this.btnSampleUploadMaterial.TabIndex = 4;
            this.btnSampleUploadMaterial.Text = "Sample Upload File";
            this.btnSampleUploadMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSampleUploadMaterial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSampleUploadMaterial.UseVisualStyleBackColor = true;
            this.btnSampleUploadMaterial.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSampleUploadMaterial.Click += new System.EventHandler(this.btnSampleUploadMaterial_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 5;
            this.label2.Tag = null;
            this.label2.Text = "Upload Items";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.TextDetached = true;
            this.label2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtMaterialFilePath
            // 
            this.txtMaterialFilePath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtMaterialFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaterialFilePath.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtMaterialFilePath.Location = new System.Drawing.Point(106, 19);
            this.txtMaterialFilePath.Name = "txtMaterialFilePath";
            this.txtMaterialFilePath.ReadOnly = true;
            this.txtMaterialFilePath.Size = new System.Drawing.Size(308, 21);
            this.txtMaterialFilePath.TabIndex = 0;
            this.txtMaterialFilePath.Tag = null;
            this.txtMaterialFilePath.Value = "";
            this.txtMaterialFilePath.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtMaterialFilePath.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnBrowseMaterial
            // 
            this.btnBrowseMaterial.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseMaterial.ForeColor = System.Drawing.Color.Black;
            this.btnBrowseMaterial.Location = new System.Drawing.Point(418, 19);
            this.btnBrowseMaterial.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnBrowseMaterial.Name = "btnBrowseMaterial";
            this.btnBrowseMaterial.Size = new System.Drawing.Size(71, 23);
            this.btnBrowseMaterial.TabIndex = 1;
            this.btnBrowseMaterial.Text = "&Browse";
            this.btnBrowseMaterial.UseVisualStyleBackColor = true;
            this.btnBrowseMaterial.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnBrowseMaterial.Click += new System.EventHandler(this.btnBrowseMaterial_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Spectrum.BO.Properties.Resources.xls;
            this.pictureBox2.Location = new System.Drawing.Point(418, 53);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 17);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // btnUploadMaterial
            // 
            this.btnUploadMaterial.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadMaterial.ForeColor = System.Drawing.Color.Black;
            this.btnUploadMaterial.Location = new System.Drawing.Point(505, 18);
            this.btnUploadMaterial.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnUploadMaterial.Name = "btnUploadMaterial";
            this.btnUploadMaterial.Size = new System.Drawing.Size(72, 23);
            this.btnUploadMaterial.TabIndex = 2;
            this.btnUploadMaterial.Text = "&Upload";
            this.btnUploadMaterial.UseVisualStyleBackColor = true;
            this.btnUploadMaterial.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnUploadMaterial.Click += new System.EventHandler(this.btnUploadMaterial_Click);
            // 
            // grpMaterialExport
            // 
            this.grpMaterialExport.Controls.Add(this.btnMaterialExport);
            this.grpMaterialExport.Controls.Add(this.txtExportMaterial);
            this.grpMaterialExport.Location = new System.Drawing.Point(3, 95);
            this.grpMaterialExport.Name = "grpMaterialExport";
            this.grpMaterialExport.Size = new System.Drawing.Size(687, 60);
            this.grpMaterialExport.TabIndex = 1;
            this.grpMaterialExport.TabStop = false;
            this.grpMaterialExport.Text = "Export Material Report";
            // 
            // btnMaterialExport
            // 
            this.btnMaterialExport.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialExport.ForeColor = System.Drawing.Color.Black;
            this.btnMaterialExport.Location = new System.Drawing.Point(422, 16);
            this.btnMaterialExport.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnMaterialExport.Name = "btnMaterialExport";
            this.btnMaterialExport.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialExport.TabIndex = 1;
            this.btnMaterialExport.Text = "&Export";
            this.btnMaterialExport.UseVisualStyleBackColor = true;
            this.btnMaterialExport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnMaterialExport.Click += new System.EventHandler(this.btnMaterialExport_Click);
            // 
            // txtExportMaterial
            // 
            this.txtExportMaterial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtExportMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExportMaterial.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtExportMaterial.Location = new System.Drawing.Point(200, 17);
            this.txtExportMaterial.MaxLength = 15;
            this.txtExportMaterial.Name = "txtExportMaterial";
            this.txtExportMaterial.ReadOnly = true;
            this.txtExportMaterial.Size = new System.Drawing.Size(216, 21);
            this.txtExportMaterial.TabIndex = 0;
            this.txtExportMaterial.Tag = null;
            this.txtExportMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtExportMaterial.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // frmImportExportItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 512);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmImportExportItem";
            this.Text = "Import/Export ";
            this.Load += new System.EventHandler(this.frmImportExportItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImportExportItem_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.lblUploadItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArticleFilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpEAXR.ResumeLayout(false);
            this.grpEAXR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectItemHierarchy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEAXRItemHierarchy)).EndInit();
            this.grpEAH.ResumeLayout(false);
            this.grpEAH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblSelectItemHierarchy2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEAHItemHierarchy)).EndInit();
            this.grpUploadItem.ResumeLayout(false);
            this.grpUploadItem.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.grpMaterialUpload.ResumeLayout(false);
            this.grpMaterialUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterialFilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.grpMaterialExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExportMaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Label lblUploadItems;
        private Controls.Button btnBrowse;
        private Controls.Button btnUpload;
        private Controls.TextBox txtArticleFilePath;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.Button btnUploadItemCancel;
        private System.Windows.Forms.GroupBox grpEAXR;
        private Controls.Label lblSelectItemHierarchy1;
        private Controls.Button btnEAXRCancel;
        private Controls.Button btnEAXRExport;
        private Controls.TextBox txtEAXRItemHierarchy;
        private System.Windows.Forms.GroupBox grpEAH;
        private Controls.Label lblSelectItemHierarchy2;
        private Controls.Button btnEAHExportLastNode;
        private Controls.TextBox txtEAHItemHierarchy;
        private Controls.Button btnEAHExport;
        private Controls.Button btnEAHCancel;
        private System.Windows.Forms.GroupBox grpUploadItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.Button btnSampleUploadFile;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox grpMaterialUpload;
        private System.Windows.Forms.Label label1;
        private Controls.Button btnSampleUploadMaterial;
        private Controls.Label label2;
        private Controls.TextBox txtMaterialFilePath;
        private Controls.Button btnBrowseMaterial;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Controls.Button btnUploadMaterial;
        private System.Windows.Forms.GroupBox grpMaterialExport;
        private Controls.Button btnMaterialExport;
        private Controls.TextBox txtExportMaterial;
    }
}