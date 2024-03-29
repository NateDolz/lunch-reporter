using System;
using System.Linq;
using System.Collections.Generic;
using LunchReporterAPI.Models;
using LunchReporterAPI.Responses;
using MongoDB.Driver;

namespace LunchReporterAPI.Database
{

    /// <summary>
    /// The restaurants helper object.
    /// Handles all interactions with the restaurants mongo collection. 
    /// </summary>
    public class RestaurantsRepo : BaseRepo
    {
        /// <summary>
        /// Data context used to gain access to data collections
        /// </summary>
        private DataContext _context { get; }

        /// <summary>
        /// The restaurants helper constructor.
        /// Opens up a mongo client instance and gets the restaurant collection.
        /// </summary>
        public RestaurantsRepo(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all restaurants from the mongo collection.
        /// </summary>
        /// <returns>A Enumberable object containing all restaurants.</returns>
        public IEnumerable<Restaurant> GetAllRestaurants() =>
          _context.Restaurants.Find(_ => true).ToEnumerable();

        /// <summary>
        /// Gets a single restaurant from the mongo collection.
        /// </summary>
        /// <param name="id">The uid of the restaurant object to fetch from the mongo collection.</param>
        /// <returns>The data for the restaurant whose mongo Id matches the given uid.</returns>
        public Restaurant GetRestaurant(string id) =>
          _context.Restaurants.Find(restaurant => restaurant.Id == id).SingleOrDefault();

        /// <summary>
        /// Gets a list of all restaurants that have been rated by the user.
        /// </summary>
        /// <param name="userID">The user uid whose rated restaurants are to be fetched.</param>
        /// <returns>An enumerable object containing all restaurants that have been rated by the user.</returns>
        public IEnumerable<RatedRestaurant> GetRestaurantsRatedByUser(string userID)
          => _context.Ratings.Find(rating => rating.UserId == userID)
            .ToEnumerable()
            .Select(rating =>
              new RatedRestaurant(_context.Restaurants.Find(res => res.Id == rating.RestaurantId).First())
              { Rating = rating.Score });


        /// <summary>
        /// Gets the top 5 restaurant's reccomended for a subset of users.
        /// The user reccomendations are decided by the following:
        ///   - Restaurant has been rated by all users.
        ///   - Restaurant has been rated higher than a 6.0 by all users
        ///   - The 5 remaining restaurants with the highest average rating among the user subset.
        /// </summary>
        /// <param name="users">
        /// An enumerable object with the user uids the reccomendations are being gathered for.
        /// </param>
        /// <returns>A list of 5 restaurants reccomended for the users.</returns>
        public IEnumerable<RatedRestaurant> GetRestaurantReccomendations(IEnumerable<string> users)
        {
            var ratings = _context.Ratings.Find(rating => users.Contains(rating.UserId)).ToEnumerable().GroupBy(x => x.RestaurantId);
            return (from rating in ratings
                    where rating.Select(x => x.Score > 6.0).All(x => x)
                    orderby rating.Select(x => x.Score).Sum() / users.Count() descending
                    select new RatedRestaurant(GetRestaurant(rating.Key))
                    { OverallRating = rating.Sum(x => x.Score) / rating.Count() }).Take(5);

        }
    }
}