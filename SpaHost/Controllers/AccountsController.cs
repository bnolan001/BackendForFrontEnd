using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaHost.Controllers
{
    public class AccountsController : Controller
    {
        /// <summary>
        /// This MVC controller will serve as the central point for the SPA to go to in order to
        /// force the user to login via the configured, Identity Server, service.
        /// </summary>
        /// <param name="redirect">
        /// The component to go to once the user has logged in through Identity Server
        /// </param>
        /// <returns>Redirect request</returns>
        [Authorize]
        public IActionResult Index(string redirect)
        {
            string path;
            switch (redirect)
            {
                case "fetch-data":
                    path = "/fetch-data";
                    break;

                case "fetch-authenticated-data":
                    path = "/fetch-authenticated-data";
                    break;

                case "guarded-route":
                    path = "/guarded-route";
                    break;

                case "counter":
                    path = "/counter";
                    break;

                default:
                    path = "/";
                    break;
            }
            return Redirect(path);
        }

        [Authorize]
        public IActionResult Logout()
        {
            return SignOut("cookies", "oidc");
        }
    }
}