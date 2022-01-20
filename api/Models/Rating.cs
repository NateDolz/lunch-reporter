using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LunchReporterAPI.Models
{

    /// <summary>
    /// The data carrier for a restuarant rating.
    /// Will tie a user and restaurant together    
    /// </summary>
    public class Rating : BaseModel
    {      

        /// <summary>        
        /// The rating on a scale of 1-10 assigned to a restaurant.
        /// </summary>    
        [BsonElement("score")]
        [JsonProperty("score")]
        public int Score { get; set; }

        /// <summary>
        /// The uuid of the restaurant being rated
        /// </summary>        
        [BsonElement("restaurant_id")]
        [JsonProperty("restaurant_id")]
        public string RestaurantId { get; set; }

        /// <summary>
        /// The uuid of the user who rated
        /// </summary>        
        [BsonElement("user_id")]
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}