namespace OpenCloud.Models.OAuth
{
    public interface IUser
    {
        /// <summary>
        /// Roblox user ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Roblox display name
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Roblox display name
        /// </summary>
        public string? Nickname { get; set; }

        /// <summary>
        /// Roblox username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Timestamp when the account was created
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Roblox user profile URL
        /// </summary>
        public string? ProfileUrl { get; set; }

        /// <summary>
        /// Avatar headshot image. Can be null if the avatar headshot image hasn't yet been generated or has been moderated.
        /// </summary>
        public string? ProfilePicture { get; set; }
    }

    public class User : IUser
    {
        /// <inheritdoc />
        public required string UserId { get; set; }

        /// <inheritdoc />
        public string? DisplayName { get; set; }

        /// <inheritdoc />
        public string? Nickname { get; set; }

        /// <inheritdoc />
        public string? Username { get; set; }

        /// <inheritdoc />
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc />
        public string? ProfileUrl { get; set; }

        /// <inheritdoc />
        public string? ProfilePicture { get; set; }
    }
}
