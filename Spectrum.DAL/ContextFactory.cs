using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.Models;
using System.Data.EntityClient;

namespace Spectrum.DAL
{
    /// <summary>
    /// A factory for data services.
    /// </summary>
    public static class ContextFactory
    {
        /// <summary>
        /// Create a new instance of the Spectrum Entities.
        /// </summary>
        /// <returns>A new instance of the Spectrum Entities.</returns>
        public static SpectrumEntities CreateContext()
        {
              var entityConnectionBuilder = new EntityConnectionStringBuilder();
              
            //Set the provider name.
              entityConnectionBuilder.Provider = "System.Data.SqlClient";

              // Set the provider-specific connection string.
              entityConnectionBuilder.ProviderConnectionString = CommonModel.ConnectionString; 

              // Set the Metadata location.
              entityConnectionBuilder.Metadata = "res://*/SpectrumModel.csdl|res://*/SpectrumModel.ssdl|res://*/SpectrumModel.msl";

              var spectrumEntities = new SpectrumEntities(entityConnectionBuilder.ConnectionString);

            return spectrumEntities;
        }

        //public static SpectrumObjectContext CreateContext1()
        //{
        //    var entityConnectionBuilder = new EntityConnectionStringBuilder();

        //    //Set the provider name.
        //    entityConnectionBuilder.Provider = "System.Data.SqlClient";

        //    // Set the provider-specific connection string.
        //    entityConnectionBuilder.ProviderConnectionString = CommonModel.ConnectionString;

        //    // Set the Metadata location.
        //    entityConnectionBuilder.Metadata = "res://*/SpectrumModel.csdl|res://*/SpectrumModel.ssdl|res://*/SpectrumModel.msl";

        //    var spectrumEntities = new SpectrumObjectContext(entityConnectionBuilder.ConnectionString);

        //    return spectrumEntities;
        //}

    }
}
