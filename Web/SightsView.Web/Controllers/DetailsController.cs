namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class DetailsController : Controller
    {
        public IActionResult Index(string id)
        {
            return this.View();
        }
    }
}
