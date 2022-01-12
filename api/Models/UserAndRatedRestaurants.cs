using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LunchReporterAPI.Models
{

    /// <summary>
    /// The data carrier for a user and his related restaurant ratings.
    /// Contains all of the user's information <see cref="T:LunchReporterApi.Models.User"/>
    /// This class is not to be persisted to mongo and is used only for data response's.
    /// </summary>
    public class UserAndRatedRestaurants : User
    {

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
        public IDictionary<string, RatedRestaurant> Ratings { get; set; }
    }
}