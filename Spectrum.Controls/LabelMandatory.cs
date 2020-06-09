using System.Windows.Forms;

namespace Spectrum.Controls
{
    public partial class LabelMandatory : UserControl
    {
        public LabelMandatory()
        {
            InitializeComponent();
        }

        public string NormalLabelText
        {
            get
            {
                return LabelText.Text;
            }
            set
            {
                LabelText.Text = value;
            }
        }

        public string MandatoryLabelText
        {
            get
            {
                return MandatoryText.Text;
            }
            set
            {
                MandatoryText.Text = value;
            }
        }
    }
}
