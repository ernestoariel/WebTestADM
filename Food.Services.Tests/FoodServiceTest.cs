using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Castle.Windsor;
using FluentAssertions;
using Food.Common;
using Food.Dal;
using Food.Services.DTOs;
using Xunit;
using Food.WebApi.Installers;

namespace Food.Services.Tests
{
    [Collection("CastleServices")]
    public class FoodServiceTest
    {
        CastleServicesFixture _castleServicesFixture { get; set; }
        private IFoodService _foodService;

        public FoodServiceTest(CastleServicesFixture castleServicesFixture)
        {
            _castleServicesFixture = castleServicesFixture;
            _foodService = _castleServicesFixture.Container.Resolve<IFoodService>();
        }

        [Theory]
        [AutoMoqData]
        public void Should_Not_Save_Duplicated_Food(FoodDTO foodDto)
        {
            foodDto.FoodId = 0;
            //foodDto.MeasureDtos.ForEach( p => p.MeasureId = 0);
            var id = _foodService.AddFood(foodDto);
            id.Should().BeGreaterThan(0);
            _foodService.Invoking(p => p.AddFood(foodDto)).ShouldThrow<DbUpdateException>().And.GetBaseException().Message.Should().Contain("IX_Food_Description");
            
            
            //var list = foodService.GetAll().ToList();
            //list.Should().NotBeEmpty().And.Contain( p=> p.Description == foodDto.Description).And.HaveCount(2);
            //List<Core.Food> list;
            //using (var context = new FoodContext())
            //{
            //    list = context.Foods.Where(p => p.Description == "Ensalada").ToList();
            //}
            //list.Should().NotBeEmpty().And.HaveCount(1).And.Contain(p => p.Description == "Ensalada");
        }

        [Theory]
        [AutoMoqData]
        public void Should_Not_Save_Duplicated_Measure_In_Food(FoodDTO foodDto)
        {
            foodDto.FoodId = 0;
            foodDto.MeasureDtos.ForEach(q =>
            {
                q.Description = "SameDescription";
                q.MeasureId = 0;
            });
            _foodService.Invoking(p => p.AddFood(foodDto)).ShouldThrow<FluentValidation.ValidationException>().And.GetBaseException().Message.Should().Contain(ErrorCode.DUPLICATED_VALUES);
        }

        [Theory]
        [AutoMoqData]
        public void Should_Update_Measures_Collection(FoodDTO foodDto, MeasureDTO measureDto1, MeasureDTO measureDto2)
        {
            foodDto.FoodId = 0;
            foodDto.MeasureDtos.ForEach( p=> p.MeasureId = 0);
            measureDto1.MeasureId = 0;
            measureDto2.MeasureId = 0;
            var measureCount = foodDto.MeasureDtos.Count;
            var id = _foodService.AddFood(foodDto);
            var resultList = _foodService.Search(id).ToList();

            resultList.Should().HaveCount(1);

            var dto = resultList[0];

            var updatedElement = dto.MeasureDtos.ToList()[0];
            updatedElement.Calories = updatedElement.Calories + 100;

            var toRemove = dto.MeasureDtos.ToList()[1];
            dto.MeasureDtos.Remove(toRemove);

            dto.MeasureDtos.Add(measureDto1);
            dto.MeasureDtos.Add(measureDto2);

            _foodService.UpdateFood(dto);

            resultList = _foodService.Search(id).ToList();
            resultList.Should().HaveCount(1);

            resultList[0].MeasureDtos.Should().HaveCount(measureCount + 1);
            resultList[0].MeasureDtos.Where(p => p.Description == measureDto1.Description).Should().HaveCount(1);
            resultList[0].MeasureDtos.Where(p => p.Description == measureDto2.Description).Should().HaveCount(1);
            resultList[0].MeasureDtos.Single(p => p.MeasureId == updatedElement.MeasureId)
                .Calories.Should()
                .Be(updatedElement.Calories);
            resultList[0].MeasureDtos.Where(p => p.MeasureId == toRemove.MeasureId).Should().BeEmpty();
        }
    }
}
