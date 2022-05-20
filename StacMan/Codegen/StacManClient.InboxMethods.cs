// <auto-generated>
//     This file was generated by a T4 template.
//     Don't change it directly as your change would get overwritten. Instead, make changes
//     to the .tt file (i.e. the T4 template) and save it to regenerate this file.
// </auto-generated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StackExchange.StacMan
{
    public partial class StacManClient : IInboxMethods
    {
        /// <summary>
        /// Stack Exchange API Inbox methods
        /// </summary>
        public IInboxMethods Inbox
        {
            get { return this; }
        }

        Task<StacManResponse<InboxItem>> IInboxMethods.Get(string access_token, string filter, int? page, int? pagesize)
        {
            ValidateString(access_token, "access_token");
            ValidatePaging(page, pagesize);

            var ub = new ApiUrlBuilder(Version, "/inbox", useHttps: true);

            ub.AddParameter("access_token", access_token);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);

            return CreateApiTask<InboxItem>(ub, HttpMethod.GET, "/inbox");
        }

        Task<StacManResponse<InboxItem>> IInboxMethods.GetUnread(string access_token, string filter, int? page, int? pagesize, DateTime? since)
        {
            ValidateString(access_token, "access_token");
            ValidatePaging(page, pagesize);

            var ub = new ApiUrlBuilder(Version, "/inbox/unread", useHttps: true);

            ub.AddParameter("access_token", access_token);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("since", since);

            return CreateApiTask<InboxItem>(ub, HttpMethod.GET, "/inbox/unread");
        }
    }

    /// <summary>
    /// Stack Exchange API Inbox methods
    /// </summary>
    public interface IInboxMethods
    {
        /// <summary>
        /// Get a user's inbox, outside of the context of a site. [auth required] (API Method: "/inbox")
        /// </summary>
        Task<StacManResponse<InboxItem>> Get(string access_token, string filter = null, int? page = null, int? pagesize = null);

        /// <summary>
        /// Get the unread items in a user's inbox, outside of the context of a site. [auth required] (API Method: "/inbox/unread")
        /// </summary>
        Task<StacManResponse<InboxItem>> GetUnread(string access_token, string filter = null, int? page = null, int? pagesize = null, DateTime? since = null);

    }
}
