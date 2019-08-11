using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using MongoDB.Driver;
using DataModels;

namespace scaffoldScript
{
  class Program
  {
    static readonly string scaffoldsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "workspace/lunch-reporter/database/scaffolds");
    static IMongoCollection<User> userCollection;
    static IMongoCollection<Restaurant> restaurantCollection;
    static Restaurant[] restaurants;
    static User[] users;

    static void Main(string[] args)
    {
      try
      {
        InitializeDbCollections();
        ReadScaffolds();
        ScaffoldDatabase();
      }
      catch (Exception e)
      {
        System.Console.Error.WriteLine("Unexpected Error: ");
        System.Console.Error.Write(e.Message);
      }
    }

    static void InitializeDbCollections()
    {
      var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_CONNECTION_NET_CORE_LUNCH_REPORTER"));
      if (client == null) throw new Exception("Mongo client could not be initialized");

      var database = client.GetDatabase("lunch-reporter");
      if (database == null) throw new Exception("lunch-reporter database could not be opened");

      userCollection = database.GetCollection<User>("users");
      restaurantCollection = database.GetCollection<Restaurant>("restaurants");
      if (userCollection == null || restaurantCollection == null)
        throw new Exception("one or more of the database collections could not be opened");
    }

    static void ReadScaffolds()
    {
      var usersJsonString = File.ReadAllText(Path.Combine(scaffoldsPath, "users.json"));
      var restaurantsJsonString = File.ReadAllText(Path.Combine(scaffoldsPath, "restaurants.json"));
      users = JsonConvert.DeserializeObject<User[]>(usersJsonString);
      restaurants = JsonConvert.DeserializeObject<Restaurant[]>(restaurantsJsonString);
    }

    static void ScaffoldDatabase()
    {
      userCollection.InsertMany(users.AsEnumerable());
      restaurantCollection.InsertMany(restaurants.AsEnumerable());
    }
  }
}
