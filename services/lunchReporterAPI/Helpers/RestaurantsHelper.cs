using System;
using System.Linq;
using System.Collections.Generic;
using DataModels;
using MongoDB.Driver;

namespace LunchReporterAPI.Helpers
{

  /// <summary>
  /// The restaurants helper object.
  /// Handles all interactions with the restaurants mongo collection. 
  /// </summary>
  public class RestaurantsHelper
  {
    /// <summary>
    /// The mongo collection for restaurants data.
    /// </summary>
    private IMongoCollection<Restaurant> restaurantCollection { get; }

    /// <summary>
    /// The restaurants helper constructor.
    /// Opens up a mongo client instance and gets the restaurant collection.
    /// </summary>
    public RestaurantsHelper()
    {
      var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_NET_CORE_LUNCH_REPORTER"));
      var database = client.GetDatabase("lunch-reporter");

      restaurantCollection = database.GetCollection<Restaurant>("restaurants");
    }

    /// <summary>
    /// Gets all restaurants from the mongo collection.
    /// </summary>
    /// <returns>A Enumberable object containing all restaurants.</returns>
    public IEnumerable<Restaurant> GetAllRestaurants() =>
      restaurantCollection.Find(_ => true).ToEnumerable();

    /// <summary>
    /// Gets a single restaurant from the mongo collection.
    /// </summary>
    /// <param name="id">The uid of the restaurant object to fetch from the mongo collection.</param>
    /// <returns>The data for the restaurant whose mongo Id matches the given uid.</returns>
    public Restaurant GetRestaurant(string id) =>
      restaurantCollection.Find(restaurant => restaurant.Id == id).SingleOrDefault();

    /// <summary>
    /// Gets a list of all restaurants that have been rated by the user.
    /// </summary>
    /// <param name="userID">The user uid whose rated restaurants are to be fetched.</param>
    /// <returns>An enumerable object containing all restaurants that have been rated by the user.</returns>
    public IEnumerable<Restaurant> GetRestaurantsRatedByUser(string userID) =>
      restaurantCollection.Find(restaurant => restaurant.Ratings.ContainsKey(userID)).ToEnumerable();

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
    public IEnumerable<Restaurant> GetRestaurantReccomendations(IEnumerable<string> users)
    {
      var restaurants = GetAllRestaurants();
      return (from restaurant in restaurants
              where users.Select(x => restaurant.Ratings.ContainsKey(x)).All(x => x)
              where users.Select(x => restaurant.Ratings[x] > 6.0).All(x => x)
              orderby users.Select(x => restaurant.Ratings[x]).Sum() / users.Count() descending
              select restaurant).Take(5);

    }
  }
}