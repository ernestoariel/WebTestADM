using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Food.Services;
using Food.Services.DTOs;
using Food.WebApi.Plumbing;
using NLog;

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
        public IHttpActionResult GetFoods()
        {
            var customResult = new FoodActionResult<IEnumerable<FoodDTO>>(Request, _foodService.GetAll());
            return customResult;
        }

        //IEnumerable<FoodDTO>
        [Route("Paging/{page:int}/{pageSize:int}/{inDiaryEntries:bool}")]
        [HttpGet]
        public IHttpActionResult GetByQuery(int page = 0, int pageSize = 5, bool inDiaryEntries = false)
        {

            return new FoodActionResult<IEnumerable<FoodDTO>>(Request,
                _foodService.Search(page, pageSize, inDiaryEntries, null));
        }


        public IHttpActionResult Get(int id)
        {
            var foodDtoList = _foodService.Search(0, 1, false, id);

            if (foodDtoList == null || !foodDtoList.Any())
            {
                return new FoodActionResult<FoodDTO>(Request, null, HttpStatusCode.NotFound);
            }
            return new FoodActionResult<FoodDTO>(Request, _foodService.Search(0, 1, false, id).Single());
        }


        [HttpPost]
        public void Add(FoodDTO foodDto)
        {
            int dib = 0;
            var result = 23 / dib;
        }


    }
}
