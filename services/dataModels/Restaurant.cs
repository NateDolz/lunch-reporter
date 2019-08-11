using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DataModels
{
  /// <summary>
  /// The data model for a restaurant.
  /// This data contains mappers both for the mongo database and a data response.
  /// </summary>
  public class Restaurant
  {
    /// <summary>
    /// The mongo uid for the restaurant.
    /// </summary>    
    [BsonId]
    [JsonProperty("_id")]
    public string Id { get; set; }

    /// <summary>
    /// The English readable name for the restaurant.
    /// </summary>    
    [BsonElement("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// The restaurant's ratings dictionary.
    /// The Key is the mongo uid for the user who made the rating.
    /// The Value is the double rating.
    /// </summary>    
    [BsonElement("ratings")]
    [JsonProperty("ratings")]
    public Dictionary<string, double> Ratings { get; set; }
  }
}