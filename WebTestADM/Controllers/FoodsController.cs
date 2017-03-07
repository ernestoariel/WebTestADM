using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Http.Results;
using Food.Services;
using Food.Services.DTOs;
using Food.WebApi.Plumbing;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace Food.WebApi.Controllers
{
    [RoutePrefix("api/Foods")]
    public class FoodsController : ApiController
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();


        public IFoodService _foodService { get; set; }
        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<FoodDTO>))]
        public IHttpActionResult GetFoods()
        {
            var customResult = new FoodActionResult<IEnumerable<FoodDTO>>(Request, _foodService.GetAll());
            return customResult;
        }

        //IEnumerable<FoodDTO>
        [Route("Paging/{page:int}/{pageSize:int}/{inDiaryEntries:bool}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<FoodDTO>))]
        public IHttpActionResult GetByQuery(int page = 0, int pageSize = 5, bool inDiaryEntries = false)
        {

            return new FoodActionResult<IEnumerable<FoodDTO>>(Request,
                _foodService.Search(null, page, pageSize, inDiaryEntries));
        }


        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FoodDTO))]
        public IHttpActionResult Get(int id)
        {
            var foodDtoList = _foodService.Search(id, 0, 1, false);

            if (foodDtoList == null || !foodDtoList.Any())
            {
                return new FoodActionResult<FoodDTO>(Request, null, HttpStatusCode.NotFound);
            }
            return new FoodActionResult<FoodDTO>(Request, _foodService.Search(id, 0, 1, false).Single());
        }


        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        public IHttpActionResult Add(FoodDTO foodDto)
        {
            var foodId = _foodService.AddFood(foodDto);
            return new FoodActionResult<int>(Request, foodId);
        }

        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        public IHttpActionResult Update(FoodDTO foodDto)
        {
            var foodId = _foodService.UpdateFood(foodDto);
            return new FoodActionResult<int>(Request, foodId);
        }
    }
}
