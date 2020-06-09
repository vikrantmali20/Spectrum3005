using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Spectrum.Controls
{
    public partial class RibbonForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        public RibbonForm()
        {
            InitializeComponent();
        }

        public RibbonForm(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
