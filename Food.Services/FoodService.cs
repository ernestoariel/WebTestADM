using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Food.Core;
using Food.Dal;
using Food.Services.DTOs;
using Mehdime.Entity;
using Food = Food.Core.Food;

namespace Food.Services
{
    public class FoodService : IFoodService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public FoodService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }
        public IEnumerable<FoodDTO> GetAll()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return
                    dbContextScope.DbContexts.Get<FoodContext>()
                        .Foods.Include(p => p.Measures)
                        .ToList()
                        .Select(FoodDTO.FromFood);
            }
        }

        public IEnumerable<FoodDTO> Search(int page = 0, int pageSize = 5, bool inDiaryEntries = false, int? foodId = null)
        {
            using (var context = _dbContextScopeFactory.CreateReadOnly())
            {
                var foodContext = context.DbContexts.Get<FoodContext>();

                IQueryable<Core.Food> query = foodContext.Foods.Include( x=> x.Measures);
                if (inDiaryEntries) query = query.Where(p => foodContext.DiaryEntries.Any(q => q.FoodId == p.FoodId));
                if (foodId.HasValue) query = query.Where(p => p.FoodId == foodId);
                query = query
                    .OrderBy(c => c.Description)
                    .Skip(page*pageSize)
                    .Take(pageSize);

                return query.ToList().Select(p => FoodDTO.FromFood(p));
            }
        }
    }
}
