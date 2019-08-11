using System;
using System.Collections.Generic;
using DataModels;
using LunchReporterAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LunchReporterAPI.Controllers
{

  /// <summary>
  /// The restaurants controller containing all api calls related to restaurants.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class RestaurantsController : ControllerBase
  {

    /// <summary>
    /// The restaurants helper. 
    /// Will handle add data fetching and mapping for restaurant objects.
    /// </summary>    
    private RestaurantsHelper _RestaurantsHelper { get; }

    /// <summary>
    /// Creates the controller.
    /// </summary>
    /// <param name="helper">The resturants helper singleton as initialized in the startup file.</param>
    public RestaurantsController(RestaurantsHelper helper)
    {
      _RestaurantsHelper = helper;
    }

    /// <summary>
    /// Gets all restaurants and returns them to the caller.
    /// </summary>    
    /// <returns>The JSON array object containing all restaurants stored in the databse.</returns>
    [HttpGet]
    public IEnumerable<Restaurant> GetAllRestaurants() => _RestaurantsHelper.GetAllRestaurants();

    /// <summary>
    /// Gets a single restaurant and returns them to the caller.    
    /// </summary>
    /// <param name="id">The restaurant uid for the requested restaurant.</param>
    /// <returns>The restaurant data for the restaurant matching with the given uid.</returns>
    [HttpGet("{id}")]
    public Restaurant GetSingleRestaurant(string id) => _RestaurantsHelper.GetRestaurant(id);

    /// <summary>
    /// Gets a list of reccomended restaurants based on a sub set of users.
    /// </summary>
    /// <param name="users">A comma dilineated list of user uids.</param>
    /// <returns>A list of reccomended restaurants.</returns>
    [HttpGet("recommendations")]
    public IEnumerable<Restaurant> GetRecommendations(string users)
    {
      var userCollection = new List<string>(users.Split(',', StringSplitOptions.RemoveEmptyEntries));
      return _RestaurantsHelper.GetRestaurantReccomendations(userCollection);
    }
  }
}
