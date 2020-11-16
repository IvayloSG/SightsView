namespace SightsView.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ExploreController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
