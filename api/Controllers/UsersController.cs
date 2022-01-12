using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LunchReporterAPI.Models;
using LunchReporterAPI.Database;

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
        /// The Users Controller constructor.
        /// </summary>
        /// <param name="userRepo">The user repo singleton as initialized in the startup file.</param>
        /// <param name="restaurantRepo">The restaurants singleton as initialized in the startup file.</param>
        public UsersController(UsersRepo usersRepo, RestaurantsRepo restaurantsRepo)
        {
            _UsersRepo = usersRepo;
            _RestaurantsRepo = restaurantsRepo;
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
        /// <param name="id">The uid of the requested user.</param>
        /// <returns>The single user object with the given uid.</returns>
        [HttpGet("{id}")]
        public User GetSingleUser(string id) => _UsersRepo.GetUser(id);

        /// <summary>
        ///  Gets a single user object and all related ratings and returns it to the caller.
        /// </summary>
        /// <param name="id">The user's uid whose ratings are being requested.</param>
        /// <returns>The user's information with all of his ratings information attached.</returns>
        [HttpGet("{id}/ratings")]
        public UserAndRatedRestaurants GetUserRatings(string id)
        {
            var user = _UsersRepo.GetUser(id);
            var ratedRestaurants = _RestaurantsRepo.GetRestaurantsRatedByUser(id);
            return _UsersRepo.MapUserAndRatedRestaurants(user, ratedRestaurants);
        }
    }
}
