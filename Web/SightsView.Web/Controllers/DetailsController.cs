namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;

    public class DetailsController : Controller
    {
        private readonly IDetailsService detailsService;

        public DetailsController(IDetailsService detailsService)
        {
            this.detailsService = detailsService;
        }

        public async Task<IActionResult> Add(string id)
        {
            return this.View();
        }
    }
}
