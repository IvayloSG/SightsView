namespace SightsView.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Photographers;

    public class PhotographersController : Controller
    {
        private readonly IPhotographersService photographersService;
        private readonly UserManager<ApplicationUser> userManager;

        public PhotographersController(IPhotographersService photographersService, UserManager<ApplicationUser> userManager)
        {
            this.photographersService = photographersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var userId = currentUser == null ? string.Empty : currentUser.Id;

            var photographersList = await this.photographersService.GetAllPhotographersAsync(userId);

            var viewModel = new PhotographersLIstViewModel()
            {
                PhotographersList = photographersList,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> MostCreations()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var userId = currentUser == null ? string.Empty : currentUser.Id;

            var photographersList = await this.photographersService.GetPhotographersWithMostCreationsAsync(userId);

            var viewModel = new PhotographersLIstViewModel()
            {
                PhotographersList = photographersList,
            };

            return this.View(nameof(this.Index), viewModel);
        }

        public async Task<IActionResult> MostLikes()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var userId = currentUser == null ? string.Empty : currentUser.Id;

            var photographersList = await this.photographersService.GetPhotographersWithMostLikesAsync(userId);

            var viewModel = new PhotographersLIstViewModel()
            {
                PhotographersList = photographersList,
            };

            return this.View(nameof(this.Index), viewModel);
        }

        public async Task<IActionResult> MostFollowers()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var userId = currentUser == null ? string.Empty : currentUser.Id;

            var photographersList = await this.photographersService.GetPhotographersWithMostFollowersAsync(userId);

            var viewModel = new PhotographersLIstViewModel()
            {
                PhotographersList = photographersList,
            };

            return this.View(nameof(this.Index), viewModel);
        }

        public async Task<IActionResult> Newest()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var userId = currentUser == null ? string.Empty : currentUser.Id;

            var photographersList = await this.photographersService.GetPhotographersWithMostNewestAsync(userId);

            var viewModel = new PhotographersLIstViewModel()
            {
                PhotographersList = photographersList,
            };

            return this.View(nameof(this.Index), viewModel);
        }
    }
}
