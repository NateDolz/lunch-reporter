namespace LunchReporterAPI.Config
{
    /// <summary>
    /// Mongo server config object
    /// </summary>
    public class MongoConfig
    {
        /// <summary>
        /// The database name
        /// </summary>        
        public string Database { get; set; }

        /// <summary>
        /// The database host
        /// </summary>        
        public string Host { get; set; }

        /// <summary>
        /// The port to access the database host on
        /// </summary>        
        public int Port { get; set; }

        /// <summary>
        /// The database user name
        /// </summary>        
        public string User { get; set; }

        /// <summary>
        /// The database password
        /// </summary>        
        public string Password { get; set; }

        /// <summary>
        /// The fully constructed database connection string
        /// </summary>        
        public string ConnectionString => string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password)
            ? $"mongodb://{Host}:{Port}"
            : $"mongodb+srv://{User}:{Password}@{Host}";
    }
}