using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Core;
using Food.Dal;

namespace Food.Services.Tests
{
    public class FoodContextTestSeeder<T> : DropCreateDatabaseAlways<FoodContext>
    {
        protected override void Seed(FoodContext context)
        {
            var foodList = new List<Core.Food>()
            {
                new Core.Food()
                {
                    Description = "Ensalada",
                    Measures = new List<Measure>()
                    {
                        new Measure()
                        {
                            Description = "una porcion chica",
                            Calories = 70,
                            TotalFat = 12,
                            Protein = 12,
                            Sugar = 0
                        },
                        new Measure()
                        {
                            Description = "una porcion mediana",
                            Calories = 110,
                            TotalFat = 22,
                            Protein = 16,
                            Sugar = 0
                        }
                    }
                },
                new Core.Food()
                {
                    Description = "Fideos a la bolognesa",
                    Measures = new List<Measure>()
                    {
                        new Measure()
                        {
                            Description = "porcion chica",
                            Calories = 180,
                            TotalFat = 20
                        },
                        new Measure()
                        {
                            Description = "porcion mediana",
                            Calories = 250,
                            TotalFat = 50
                        }
                    }
                }
            };
            foodList.ForEach(p => context.Foods.Add(p));
            
            base.Seed(context);
        }
    }
}
