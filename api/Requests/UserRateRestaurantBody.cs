using System;
using Newtonsoft.Json;

namespace LunchReporterAPI.Requests
{
    /// <summary>
    /// A Data carrier for a restaurant's and name and a rating given to that restaurant.
    /// This model is a data mapping only and should not be persisted to the database.q
    /// </summary>
    public class UserRateRestaurantBody
    {

        /// <summary>
        /// The instance rating of the restaurant by a user.
        /// </summary>    
        [JsonProperty("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// The restaruants overall rating
        /// </summary>        
        [JsonProperty("restaurant")]
        public string RestaurantName { get; set; }
    }
}