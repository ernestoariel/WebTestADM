using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Food.Services;

namespace Food.WebApi.Installers
{
    public class WindsorWebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());
            container.Register(Classes.FromAssembly(typeof(IFoodService).Assembly).Pick().WithService.DefaultInterfaces().LifestyleTransient()); //This method performs matching based on type name and interface's name
            container.Register(Classes.FromAssemblyNamed("Mehdime.Entity").Pick().WithServiceAllInterfaces().LifestyleTransient());
        }
    }
}
