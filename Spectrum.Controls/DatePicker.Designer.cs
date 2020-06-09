namespace Spectrum.Controls
{
    partial class DatePicker:C1.Win.C1Input.C1DateEdit
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DatePicker
            // 
            // 
            // 
            // 
            this.Calendar.DayNameLength = 1;
            this.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            this.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.EditFormat.CustomFormat = "dd/MM/yyyy";
            this.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.EmptyAsNull = true;            
            this.Value = new System.DateTime(2014, 2, 27, 13, 7, 25, 117);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

    }
}
