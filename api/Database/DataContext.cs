using MongoDB.Driver;
using System;
using LunchReporterAPI.Config;
using LunchReporterAPI.Models;
using LunchReporterAPI.Database.Seeds;

namespace LunchReporterAPI.Database
{
    /// <summary>
    /// The Mongo Data context
    /// </summary>
    public class DataContext
    {
        /// <summary>
        /// The database connection
        /// </summary>
        private readonly IMongoDatabase _db;

        /// <summary>
        /// A collection of restaurants to be queried
        /// </summary>
        /// <typeparam name="Restaurant">The restaurant data model</typeparam>
        public IMongoCollection<Restaurant> Restaurants => _db.GetCollection<Restaurant>("restaurants");

        /// <summary>
        /// A collection of users to be queried
        /// </summary>
        /// <typeparam name="User">The user data model</typeparam>        
        public IMongoCollection<User> Users => _db.GetCollection<User>("users");

        /// <summary>
        /// Constructs a new Data context using the database configuration loaded at run time
        /// </summary>
        /// <param name="config">The mongo config object</param>
        public DataContext(MongoConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);

            if (Environment.GetEnvironmentVariable("SEED") == "DO")
            {
                Seed.Run(client);
            }
        }
    }
}