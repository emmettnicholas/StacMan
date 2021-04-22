using System.Text.Json.Serialization;

namespace StackExchange.StacMan.InboxItems
{
    /// <summary>
    /// item_type
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// comment
        /// </summary>
        [JsonPropertyName("comment")]
        Comment,

        /// <summary>
        /// chat_message
        /// </summary>
        [JsonPropertyName("chat_message")]
        ChatMessage,

        /// <summary>
        /// new_answer
        /// </summary>
        [JsonPropertyName("new_answer")]
        NewAnswer,

        /// <summary>
        /// careers_message
        /// </summary>
        [JsonPropertyName("careers_message")]
        CareersMessage,

        /// <summary>
        /// careers_invitations
        /// </summary>
        [JsonPropertyName("careers_invitations")]
        CareersInvitations,

        /// <summary>
        /// meta_question
        /// </summary>
        [JsonPropertyName("meta_question")]
        MetaQuestion,

        /// <summary>
        /// post_notice
        /// </summary>
        [JsonPropertyName("post_notice")]
        PostNotice,

        /// <summary>
        /// moderator_message
        /// </summary>
        [JsonPropertyName("moderator_message")]
        ModeratorMessage
    }
}
