﻿namespace Core.Configuration
{
    public class RemConfig
    {
        public const string Rem = "Rem";

        /// <summary>
        /// Gets or sets a value indicating whether we should use Redis server
        /// </summary>
        public bool RedisEnabled { get; set; }

        /// <summary>
        /// Gets or sets Redis connection string. Used when Redis is enabled
        /// </summary>
        public string RedisConnectionString { get; set; }

        public string DbType { get; set; }

        /// <summary>
        /// UHub IdentityServer4 Authority Url
        /// </summary>
        public string Authority { get; set; }
    }
}
