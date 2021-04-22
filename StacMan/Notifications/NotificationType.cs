using System.Text.Json.Serialization;

namespace StackExchange.StacMan.Notifications
{
    /// <summary>
    /// notification_type
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// generic
        /// </summary>
        [JsonPropertyName("generic")]
        Generic,

        /// <summary>
        /// profile_activity
        /// </summary>
        [JsonPropertyName("profile_activity")]
        ProfileActivity,

        /// <summary>
        /// bounty_expired
        /// </summary>
        [JsonPropertyName("bounty_expired")]
        BountyExpired,

        /// <summary>
        /// bounty_expires_in_one_day
        /// </summary>
        [JsonPropertyName("bounty_expires_in_one_day")]
        BountyExpiresInOneDay,

        /// <summary>
        /// bounty_expires_in_three_days
        /// </summary>
        [JsonPropertyName("bounty_expires_in_three_days")]
        BountyExpiresInThreeDays,

        /// <summary>
        /// badge_earned
        /// </summary>
        [JsonPropertyName("badge_earned")]
        BadgeEarned,

        /// <summary>
        /// reputation_bonus
        /// </summary>
        [JsonPropertyName("reputation_bonus")]
        ReputationBonus,

        /// <summary>
        /// accounts_associated
        /// </summary>
        [JsonPropertyName("accounts_associated")]
        AccountsAssociated,

        /// <summary>
        /// new_privilege
        /// </summary>
        [JsonPropertyName("new_privilege")]
        NewPrivilege,

        /// <summary>
        /// post_migrated
        /// </summary>
        [JsonPropertyName("post_migrated")]
        PostMigrated,

        /// <summary>
        /// moderator_message
        /// </summary>
        [JsonPropertyName("moderator_message")]
        ModeratorMessage,

        /// <summary>
        /// registration_reminder
        /// </summary>
        [JsonPropertyName("registration_reminder")]
        RegistrationReminder,

        /// <summary>
        /// edit_suggested
        /// </summary>
        [JsonPropertyName("edit_suggested")]
        EditSuggested,

        /// <summary>
        /// substantive_edit
        /// </summary>
        [JsonPropertyName("substantive_edit")]
        SubstantiveEdit
    }
}
