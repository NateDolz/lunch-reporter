using System;
using MongoDB.Driver;
using LunchReporterAPI.Models;
using System.Collections.Generic;
using LunchReporterAPI.Responses;

namespace LunchReporterAPI.Database
{
    /// <summary>
    /// The users helper responsible for interacting with data from the users mongo collection.
    /// </summary>
    public class UsersRepo : BaseRepo
    {

        /// <summary>
        /// Data context used to gain access to data collections
        /// </summary>
        private DataContext _context;

        /// <summary>
        /// The users helper constructor.
        /// Opens a connection to the database and sets the user collection field.
        /// </summary>
        public UsersRepo(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all users from the user collection.
        /// </summary>
        /// <returns>An enumerable object containing all user information.</returns>
        public IEnumerable<User> GetAllUsers() =>
          _context.Users.Find(_ => true).ToEnumerable();

        /// <summary>
        /// Gets a single user from the user collection.    
        /// </summary>
        /// <param name="id">The user's uid or discord id.</param>
        /// <returns>The user object whose mongo Id cooresponds with the given uid.</returns>
        public User GetUser(string id) =>
          _context.Users.Find(user => user.DiscordId == id).SingleOrDefault();

        /// <summary>
        /// Creates a single user entry into the users collection
        /// </summary>
        /// <param name="user">The user to be inserted should include a first_name and a last_name</param>
        public void CreateUser(User user)
        {
            Console.Out.WriteLine(user.DiscordName);
            Console.Out.WriteLine(user.DiscordId);
            _context.Users.InsertOne(user);
        }

        /// <summary>
        /// Create a restaurant rating by a given user
        /// </summary>
        /// <param name="userId">The users discord ID</param>
        /// <param name="restaurantID">The name of the restaurant</param>
        /// <param name="rating">The rating</param>
        public void UserRateRestaurant(string userId, string restaurantName, int rating)
        {
            var restaurants = _context.Restaurants.Find(rest => rest.Name == restaurantName);
            if (restaurants.CountDocuments() == 0)
            {
                _context.Restaurants.InsertOne(new Restaurant()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = restaurantName
                });
                restaurants = _context.Restaurants.Find(rest => rest.Name == restaurantName);
            }

            var restaurant = _context.Restaurants.Find(rest => rest.Name == restaurantName).Single();

            _context.Ratings.InsertOne(new Rating()
            {
                RestaurantId = restaurant.Id,
                UserId = userId,
                Score = rating,
            });
        }

        /// <summary>
        /// Does a full update on the user.
        /// </summary>
        /// <param name="id">The user's discord id</param>
        /// <param name="updatedUser">The fully updated user object.</param>
        public void UpdateUser(string id, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(user => user.DiscordId, updatedUser.DiscordId);
            _context.Users.ReplaceOne(filter, updatedUser);
        }
    }
}