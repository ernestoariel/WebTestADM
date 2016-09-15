using System.Collections.Generic;
using Food.Core.Interfases;
using Mehdime.Entity;

namespace Food.Core.Services
{
    public class FoodService : IFoodService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public FoodService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }
        public ICollection<Food> GetAll()
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

            }
            return null;
        } 
    }
}
