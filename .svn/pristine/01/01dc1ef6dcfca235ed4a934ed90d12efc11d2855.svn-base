using Spectrum.Models.Enums;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmPopupMessage : Controls.DialogRibbonForm
    {
        public frmPopupMessage(MessageType messageType)
        {
            InitializeComponent();
            if (CommonFunc.Themeselect == "Theme 1")
            {
                ThemeChange();
            }
           
            switch (messageType)
            {
                case MessageType.Information:
                    btnCancel.Visible = false;
                    btnNo.Visible = false;
                    btnYes.Text = "OK";
                    break;
                case MessageType.YesNo:
                    btnCancel.Text = "No";
                    btnNo.Visible = false;
                    btnYes.Text = "Yes";
                    break;
                case MessageType.YesNoCancel:
                    btnNo.Text = "No";
                    btnYes.Text = "Yes";
                    break;
                case MessageType.OKCancel:
                    btnNo.Visible = false;
                    btnYes.Text = "OK";
                    break;
                case MessageType.AbortRetryIgnore:
                    btnCancel.Text = "Abort";
                    btnNo.Text = "Ignore";
                    btnYes.Text = "Retry";
                    break;
                case MessageType.RetryCancel:
                    btnNo.Visible = false;
                    btnYes.Text = "Retry";
                    break;
                default:
                    break;
            }
        }
        public void ThemeChange()
        {
            tablePanel.BackColor = Color.FromArgb(134, 134, 134);
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            flowPanel.BackColor = Color.FromArgb(134, 134, 134);
            MessageText.BackColor = Color.FromArgb(134, 134, 134);
            MessageText.ForeColor = Color.White;
            btnYes.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnYes.BackColor = Color.Transparent;
            btnYes.BackColor = Color.FromArgb(0, 107, 163);
            btnYes.ForeColor = Color.FromArgb(255, 255, 255);
            btnYes.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnYes.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnYes.FlatAppearance.BorderSize = 0;
            btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnNo.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnNo.BackColor = Color.Transparent;
            btnNo.BackColor = Color.FromArgb(0, 107, 163);
            btnNo.ForeColor = Color.FromArgb(255, 255, 255);
            btnNo.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnNo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnNo.FlatAppearance.BorderSize = 0;
            btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }
    }
}
