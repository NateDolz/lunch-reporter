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
    public class User : BaseModel
    {
        /// <summary>
        /// The user's first name.
        /// </summary>    
        [BsonElement("first_name")]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>    
        [BsonElement("last_name")]
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>    
        [BsonElement("discord_name")]
        [JsonProperty("discord_name")]
        public string DiscordName { get; set; }

        /// <summary>
        /// The user's last name.
        /// </summary>    
        [BsonElement("discord_id")]
        [JsonProperty("discord_id")]
        public string DiscordId { get; set; }
    }
}
