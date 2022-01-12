using System;
using System.Collections.Generic;
using LunchReporterAPI.Models;
using LunchReporterAPI.Database;
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
        /// The restaurants repo. 
        /// Will handle add data fetching and mapping for restaurant objects.
        /// </summary>    
        private RestaurantsRepo _RestaurantsRepo { get; }

        /// <summary>
        /// Creates the controller.
        /// </summary>
        /// <param name="helper">The resturants helper singleton as initialized in the startup file.</param>
        public RestaurantsController(RestaurantsRepo repo)
        {
            _RestaurantsRepo = repo;
        }

        /// <summary>
        /// Gets all restaurants and returns them to the caller.
        /// </summary>    
        /// <returns>The JSON array object containing all restaurants stored in the databse.</returns>
        [HttpGet]
        public IEnumerable<Restaurant> GetAllRestaurants() => _RestaurantsRepo.GetAllRestaurants();

        /// <summary>
        /// Gets a single restaurant and returns them to the caller.    
        /// </summary>
        /// <param name="id">The restaurant uid for the requested restaurant.</param>
        /// <returns>The restaurant data for the restaurant matching with the given uid.</returns>
        [HttpGet("{id}")]
        public Restaurant GetSingleRestaurant(string id) => _RestaurantsRepo.GetRestaurant(id);

        /// <summary>
        /// Gets a list of reccomended restaurants based on a sub set of users.
        /// </summary>
        /// <param name="users">A comma dilineated list of user uids.</param>
        /// <returns>A list of reccomended restaurants.</returns>
        [HttpGet("recommendations")]
        public IEnumerable<Restaurant> GetRecommendations(string users)
        {
            var userCollection = new List<string>(users.Split(',', StringSplitOptions.RemoveEmptyEntries));
            return _RestaurantsRepo.GetRestaurantReccomendations(userCollection);
        }
    }
}
