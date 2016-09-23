using System.Collections.Generic;
using System.Linq;
using Food.Core;
using Mehdime.Entity;

namespace Food.Services
{
    public class FoodService : IFoodService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public FoodService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }
        public IEnumerable<Core.Food> GetAll()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return dbContextScope.DbContexts.Get<Dal.FoodContext>().Foods.ToList();
            }
        } 
    }
}
