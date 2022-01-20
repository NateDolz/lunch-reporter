using System;
using System.Linq;
using System.Collections.Generic;
using LunchReporterAPI.Models;
using LunchReporterAPI.Responses;
using MongoDB.Driver;

namespace LunchReporterAPI.Database
{

    /// <summary>
    /// The ratings helper object.
    /// Handles all interactions with the ratings mongo collection. 
    /// </summary>
    public class RatingsRepo : BaseRepo
    {
        /// <summary>
        /// Data context used to gain access to data collections
        /// </summary>
        private DataContext _context { get; }

        /// <summary>
        /// The ratings helper constructor.
        /// Opens up a mongo client instance and gets the ratings collection.
        /// </summary>
        public RatingsRepo(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all ratings from the mongo collection.
        /// </summary>
        /// <returns>A Enumberable object containing all ratings.</returns>
        public IEnumerable<Rating> getAllRatings() =>
          _context.Ratings.Find(_ => true).ToEnumerable();

        /// <summary>
        /// Gets all user ratings from the mongo collection.
        /// </summary>
        /// <param name="id">The uid of the user object to fetch ratings for from the mongo collection.</param>
        /// <returns>A collection of all ratings done by the user.</returns>
        public IEnumerable<Rating> getUserRatings (string userId) =>
          _context.Ratings.Find(rating => rating.UserId == userId).ToEnumerable();

        /// <summary>
        /// Gets all restaurant ratings from the mongo collection.
        /// </summary>
        /// <param name="id">The uid of the restaurant object to fetch ratings for from the mongo collection.</param>
        /// <returns>A collection of all ratings for the restaurant.</returns>
        public IEnumerable<Rating> getRestaurantRatings (string restaurantId) =>
          _context.Ratings.Find(rating => rating.RestaurantId == restaurantId).ToEnumerable();
    }
}