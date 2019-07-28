using System;
using MongoDB.Driver;
using DataModels;
using System.Collections.Generic;

namespace LunchReporterAPI.Helpers
{
  public class UsersHelper
  {
    private IMongoCollection<User> userCollection { get; }

    public UsersHelper()
    {
      var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_NET_CORE_LUNCH_REPORTER"));
      var database = client.GetDatabase("lunch-reporter");

      userCollection = database.GetCollection<User>("users");
    }

    public IEnumerable<User> GetAllUsers()
    {
      return userCollection.Find(_ => true).ToEnumerable();
    }
  }
}