using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DataModels
{
  public class Restaurant
  {
    [BsonId]
    [JsonProperty("_id")]
    public string Id { get; set; }

    [BsonElement("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [BsonElement("ratings")]
    [JsonProperty("ratings")]
    public Dictionary<string, double> Ratings { get; set; }
  }
}