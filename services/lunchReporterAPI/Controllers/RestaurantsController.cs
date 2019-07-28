using System.Collections.Generic;
using DataModels;
using LunchReporterAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LunchReporterAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RestaurantsController : ControllerBase
  {

    private RestaurantsHelper _RestaurantsHelper { get; }

    public RestaurantsController(RestaurantsHelper helper)
    {
      _RestaurantsHelper = helper;
    }

    [HttpGet]
    public IEnumerable<Restaurant> Get()
    {
      return _RestaurantsHelper.GetAllRestaurants();
    }
  }
}
