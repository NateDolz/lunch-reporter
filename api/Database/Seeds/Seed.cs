using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using MongoDB.Driver;
using LunchReporterAPI.Models;

namespace LunchReporterAPI.Database.Seeds
{
    /// <summary>
    /// Seeder class to be run if the seed environment variable is passed to the application
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// Path to the Scaffolds directory
        /// </summary>
        static readonly string scaffoldsPath = "./Scaffolds";

        /// <summary>
        /// users collection object
        /// </summary>
        static IMongoCollection<User> userCollection;

        /// <summary>
        /// restaurants collection
        /// </summary>
        static IMongoCollection<Restaurant> restaurantCollection;

        /// <summary>
        /// array of restaurants to be seeded
        /// </summary>
        static Restaurant[] restaurants;

        /// <summary>
        /// array of users to be seeded
        /// </summary>
        static User[] users;

        /// <summary>
        /// Run the seeder command to full the database
        /// </summary>
        /// <param name="client">The mongo client to be used to seed the db</param>
        public static void Run(MongoClient client)
        {
            try
            {
                InitializeDbCollections(client);
                ReadScaffolds();
                ScaffoldDatabase();
            }
            catch (Exception e)
            {
                System.Console.Error.WriteLine("Unexpected Error: ");
                System.Console.Error.Write(e.Message);
            }
        }

        /// <summary>
        /// Initialize access to db collections and create those collection if they are not present
        /// </summary>
        /// <param name="client">Mongo client for access to the database</param>
        static void InitializeDbCollections(MongoClient client)
        {
            var database = client.GetDatabase("lunch-reporter");
            if (database == null)
                throw new Exception("lunch-reporter database could not be opened");

            userCollection = database.GetCollection<User>("users");
            if (userCollection == null)
            {
                database.CreateCollection("users");
                userCollection = database.GetCollection<User>("users");
            }

            restaurantCollection = database.GetCollection<Restaurant>("restaurants");
            if (restaurantCollection == null)
            {
                database.CreateCollection("restaurants");
                restaurantCollection = database.GetCollection<Restaurant>("restaurants");
            }

            if (userCollection == null || restaurantCollection == null)
                throw new Exception("one or more of the database collections could not be opened");
        }

        /// <summary>
        /// Read scaffold json from the provided directory
        /// </summary>
        static void ReadScaffolds()
        {
            var usersJsonString = File.ReadAllText(Path.Combine(scaffoldsPath, "users.json"));
            var restaurantsJsonString = File.ReadAllText(Path.Combine(scaffoldsPath, "restaurants.json"));
            users = JsonConvert.DeserializeObject<User[]>(usersJsonString);
            restaurants = JsonConvert.DeserializeObject<Restaurant[]>(restaurantsJsonString);
        }

        /// <summary>
        /// Actually scaffold db collections
        /// </summary>
        static void ScaffoldDatabase()
        {
            userCollection.InsertMany(users.AsEnumerable());
            restaurantCollection.InsertMany(restaurants.AsEnumerable());
        }
    }
}
