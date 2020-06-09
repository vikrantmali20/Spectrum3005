using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spectrum.Controls
{
    public partial class TextBox : C1.Win.C1Input.C1TextBox
    {
        public TextBox()
        {
            InitializeComponent();
        }

        public TextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string dataTypes = "Int64,Int32,Double";
            if (e.KeyChar == 101 || e.KeyChar==69 )
            {
                if (dataTypes.Contains(this.DataType.Name))
                {
                    e.Handled = true;
                }
            }
        }



    }
}
