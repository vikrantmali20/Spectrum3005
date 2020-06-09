using System.ComponentModel;

namespace Spectrum.Controls
{
    public partial class ComboBox : C1.Win.C1List.C1Combo
    {
        public ComboBox()
        {
            InitializeComponent();
        }

        public ComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
