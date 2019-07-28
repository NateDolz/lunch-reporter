using System;
using System.Collections.Generic;
using DataModels;
using MongoDB.Driver;

namespace LunchReporterAPI.Helpers
{
  public class RestaurantsHelper
  {
    private IMongoCollection<Restaurant> restaurantCollection { get; }

    public RestaurantsHelper()
    {
      var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_NET_CORE_LUNCH_REPORTER"));
      var database = client.GetDatabase("lunch-reporter");

      restaurantCollection = database.GetCollection<Restaurant>("restaurants");
    }

    public IEnumerable<Restaurant> GetAllRestaurants()
    {
      return restaurantCollection.Find(_ => true).ToEnumerable();
    }
  }
}