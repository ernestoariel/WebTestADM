using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Core;

namespace Food.Dal.TestData
{
    public class FoodContextSeeder : DropCreateDatabaseAlways<FoodContext>
    {
        protected override void Seed(FoodContext context)
        {
            var foodList = new List<Core.Food>
            {
                new Core.Food
                {
                    Description = "Chuletas cerdo",
                    Measures = new List<Measure>
                    {
                        new Measure
                        {
                            Calories = 50,
                            Description = "Un plato con 2 chuletas promedio, 200gr c/u",
                            TotalFat = 12
                        },
                        new Measure {Calories = 100, Description = "Un plato con 4 chuletas promedio", TotalFat = 20}
                    }
                },
                new Core.Food
                {
                    Description = "Ensalada normal",
                    Measures = new List<Measure>
                    {
                        new Measure {Calories = 30, Description = "Acompañamiento a plato ppal.", TotalFat = 21}
                    }
                },
                new Core.Food {Description = "Milanesa frita"},
                new Core.Food {Description = "Papas fritas"},
                new Core.Food {Description = "Tallarines"},
                new Core.Food {Description = "Salsa bolognesa"},
                new Core.Food {Description = "Empanadas al horno salteñas"}
            };

            var diaryList = new List<Core.Diary>
            {
                new Core.Diary
                {
                    CurrentDate = new DateTime(2016, 1, 2),
                    ApiUser = new ApiUser {UserName = "admin", Password = "pass1"},
                    DiaryEntries = new List<DiaryEntry>
                    {
                        new DiaryEntry
                        {
                            Food = foodList.Single(p => p.Description == "Chuletas cerdo"),
                            Quantity = 1,
                            Measure =
                                foodList.Single(p => p.Description == "Chuletas cerdo")
                                    .Measures.Single(q => q.Calories == 100),

                        },
                        new DiaryEntry
                        {
                            Food = foodList.Single(p => p.Description == "Ensalada normal"),
                            Quantity = 2,
                            Measure =
                                foodList.Single(p => p.Description == "Ensalada normal")
                                    .Measures.Single(q => q.Calories == 30),

                        }
                    }
                },
            };
            foodList.ForEach(p => context.Foods.AddOrUpdate(q => q.FoodId, p));
            diaryList.ForEach( p => context.Diaries.AddOrUpdate( q=> q.DiaryId, p));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
