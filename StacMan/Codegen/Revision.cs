// <auto-generated>
//     This file was generated by a T4 template.
//     Don't change it directly as your change would get overwritten. Instead, make changes
//     to the .tt file (i.e. the T4 template) and save it to regenerate this file.
// </auto-generated>

using System;

namespace StackExchange.StacMan
{
    /// <summary>
    /// StacMan Revision, corresponding to Stack Exchange API v2's revision type
    /// http://api.stackexchange.com/docs/types/revision
    /// </summary>
    public partial class Revision : StacManType
    {
        /// <summary>
        /// body
        /// </summary>
        [Field("body")]
        public string Body { get; internal set; }

        /// <summary>
        /// comment
        /// </summary>
        [Field("comment")]
        public string Comment { get; internal set; }

        /// <summary>
        /// creation_date
        /// </summary>
        [Field("creation_date")]
        public DateTime CreationDate { get; internal set; }

        /// <summary>
        /// is_rollback
        /// </summary>
        [Field("is_rollback")]
        public bool IsRollback { get; internal set; }

        /// <summary>
        /// last_body
        /// </summary>
        [Field("last_body")]
        public string LastBody { get; internal set; }

        /// <summary>
        /// last_tags
        /// </summary>
        [Field("last_tags")]
        public string[] LastTags { get; internal set; }

        /// <summary>
        /// last_title
        /// </summary>
        [Field("last_title")]
        public string LastTitle { get; internal set; }

        /// <summary>
        /// post_id
        /// </summary>
        [Field("post_id")]
        public int PostId { get; internal set; }

        /// <summary>
        /// post_type
        /// </summary>
        [Field("post_type")]
        public Posts.PostType PostType { get; internal set; }

        /// <summary>
        /// revision_guid
        /// </summary>
        [Field("revision_guid")]
        public Guid RevisionGuid { get; internal set; }

        /// <summary>
        /// revision_number
        /// </summary>
        [Field("revision_number")]
        public int RevisionNumber { get; internal set; }

        /// <summary>
        /// revision_type
        /// </summary>
        [Field("revision_type")]
        public Revisions.RevisionType RevisionType { get; internal set; }

        /// <summary>
        /// set_community_wiki
        /// </summary>
        [Field("set_community_wiki")]
        public bool SetCommunityWiki { get; internal set; }

        /// <summary>
        /// tags
        /// </summary>
        [Field("tags")]
        public string[] Tags { get; internal set; }

        /// <summary>
        /// title
        /// </summary>
        [Field("title")]
        public string Title { get; internal set; }

        /// <summary>
        /// user
        /// </summary>
        [Field("user")]
        public ShallowUser User { get; internal set; }

    }
}
