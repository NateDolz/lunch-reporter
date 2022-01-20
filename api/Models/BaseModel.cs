using System;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LunchReporterAPI.Models
{
    /// <summary>
    /// Base class for all model objects
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// The mongo uid for the data model.
        /// </summary>    
        [BsonId]
        [JsonProperty("_id")]
        public string Id { get; set; } = GenerateGuid();

        /// <summary>
        /// generates a new guid for use by the model
        /// </summary>
        /// <returns>A guid string</returns>
        private static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}