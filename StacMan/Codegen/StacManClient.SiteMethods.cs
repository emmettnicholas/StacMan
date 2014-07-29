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
    public partial class StacManClient : ISiteMethods
    {
        /// <summary>
        /// Stack Exchange API Sites methods
        /// </summary>
        public ISiteMethods Sites
        {
            get { return this; }
        }

        Task<StacManResponse<Site>> ISiteMethods.GetAll(string filter, int? page, int? pagesize)
        {
            ValidatePaging(page, pagesize);

            var ub = new ApiUrlBuilder(host, Version, "/sites", useHttps: false);

            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);

            return CreateApiTask<Site>(ub, HttpMethod.GET, "/sites");
        }
    }

    /// <summary>
    /// Stack Exchange API Sites methods
    /// </summary>
    public interface ISiteMethods
    {
        /// <summary>
        /// Get all the sites in the Stack Exchange network. (API Method: "/sites")
        /// </summary>
        Task<StacManResponse<Site>> GetAll(string filter = null, int? page = null, int? pagesize = null);

    }
}
