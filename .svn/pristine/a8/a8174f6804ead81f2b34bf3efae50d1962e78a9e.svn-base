using System;
using System.Windows.Forms;
using Spectrum.Models;
using System.Configuration;
using Spectrum.BL;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using Spectrum.Models.Mappers;

namespace Spectrum.BO
{
    internal class SpectrumApplication
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            CommonModel.ConnectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
            //IUnityContainer container = new UnityContainer();

            CommonModel.SiteCode = "0002";//"0000111";/;
            CommonModel.UserID = "admin";
            CommonModel.CurrentDate = DateTime.Now;

            CommonFunc.SpectrumResources = GlobalizationResourceManager.GetResourceManager();
            EntityToModelMappings.ConfigureMappers();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDISpectrumLite());
        }

        private void SpectrumApplication_NetworkAvailabilityChanged(object sender, NetworkAvailableEventArgs e)
        {
            MessageBox.Show("MyApplication_NetworkAvailabilityChanged");
            //Trace.WriteLine("Network Availability Changed: Is Network Available =" + e.IsNetworkAvailable.ToString());
        }

        private void SpectrumApplication_Startup(object sender, StartupEventArgs e)
        {
            MessageBox.Show("SpectrumApplication_Startup");
        }

        private void SpectrumApplication_UnhandledException(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("SpectrumApplication_UnhandledException");

            //Trace.WriteLine("Unhandled Exception" + e.Exception.ToString());
            //Interaction.MsgBox("Unhandled exception: " + e.Exception.Message, MsgBoxStyle.OkOnly, "Blogger Backup - Unhandled Exception");
            e.ExitApplication = false;
        }

    }
}
