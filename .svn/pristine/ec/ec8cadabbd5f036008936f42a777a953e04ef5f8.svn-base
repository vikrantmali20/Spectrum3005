using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sodexo.Factories;
using Microsoft.Practices.Unity;
using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL.RepositoryInterfaces;
using Spectrum.DAL;

namespace Spectrum.Factory
{
    public class ObjectFactory : IObjectFactory
    {
        private UnityContainer _contaier;
        private UnityContainer Container
        {
            get
            {
                if (_contaier == null)
                { InitializeContainer(); }

                return _contaier;
            }
            set { _contaier = value; }
        }
        public UnityContainer GetContainer()
        {
            if (_contaier == null)
            {
                InitializeContainer();
            }

            return _contaier;
        }

        public T Create<T>()
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InitializeContainer()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterInstance<IObjectFactory>(this);

            container.RegisterType<ISiteManager, SiteManager>();
            container.RegisterType<ISiteRepository, SiteRepository>();

        }
    }
}
