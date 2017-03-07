using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Food.Dal;
using Food.WebApi.Installers;

namespace Food.Services.Tests
{
    public class CastleServicesFixture : IDisposable
    {
        public IWindsorContainer Container { get; private set; }    

        public CastleServicesFixture()
        {
            Database.SetInitializer(new FoodContextTestSeeder<FoodContext>());
            Container = new WindsorContainer();
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));
            Container.Install(new WindsorWebApiInstaller());
        }


        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
