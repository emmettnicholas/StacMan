using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Posts
{
    /// <summary>
    /// post_type
    /// </summary>
    public enum PostType
    {
        /// <summary>
        /// question
        /// </summary>
        [JsonPropertyName("question")]
        Question,

        /// <summary>
        /// answer
        /// </summary>
        [JsonPropertyName("answer")]
        Answer
    }
}