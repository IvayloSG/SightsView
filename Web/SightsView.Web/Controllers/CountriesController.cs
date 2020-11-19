namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CountriesController : Controller
    {
        public IActionResult Index(int id)
        {
            return this.View();
        }
    }
}
