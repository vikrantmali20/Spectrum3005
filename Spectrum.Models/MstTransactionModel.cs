using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrum.Models
{
    class MstTransactionModel:BaseModel
    {
        public string TransactionCode { get; set; }
        public string TransactionName { get; set; }
        //  public bool Status { get; set; }
        public string MainFunction { get; set; }
        public string SubFunction { get; set; }
    }
}
