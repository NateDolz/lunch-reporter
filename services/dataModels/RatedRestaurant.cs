using System;
using Newtonsoft.Json;

namespace DataModels
{
  /// <summary>
  /// A Data carrier for a restaurant's and name and a rating given to that restaurant.
  /// This model is a data mapping only and should not be persisted to the database.q
  /// </summary>
  public class RatedRestaurant
  {

    /// <summary>
    /// The english readable name of the restaurant.
    /// </summary>   
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// The instance rating of the restaurant by a user.
    /// </summary>    
    [JsonProperty("rating")]
    public double Rating { get; set; }
  }
}