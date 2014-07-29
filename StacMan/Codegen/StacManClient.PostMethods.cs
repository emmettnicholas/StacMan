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
    public partial class StacManClient : IPostMethods
    {
        /// <summary>
        /// Stack Exchange API Posts methods
        /// </summary>
        public IPostMethods Posts
        {
            get { return this; }
        }

        Task<StacManResponse<Post>> IPostMethods.GetAll(string site, string filter, int? page, int? pagesize, DateTime? fromdate, DateTime? todate, Posts.Sort? sort, DateTime? mindate, DateTime? maxdate, int? min, int? max, Order? order)
        {
            ValidateString(site, "site");
            ValidatePaging(page, pagesize);
            ValidateSortMinMax(sort, mindate: mindate, maxdate: maxdate, min: min, max: max);

            var ub = new ApiUrlBuilder(host, Version, "/posts", useHttps: false);

            ub.AddParameter("site", site);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("fromdate", fromdate);
            ub.AddParameter("todate", todate);
            ub.AddParameter("sort", sort);
            ub.AddParameter("min", mindate);
            ub.AddParameter("max", maxdate);
            ub.AddParameter("min", min);
            ub.AddParameter("max", max);
            ub.AddParameter("order", order);

            return CreateApiTask<Post>(ub, HttpMethod.GET, "/posts");
        }

        Task<StacManResponse<Post>> IPostMethods.GetByIds(string site, IEnumerable<int> ids, string filter, int? page, int? pagesize, DateTime? fromdate, DateTime? todate, Posts.Sort? sort, DateTime? mindate, DateTime? maxdate, int? min, int? max, Order? order)
        {
            ValidateString(site, "site");
            ValidateEnumerable(ids, "ids");
            ValidatePaging(page, pagesize);
            ValidateSortMinMax(sort, mindate: mindate, maxdate: maxdate, min: min, max: max);

            var ub = new ApiUrlBuilder(host, Version, String.Format("/posts/{0}", String.Join(";", ids)), useHttps: false);

            ub.AddParameter("site", site);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("fromdate", fromdate);
            ub.AddParameter("todate", todate);
            ub.AddParameter("sort", sort);
            ub.AddParameter("min", mindate);
            ub.AddParameter("max", maxdate);
            ub.AddParameter("min", min);
            ub.AddParameter("max", max);
            ub.AddParameter("order", order);

            return CreateApiTask<Post>(ub, HttpMethod.GET, "/posts/{ids}");
        }

        Task<StacManResponse<Comment>> IPostMethods.GetComments(string site, IEnumerable<int> ids, string filter, int? page, int? pagesize, DateTime? fromdate, DateTime? todate, Comments.Sort? sort, DateTime? mindate, DateTime? maxdate, int? min, int? max, Order? order)
        {
            ValidateString(site, "site");
            ValidateEnumerable(ids, "ids");
            ValidatePaging(page, pagesize);
            ValidateSortMinMax(sort, mindate: mindate, maxdate: maxdate, min: min, max: max);

            var ub = new ApiUrlBuilder(host, Version, String.Format("/posts/{0}/comments", String.Join(";", ids)), useHttps: false);

            ub.AddParameter("site", site);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("fromdate", fromdate);
            ub.AddParameter("todate", todate);
            ub.AddParameter("sort", sort);
            ub.AddParameter("min", mindate);
            ub.AddParameter("max", maxdate);
            ub.AddParameter("min", min);
            ub.AddParameter("max", max);
            ub.AddParameter("order", order);

            return CreateApiTask<Comment>(ub, HttpMethod.GET, "/posts/{ids}/comments");
        }

        Task<StacManResponse<Comment>> IPostMethods.AddComment(string site, string access_token, int id, string body, string filter, bool? preview)
        {
            ValidateString(site, "site");
            ValidateString(access_token, "access_token");
            ValidateString(body, "body");
            ValidateMinApiVersion("2.1");

            var ub = new ApiUrlBuilder(host, Version, String.Format("/posts/{0}/comments/add", id), useHttps: true);

            ub.AddParameter("site", site);
            ub.AddParameter("access_token", access_token);
            ub.AddParameter("body", body);
            ub.AddParameter("filter", filter);
            ub.AddParameter("preview", preview);

            return CreateApiTask<Comment>(ub, HttpMethod.POST, "/posts/{id}/comments/add");
        }

        Task<StacManResponse<Revision>> IPostMethods.GetRevisions(string site, IEnumerable<int> ids, string filter, int? page, int? pagesize, DateTime? fromdate, DateTime? todate)
        {
            ValidateString(site, "site");
            ValidateEnumerable(ids, "ids");
            ValidatePaging(page, pagesize);

            var ub = new ApiUrlBuilder(host, Version, String.Format("/posts/{0}/revisions", String.Join(";", ids)), useHttps: false);

            ub.AddParameter("site", site);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("fromdate", fromdate);
            ub.AddParameter("todate", todate);

            return CreateApiTask<Revision>(ub, HttpMethod.GET, "/posts/{ids}/revisions");
        }

        Task<StacManResponse<SuggestedEdit>> IPostMethods.GetSuggestedEdits(string site, IEnumerable<int> ids, string filter, int? page, int? pagesize, DateTime? fromdate, DateTime? todate, SuggestedEdits.Sort? sort, DateTime? mindate, DateTime? maxdate, Order? order)
        {
            ValidateString(site, "site");
            ValidateEnumerable(ids, "ids");
            ValidatePaging(page, pagesize);
            ValidateSortMinMax(sort, mindate: mindate, maxdate: maxdate);

            var ub = new ApiUrlBuilder(host, Version, String.Format("/posts/{0}/suggested-edits", String.Join(";", ids)), useHttps: false);

            ub.AddParameter("site", site);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);
            ub.AddParameter("fromdate", fromdate);
            ub.AddParameter("todate", todate);
            ub.AddParameter("sort", sort);
            ub.AddParameter("min", mindate);
            ub.AddParameter("max", maxdate);
            ub.AddParameter("order", order);

            return CreateApiTask<SuggestedEdit>(ub, HttpMethod.GET, "/posts/{ids}/suggested-edits");
        }
    }

    /// <summary>
    /// Stack Exchange API Posts methods
    /// </summary>
    public interface IPostMethods
    {
        /// <summary>
        /// Get all posts (questions and answers) in the system. (API Method: "/posts")
        /// </summary>
        Task<StacManResponse<Post>> GetAll(string site, string filter = null, int? page = null, int? pagesize = null, DateTime? fromdate = null, DateTime? todate = null, Posts.Sort? sort = null, DateTime? mindate = null, DateTime? maxdate = null, int? min = null, int? max = null, Order? order = null);

        /// <summary>
        /// Get all posts identified by a set of ids. Useful for when the type of post (question or answer) is not known. (API Method: "/posts/{ids}")
        /// </summary>
        Task<StacManResponse<Post>> GetByIds(string site, IEnumerable<int> ids, string filter = null, int? page = null, int? pagesize = null, DateTime? fromdate = null, DateTime? todate = null, Posts.Sort? sort = null, DateTime? mindate = null, DateTime? maxdate = null, int? min = null, int? max = null, Order? order = null);

        /// <summary>
        /// Get comments on the posts (question or answer) identified by a set of ids. (API Method: "/posts/{ids}/comments")
        /// </summary>
        Task<StacManResponse<Comment>> GetComments(string site, IEnumerable<int> ids, string filter = null, int? page = null, int? pagesize = null, DateTime? fromdate = null, DateTime? todate = null, Comments.Sort? sort = null, DateTime? mindate = null, DateTime? maxdate = null, int? min = null, int? max = null, Order? order = null);

        /// <summary>
        /// Create a new comment on the post identified by id. [auth required] (API Method: "/posts/{id}/comments/add") -- introduced in API version 2.1
        /// </summary>
        Task<StacManResponse<Comment>> AddComment(string site, string access_token, int id, string body, string filter = null, bool? preview = null);

        /// <summary>
        /// Get revisions on the set of posts in ids. (API Method: "/posts/{ids}/revisions")
        /// </summary>
        Task<StacManResponse<Revision>> GetRevisions(string site, IEnumerable<int> ids, string filter = null, int? page = null, int? pagesize = null, DateTime? fromdate = null, DateTime? todate = null);

        /// <summary>
        /// Get suggested edits on the set of posts in ids. (API Method: "/posts/{ids}/suggested-edits")
        /// </summary>
        Task<StacManResponse<SuggestedEdit>> GetSuggestedEdits(string site, IEnumerable<int> ids, string filter = null, int? page = null, int? pagesize = null, DateTime? fromdate = null, DateTime? todate = null, SuggestedEdits.Sort? sort = null, DateTime? mindate = null, DateTime? maxdate = null, Order? order = null);

    }
}
