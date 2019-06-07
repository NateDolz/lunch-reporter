using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace scripting
{
    class Program
    {
        static readonly string csvPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "workspace/scripting/files");
        static readonly string restaurantsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "workspace/lunch-reporter/database/scaffolds");
        static void Main(string[] args)
        {
            PopulateRestaurantsWithRatings();
        }

        static List<User> InitUserObject() {
            return new List<User>() {new User("a27a0e12", "peter-ratings.csv"),
                                     new User("ae18913a", "marg-ratings.csv"),
                                     new User("7d573e2e", "willy-ratings.csv"),
                                     new User("a2c16f94", "james-ratings.csv"),
                                     new User("7f9ccde1", "nathan-ratings.csv"),
                                     new User("cd773e21", "daniel-ratings.csv"),
                                     new User("c2f94c8b", "luke-ratings.csv"),
                                     new User("b1159b73", "lane-ratings.csv"),
                                     new User("3e7739ce", "frank-ratings.csv"),
                                     new User("a49c2f7c", "mike-ratings.csv"),
                                     new User("d0512e9b", "haden-ratings.csv"),
                                     new User("362388e0", "aaron-ratings.csv"),};
        }

        static void PopulateRestaurantsWithRatings() {                        
            var restaurantsJson = JsonConvert.DeserializeObject<Restaurant[]>(GetRestaurantsData());
            var userList = InitUserObject();
            foreach(var user in userList){
                using(var userReader = new StreamReader(File.OpenRead($"{csvPath}/{user.FileName}"))){
                    string line;
                    while((line = userReader.ReadLine()) != null){
                        var parts = line.Split(',');
                        var restaurant = restaurantsJson.FirstOrDefault(res => res.name == parts[0]);
                        if(restaurant == null) continue;
                        restaurant.ratings[user.Id] = Double.Parse(parts[1]);
                    }
                }
            }
            WriteRestaurantData(restaurantsJson);
        }

        static string GetRestaurantsData() {
            using(var restaurantStream = new StreamReader(File.OpenRead($"{restaurantsPath}/restaurants.json"))) {
                var restaurantsString = restaurantStream.ReadToEnd();            
                return restaurantsString;
            }
        }

        static void WriteRestaurantData(Restaurant[] data) {
            using(var writer = new StreamWriter(File.OpenWrite($"{restaurantsPath}/restaurants-final.json"))) {
                string restaurantData = JsonConvert.SerializeObject(data);
                writer.Write(restaurantData);
            }
        }
    }

    class User {
        public string Id {get;}
        public string FileName {get;}
        public User(string id, string fileName) {
            Id = id;
            FileName = fileName;
        }
    }

    class Restaurant {
        public string name {get;set;}
        public string _id{get;set;}
        public dynamic ratings {get;set;}
    }    
}
