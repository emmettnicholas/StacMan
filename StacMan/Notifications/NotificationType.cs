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
        Generic,

        /// <summary>
        /// profile_activity
        /// </summary>
        ProfileActivity,

        /// <summary>
        /// bounty_expired
        /// </summary>
        BountyExpired,

        /// <summary>
        /// bounty_expires_in_one_day
        /// </summary>
        BountyExpiresInOneDay,

        /// <summary>
        /// bounty_expires_in_three_days
        /// </summary>
        BountyExpiresInThreeDays,

        /// <summary>
        /// badge_earned
        /// </summary>
        BadgeEarned,

        /// <summary>
        /// reputation_bonus
        /// </summary>
        ReputationBonus,

        /// <summary>
        /// accounts_associated
        /// </summary>
        AccountsAssociated,

        /// <summary>
        /// new privilege
        /// </summary>
        NewPrivilege,

        /// <summary>
        /// post migrated
        /// </summary>
        PostMigrated,

        /// <summary>
        /// moderator_message
        /// </summary>
        ModeratorMessage,

        /// <summary>
        /// registration_reminder
        /// </summary>
        RegistrationReminder,

        /// <summary>
        /// edit_suggested
        /// </summary>
        EditSuggested,

        /// <summary>
        /// substantive_edit
        /// </summary>
        SubstantiveEdit
    }
}
