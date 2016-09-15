using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Food.Core.Interfases;

namespace Food.WebApi.Controllers
{
    public class FoodsController : ApiController
    {
        public IFoodService _foodService { get; set; }
        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

    }
}
