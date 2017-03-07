using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Food.Common;
using Food.Core;
using Food.Dal;
using Food.Services.DTOs;
using Mehdime.Entity;
using Food = Food.Core.Food;
using ValidationException = FluentValidation.ValidationException;

namespace Food.Services
{
    public class FoodService : IFoodService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IFoodDTOValidatorFactory _foodDtoValidatorFactory;

        public FoodService(IDbContextScopeFactory dbContextScopeFactory, IFoodDTOValidatorFactory foodDtoValidatorFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _foodDtoValidatorFactory = foodDtoValidatorFactory;
        }
        public IEnumerable<FoodDTO> GetAll()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return
                    dbContextScope.DbContexts.Get<FoodContext>()
                        .Foods.Include(p => p.Measures)
                        .ToList()
                        .Select( p => p.ToDto());
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="inDiaryEntries"></param>
        /// <param name="foodId"></param>
        /// <returns>List of Food Dtos by page</returns>
        
        public IEnumerable<FoodDTO> Search(int? foodId = null, int page = 0, int pageSize = 5, bool inDiaryEntries = false)
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
                return query.ToList().Select(p => p.ToDto());
            }
        }

        
        public int AddFood(FoodDTO foodDto)
        {
            var  factory = _foodDtoValidatorFactory.Create(true);
            factory.ValidateAndThrow(foodDto);
            using (var context = _dbContextScopeFactory.Create())
            {
                var food = foodDto.ToEntity();
                var newFood = context.DbContexts.Get<FoodContext>().Foods.Add(food);
                context.SaveChanges();
                return newFood.FoodId;
            }
        }

        public int UpdateFood(FoodDTO foodDto)
        {
            var factory = _foodDtoValidatorFactory.Create(false);
            factory.ValidateAndThrow(foodDto);
            
            using (var context = _dbContextScopeFactory.Create())
            {
                var foodContext = context.DbContexts.Get<FoodContext>();
                var food = foodContext.Foods.Include(x => x.Measures).SingleOrDefault(q => q.FoodId == foodDto.FoodId);
                if (food == null)
                {
                    throw new ValidationException(string.Empty).CreateError(nameof(food.FoodId),
                        ErrorCode.ENTITY_WAS_NOT_FOUND, ErrorCode.ENTITY_WAS_NOT_FOUND);
                }
                if (foodDto.MeasureDtos.AreDuplicates(p => p.Description))
                {
                    var list = foodDto.MeasureDtos.GetDuplicates(p => p.Description);
                    string message = string.Empty;
                    foreach (var item in list)
                    {
                        message += item.Description + "-";
                    }
                    throw new ValidationException(string.Empty).CreateError(nameof(foodDto.MeasureDtos),
                        ErrorCode.NEW_ENTITY_IS_ALREADY_IN_REPOSITORY, ErrorCode.NEW_ENTITY_IS_ALREADY_IN_REPOSITORY);
                }
                var dtoToAddList =
                    foodDto.MeasureDtos.Where(dto => dto.MeasureId == 0 && food.Measures.All(entity => entity.Description != dto.Description)).ToList();
                var dtoToUpdate = foodDto.MeasureDtos.Where(dto => food.Measures.Any(entity => entity.MeasureId == dto.MeasureId)).ToList();
                var entitiesToRemove =
                    food.Measures.Where(entity => foodDto.MeasureDtos.All(dto => dto.MeasureId != entity.MeasureId)).ToList();
                
                foreach (var entity in entitiesToRemove)
                {
                    foodContext.Measures.Remove(entity);
                }
                foreach (var dto in dtoToAddList)
                {
                    food.Measures.Add(new Measure
                    {
                        Description = dto.Description,
                        Calories = dto.Calories,
                        TotalFat = dto.TotalFat
                    });
                }
                foreach (var dto in dtoToUpdate)
                {
                    var measure = food.Measures.Single(p => p.MeasureId == dto.MeasureId);
                    measure.Description = dto.Description;
                    measure.Calories = dto.Calories;
                    measure.TotalFat = dto.TotalFat;
                }
                context.SaveChanges();
                return foodDto.FoodId;
            }
        }
    }
}
