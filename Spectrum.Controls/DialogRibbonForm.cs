using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Spectrum.Controls
{
    public partial class DialogRibbonForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        public DialogRibbonForm()
        {
            InitializeComponent();
        }

        public DialogRibbonForm(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
