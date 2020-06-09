using System;
using System.ComponentModel;

namespace Spectrum.Models
{
    public class BaseModel
    {
        [Browsable(false)]
        public string CreatedAt { get; set; }

        [Browsable(false)]
        public string CreatedBy { get; set; }

        [Browsable(false)]
        public DateTime? CreatedOn { get; set; }

        [Browsable(false)]
        public string UpdatedAt { get; set; }

        [Browsable(false)]
        public string UpdatedBy { get; set; }

        [Browsable(false)]
        public DateTime? UpdatedOn { get; set; }

        [Browsable(false)]
        public bool? Status { get; set; }
    }
}
