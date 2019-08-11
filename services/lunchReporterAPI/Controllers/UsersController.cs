using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataModels;
using LunchReporterAPI.Helpers;

namespace LunchReporterAPI.Controllers
{

  /// <summary>
  /// The users controller containing all api handlers for users data.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    /// <summary>
    /// The users helper containing all data fetches and mappers for users data.
    /// </summary>    
    private UsersHelper _UsersHelper { get; }

    /// <summary>
    /// The restaurants helper containing all data fetches and mappers for restaurants data.
    /// </summary>    
    private RestaurantsHelper _RestaurantsHelper { get; }

    /// <summary>
    /// The Users Controller constructor.
    /// </summary>
    /// <param name="userHelper">The user helper singleton as initialized in the startup file.</param>
    /// <param name="restaurantHelper">The restaurants singleton as initialized in the startup file.</param>
    public UsersController(UsersHelper userHelper, RestaurantsHelper restaurantHelper)
    {
      _UsersHelper = userHelper;
      _RestaurantsHelper = restaurantHelper;
    }

    /// <summary>
    /// Gets all users and returns them to the caller.
    /// </summary>
    /// <returns>A JSON Array object with a list of all user information.</returns>
    [HttpGet]
    public IEnumerable<User> GetAllUsers() => _UsersHelper.GetAllUsers();

    /// <summary>
    /// Gets a single user object and returns it to the caller.
    /// </summary>
    /// <param name="id">The uid of the requested user.</param>
    /// <returns>The single user object with the given uid.</returns>
    [HttpGet("{id}")]
    public User GetSingleUser(string id) => _UsersHelper.GetUser(id);

    /// <summary>
    ///  Gets a single user object and all related ratings and returns it to the caller.
    /// </summary>
    /// <param name="id">The user's uid whose ratings are being requested.</param>
    /// <returns>The user's information with all of his ratings information attached.</returns>
    [HttpGet("{id}/ratings")]
    public UserAndRatedRestaurants GetUserRatings(string id)
    {
      var user = _UsersHelper.GetUser(id);
      var ratedRestaurants = _RestaurantsHelper.GetRestaurantsRatedByUser(id);
      return _UsersHelper.MapUserAndRatedRestaurants(user, ratedRestaurants);
    }
  }
}
