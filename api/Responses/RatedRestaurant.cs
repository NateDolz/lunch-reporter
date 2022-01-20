using System;
using Newtonsoft.Json;
using LunchReporterAPI.Models;

namespace LunchReporterAPI.Responses
{
    /// <summary>
    /// A Data carrier for a restaurant's and name and a rating given to that restaurant.
    /// This model is a data mapping only and should not be persisted to the database.q
    /// </summary>
    public class RatedRestaurant : Restaurant
    {

        public RatedRestaurant(Restaurant restaurant)
        {
            this.Id = restaurant.Id;
            this.Name = restaurant.Name;
            this.PlaceIds = restaurant.PlaceIds;
        }

        /// <summary>
        /// The instance rating of the restaurant by a user.
        /// </summary>    
        [JsonProperty("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// The restaruants overall rating
        /// </summary>        
        public double OverallRating { get; set; }
    }
}