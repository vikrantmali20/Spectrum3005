namespace Spectrum.Controls
{
    partial class LabelMandatory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MandatoryText = new Spectrum.Controls.Label(this.components);
            this.LabelText = new Spectrum.Controls.Label(this.components);
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.MandatoryText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LabelText)).BeginInit();
            this.flowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MandatoryText
            // 
            this.MandatoryText.AutoSize = true;
            this.MandatoryText.BackColor = System.Drawing.Color.Transparent;
            this.MandatoryText.BorderColor = System.Drawing.Color.Transparent;
            this.MandatoryText.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.MandatoryText.ForeColor = System.Drawing.Color.Red;
            this.MandatoryText.Location = new System.Drawing.Point(32, 0);
            this.MandatoryText.Margin = new System.Windows.Forms.Padding(0);
            this.MandatoryText.Name = "MandatoryText";
            this.MandatoryText.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.MandatoryText.Size = new System.Drawing.Size(17, 19);
            this.MandatoryText.TabIndex = 1;
            this.MandatoryText.Tag = null;
            this.MandatoryText.Text = "*";
            this.MandatoryText.TextDetached = true;
            this.MandatoryText.Value = "*";
            this.MandatoryText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // LabelText
            // 
            this.LabelText.AutoSize = true;
            this.LabelText.BackColor = System.Drawing.Color.Transparent;
            this.LabelText.BorderColor = System.Drawing.Color.Transparent;
            this.LabelText.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.LabelText.ForeColor = System.Drawing.Color.Black;
            this.LabelText.Location = new System.Drawing.Point(0, 0);
            this.LabelText.Margin = new System.Windows.Forms.Padding(0);
            this.LabelText.Name = "LabelText";
            this.LabelText.Size = new System.Drawing.Size(32, 13);
            this.LabelText.TabIndex = 0;
            this.LabelText.Tag = null;
            this.LabelText.Text = "Text";
            this.LabelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelText.TextDetached = true;
            this.LabelText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // flowPanel
            // 
            this.flowPanel.Controls.Add(this.LabelText);
            this.flowPanel.Controls.Add(this.MandatoryText);
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(121, 21);
            this.flowPanel.TabIndex = 3;
            // 
            // LabelMandatory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowPanel);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LabelMandatory";
            this.Size = new System.Drawing.Size(121, 21);
            ((System.ComponentModel.ISupportInitialize)(this.MandatoryText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LabelText)).EndInit();
            this.flowPanel.ResumeLayout(false);
            this.flowPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Label LabelText;
        public Label MandatoryText;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
    }
}
