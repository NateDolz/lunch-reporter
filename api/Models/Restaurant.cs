using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LunchReporterAPI.Models
{
    /// <summary>
    /// The data model for a restaurant.
    /// This data contains mappers both for the mongo database and a data response.
    /// </summary>
    public class Restaurant : BaseModel
    {
        /// <summary>
        /// The English readable name for the restaurant.
        /// </summary>    
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// All possible google maps place_ids
        /// </summary>
        [BsonElement("place_ids")]
        [JsonProperty("place_ids")]
        public List<string> PlaceIds {get;set;}        
    }
}