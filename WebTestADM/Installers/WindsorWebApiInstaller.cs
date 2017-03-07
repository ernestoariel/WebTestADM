using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using Food.Services;
using Food.Services.DTOs;
using NLog;

namespace Food.WebApi.Installers
{
    public class WindsorWebApiInstaller : IWindsorInstaller
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());
            container.Register(Classes.FromAssembly(typeof (IFoodService).Assembly).InNamespace("Food.Services").WithService.DefaultInterfaces().LifestyleTransient()); //This method performs matching based on type name and interface's name
            container.Register(Classes.FromAssemblyNamed("Mehdime.Entity").Pick().WithServiceAllInterfaces().LifestyleTransient());
            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Kernel.Register(
                Component.For<IFoodDTOValidatorFactory>().AsFactory(),
                Component.For<IValidator<FoodDTO>>().ImplementedBy<FoodDTOValidator>().LifeStyle.Transient);
            AssemblyScanner.FindValidatorsInAssemblyContaining<FoodDTOValidator>().ForEach(
                validator =>
                {
                    if (validator.ValidatorType != typeof (FoodDTOValidator))
                    {
                        container.Register(
                            Component.For(validator.InterfaceType)
                                .ImplementedBy(validator.ValidatorType)
                                .LifeStyle.Transient);
                    }
                });

        }
    }
}
