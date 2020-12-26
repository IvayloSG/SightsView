namespace SightsView.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
