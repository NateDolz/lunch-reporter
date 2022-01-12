using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LunchReporterAPI.Models
{
    /// <summary>
    /// The data model for a user.
    /// This is the mapping for both the mongo data and the data response.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user's uid as it is represented in mongo.
        /// </summary>    
        [BsonId]
        [JsonProperty("_id")]
        public string Id { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>    
        [BsonElement("firstname")]
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>    
        [BsonElement("lastname")]
        [JsonProperty("lastname")]
        public string LastName { get; set; }
    }
}
