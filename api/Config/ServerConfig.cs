namespace LunchReporterAPI.Config
{
    /// <summary>
    /// Server config used to setup the mongo connection
    /// </summary>
    public class ServerConfig
    {
        /// <summary>
        /// Mongo config object
        /// </summary>
        public MongoConfig Mongo { get; set; } = new MongoConfig();
    }
}