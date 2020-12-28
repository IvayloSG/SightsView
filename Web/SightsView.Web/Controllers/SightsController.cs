namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> MySight()
        {
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = await this.photographersService.GetPhotographerByIdAsync<SightsUserInfoViewModel>(currentUser);

            return this.View(nameof(this.Index), viewModel);
        }
    }
}
