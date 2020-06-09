using System.ComponentModel;

namespace Spectrum.Controls
{
    public partial class Button : C1.Win.C1Input.C1Button
    {
        public Button()
        {
            InitializeComponent();
        }

        public Button(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
