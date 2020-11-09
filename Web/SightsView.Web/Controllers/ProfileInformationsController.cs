namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ProfileInformationsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult EditProfileInformation()
        {
            return this.View();
        }
    }
}
