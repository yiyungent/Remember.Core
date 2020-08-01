namespace Core.Configuration
{
    public class RemConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether we should use Redis server
        /// </summary>
        public bool RedisEnabled { get; set; }

        /// <summary>
        /// Gets or sets Redis connection string. Used when Redis is enabled
        /// </summary>
        public string RedisConnectionString { get; set; }
    }
}
