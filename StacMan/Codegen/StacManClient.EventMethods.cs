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
    public partial class StacManClient : IEventMethods
    {
        /// <summary>
        /// Stack Exchange API Events methods
        /// </summary>
        public IEventMethods Events
        {
            get { return this; }
        }

        Task<StacManResponse<Event>> IEventMethods.GetRecent(string site, string access_token, string filter, int? page, int? pagesize, DateTime? since)
        {
            ValidateString(site, "site");
            ValidateString(access_token, "access_token");
            ValidatePaging(page, pagesize);

            var ub = new ApiUrlBuilder(host, Version, "/events", useHttps: true);

            ub.AddParameter("site", site);
            ub.AddParameter("access_token", access_token);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("since", since);

            return CreateApiTask<Event>(ub, HttpMethod.GET, "/events");
        }
    }

    /// <summary>
    /// Stack Exchange API Events methods
    /// </summary>
    public interface IEventMethods
    {
        /// <summary>
        /// Get recent events that have occurred on the site. Effectively a stream of new users and content. [auth required] (API Method: "/events")
        /// </summary>
        Task<StacManResponse<Event>> GetRecent(string site, string access_token, string filter = null, int? page = null, int? pagesize = null, DateTime? since = null);

    }
}
