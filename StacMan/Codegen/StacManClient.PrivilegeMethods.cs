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
    public partial class StacManClient : IPrivilegeMethods
    {
        /// <summary>
        /// Stack Exchange API Privileges methods
        /// </summary>
        public IPrivilegeMethods Privileges
        {
            get { return this; }
        }

        Task<StacManResponse<Privilege>> IPrivilegeMethods.GetAll(string site, string filter, int? page, int? pagesize)
        {
            ValidateString(site, "site");
            ValidatePaging(page, pagesize);

            var ub = new ApiUrlBuilder(urlManager, Version, "/privileges", useHttps: false);

            ub.AddParameter("site", site);
            ub.AddParameter("filter", filter);
            ub.AddParameter("page", page);
            ub.AddParameter("pagesize", pagesize);

            return CreateApiTask<Privilege>(ub, HttpMethod.GET, "/privileges");
        }
    }

    /// <summary>
    /// Stack Exchange API Privileges methods
    /// </summary>
    public interface IPrivilegeMethods
    {
        /// <summary>
        /// Get all the privileges available on the site. (API Method: "/privileges")
        /// </summary>
        Task<StacManResponse<Privilege>> GetAll(string site, string filter = null, int? page = null, int? pagesize = null);

    }
}
