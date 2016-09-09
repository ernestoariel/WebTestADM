using System.Collections.Generic;

namespace Food.Dal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FoodContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var foodList = new List<Model.Food>
            {
                new Model.Food { FoodId  = 1, Description = "Chuletas cerdo"},
                new Model.Food { FoodId = 2, Description = "Ensalada normal"},
                new Model.Food { FoodId = 3, Description = "Milanesa frita"},
                new Model.Food { FoodId = 4, Description = "Papas fritas"},
                new Model.Food { FoodId = 5, Description = "Tallarines"},
                new Model.Food { FoodId = 6, Description = "Salsa bolognesa"}
            };
            foodList.ForEach(p => context.Foods.AddOrUpdate( q => q.FoodId, p));
            context.SaveChanges();
        }
    }
}
