using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Food.Services;
using Food.Services.DTOs;

namespace Food.WebApi.Controllers
{
    [RoutePrefix("api/Foods")]
    public class FoodsController : ApiController
    {
        public IFoodService _foodService { get; set; }
        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet]
        public IEnumerable<FoodDTO> GetFoods()
        {
            return _foodService.GetAll();
        }


        [Route("Paging/{page:int}/{pageSize:int}/{inDiaryEntries:bool}")]
        [HttpGet]
        public IEnumerable<FoodDTO> GetByQuery(int page = 0, int pageSize = 5, bool inDiaryEntries = false)
        {
            return _foodService.GetByQuery(page, pageSize, inDiaryEntries);
        }


    }
}
