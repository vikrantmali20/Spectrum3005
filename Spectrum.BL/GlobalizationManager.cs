using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace Spectrum.BL
{
    public class GlobalizationResourceManager
    {
        public static ResourceManager GetResourceManager()
        {
            try
            {
                // Gets a reference to the same assembly that contains the type that is creating the ResourceManager.
                Assembly assembly;

                // Gets a reference to a different assembly.
                assembly = Assembly.Load("Spectrum.Globalization");

                CultureInfo cultureInfo = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                // Creates the ResourceManager.
                return new ResourceManager("Spectrum.Globalization.SpectrumLite", assembly);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
