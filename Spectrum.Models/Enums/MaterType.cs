using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Spectrum.Models.Enums
{
    public enum MaterType
    {
        Country=101,
        State =102,
        City=103,
        VendorPaymentTerms,
        VendorShipmentType,
        sitedefaultEan
 
    }


    public enum ActionResultType
    {
        OK,
        Cancel,
        Yes,
        No,
        Exit
    }

    public enum enumCharacteristics
    {
        [Description("SpectrumLite")]
        defaultProfileName ,
        [Description("Size")]
        size ,
        [Description("Colour")]
        colour ,
        [Description("Style")]
        style ,
        [Description("Fabric")]
        fabric  
    }




}
