namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Sights;

    public class SightsController : Controller
    {
        private readonly IPhotographersService photographersService;

        public SightsController(IPhotographersService photographersService)
        {
            this.photographersService = photographersService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                id = currentUser;
            }

            var viewModel = await this.photographersService.GetPhotographerByIdAsync<SightsUserInfoViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult EditProfileInformation()
        {
            return this.View();
        }
    }
}
