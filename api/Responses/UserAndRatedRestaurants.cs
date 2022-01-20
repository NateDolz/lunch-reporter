using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using LunchReporterAPI.Models;

namespace LunchReporterAPI.Responses
{
    public class UserAndRatedRestaurants : User
    {
        /// <summary>
        /// The data carrier for a user and his related restaurant ratings.
        /// Contains all of the user's information <see cref="T:LunchReporterApi.Models.User"/>
        /// This class is not to be persisted to mongo and is used only for data response's.
        /// </summary>
        public UserAndRatedRestaurants(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Ratings = new Dictionary<string, RatedRestaurant>();
        }

        /// <summary>
        /// The user's rating dictionary.
        /// The key is the restaurant's uid.
        /// The value is the rated restaurant object for each restaurant the user has rated.
        /// </summary>    
        [JsonProperty("ratings")]
        public IDictionary<String, RatedRestaurant> Ratings { get; set; }
    }
}