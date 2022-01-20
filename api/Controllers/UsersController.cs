using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LunchReporterAPI.Models;
using LunchReporterAPI.Database;
using LunchReporterAPI.Responses;
using LunchReporterAPI.Requests;

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
        /// The users repo containing all data fetches and mappers for users data.
        /// </summary>    
        private UsersRepo _UsersRepo { get; }

        /// <summary>
        /// The restaurants repo containing all data fetches and mappers for restaurants data.
        /// </summary>    
        private RestaurantsRepo _RestaurantsRepo { get; }

        /// <summary>
        /// The ratings repo containing all data fetches and mappers for ratings data.
        /// </summary>        
        private RatingsRepo _RatingsRepo { get; }

        /// <summary>
        /// The Users Controller constructor.
        /// </summary>
        /// <param name="userRepo">The user repo singleton as initialized in the startup file.</param>
        /// <param name="restaurantRepo">The restaurants singleton as initialized in the startup file.</param>
        public UsersController(UsersRepo usersRepo, RestaurantsRepo restaurantsRepo, RatingsRepo ratingsRepo)
        {
            _UsersRepo = usersRepo;
            _RestaurantsRepo = restaurantsRepo;
            _RatingsRepo = ratingsRepo;
        }

        /// <summary>
        /// Gets all users and returns them to the caller.
        /// </summary>
        /// <returns>A JSON Array object with a list of all user information.</returns>
        [HttpGet]
        public IEnumerable<User> GetAllUsers() => _UsersRepo.GetAllUsers();

        /// <summary>
        /// Gets a single user object and returns it to the caller.
        /// </summary>
        /// <param name="id">The discord id of the requested user.</param>
        /// <returns>The single user object with the given uid.</returns>
        [HttpGet("{id}")]
        public IActionResult asyncGetSingleUser(string id)
        {
            var user = _UsersRepo.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        ///  Gets a single user object and all related ratings and returns it to the caller.
        /// </summary>
        /// <param name="id">The user's discord id whose ratings are being requested.</param>
        /// <returns>The user's information with all of his ratings information attached.</returns>
        [HttpGet("{id}/ratings")]
        public IEnumerable<RatedRestaurant> GetUserRatings(string id) => _RestaurantsRepo.GetRestaurantsRatedByUser(id);

        /// <summary>
        ///  post a rating for a restaurant by the user
        /// </summary>
        /// <param name="id">The user's discord id whose ratings are being requested.</param>
        [HttpPost("{id}/ratings")]
        public void RateRestaurant(
            string id,
            [FromBody] UserRateRestaurantBody body)
                => _UsersRepo.UserRateRestaurant(id, body.RestaurantName, body.Rating);

        /// <summary>
        /// Creates a user object.
        /// </summary>
        /// <param name="user">The user input should include first_name and last_name </param>
        [HttpPost]
        public void CreateUser([FromBody] User user) =>
            _UsersRepo.CreateUser(user);

        /// <summary>
        /// does a full update on a user.
        /// </summary>
        /// <param name="id">The user's discord id </param>
        /// <param name="user">The user object fully updated with all fields provided.</param>
        [HttpPut("{id}")]
        public void UpdateUser(string id, [FromBody] User user) =>
            _UsersRepo.UpdateUser(id, user);
    }
}
