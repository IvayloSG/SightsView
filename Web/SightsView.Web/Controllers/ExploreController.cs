namespace SightsView.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Services.Data.Contracts;

    public class ExploreController : Controller
    {
        private readonly ICreationsService creationsService;

        public ExploreController(ICreationsService creationsService)
        {
            this.creationsService = creationsService;
        }

        public IActionResult Index(string id)
        {
            return this.View();
        }
    }
}
