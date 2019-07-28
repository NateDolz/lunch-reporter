using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DataModels
{
  public class User
  {
    [BsonId]
    [JsonProperty("_id")]
    public string Id { get; set; }

    [BsonElement("firstname")]
    [JsonProperty("firstname")]
    public string FirstName { get; set; }

    [BsonElement("lastname")]
    [JsonProperty("lastname")]
    public string LastName { get; set; }
  }
}
