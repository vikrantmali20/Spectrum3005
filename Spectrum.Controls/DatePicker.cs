using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Spectrum.Controls
{
    public partial class DatePicker : C1.Win.C1Input.C1DateEdit
    {
        public DatePicker()
        {
            InitializeComponent();
        }

        public DatePicker(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
