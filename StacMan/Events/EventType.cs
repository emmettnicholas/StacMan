using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Events
{
    /// <summary>
    /// event_type
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// question_posted
        /// </summary>
        [JsonPropertyName("question_posted")]
        QuestionPosted,

        /// <summary>
        /// answer_posted
        /// </summary>
        [JsonPropertyName("answer_posted")]
        AnswerPosted,

        /// <summary>
        /// comment_posted
        /// </summary>
        [JsonPropertyName("comment_posted")]
        CommentPosted,

        /// <summary>
        /// post_edited
        /// </summary>
        [JsonPropertyName("post_edited")]
        PostEdited,

        /// <summary>
        /// user_created
        /// </summary>
        [JsonPropertyName("user_created")]
        UserCreated
    }
}
