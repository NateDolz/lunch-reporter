using System;
using MongoDB.Driver;
using DataModels;
using System.Collections.Generic;

namespace LunchReporterAPI.Helpers
{
  /// <summary>
  /// The users helper responsible for interacting with data from the users mongo collection.
  /// </summary>
  public class UsersHelper
  {
    /// <summary>
    /// The mongo collection containing all of the users data.
    /// </summary>    
    private IMongoCollection<User> userCollection { get; }

    /// <summary>
    /// The users helper constructor.
    /// Opens a connection to the database and sets the user collection field.
    /// </summary>
    public UsersHelper()
    {
      var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_NET_CORE_LUNCH_REPORTER"));
      var database = client.GetDatabase("lunch-reporter");

      userCollection = database.GetCollection<User>("users");
    }

    /// <summary>
    /// Gets all users from the user collection.
    /// </summary>
    /// <returns>An enumerable object containing all user information.</returns>
    public IEnumerable<User> GetAllUsers() =>
      userCollection.Find(_ => true).ToEnumerable();

    /// <summary>
    /// Gets a single user from the user collection.    
    /// </summary>
    /// <param name="id">The user's uid.</param>
    /// <returns>The user object whose mongo Id cooresponds with the given uid.</returns>
    public User GetUser(string id) =>
      userCollection.Find(user => user.Id == id).SingleOrDefault();

    /// <summary>
    /// Gets a user object with all of the users raing information mapped with it.
    /// </summary>
    /// <param name="user">The user whose ratings will be mapped.</param>
    /// <param name="ratedRestaurants">All restaurants the user has a raing record for.</param>
    /// <returns>A user object with all restaurant data mapped inside it.</returns>
    public UserAndRatedRestaurants MapUserAndRatedRestaurants(User user, IEnumerable<Restaurant> ratedRestaurants)
    {
      var userAndRatings = new UserAndRatedRestaurants(user);
      foreach (var restaurant in ratedRestaurants)
      {
        userAndRatings.Ratings[restaurant.Id] = new RatedRestaurant()
        {
          Name = restaurant.Name,
          Rating = restaurant.Ratings[user.Id]
        };
      }
      return userAndRatings;
    }
  }
}