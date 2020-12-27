namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Load;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoadController : ControllerBase
    {
        private readonly ICreationsService creationsService;

        public LoadController(ICreationsService creationsService)
        {
            this.creationsService = creationsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<LoadResponseModel> LoadMoreCountry(LoadCountryInputModel input)
        {
            // TODO: Constant value
            var creationsCount = GlobalConstants.CreationsPerPage;

            var creations = await this.creationsService.GetCreationByCountryAsync<CreationsViewModel>(input.ElementId, input.PageNumber, creationsCount);

            return new LoadResponseModel { Creations = creations };
        }

        [HttpPost]
        [Authorize]
        public async Task<LoadResponseModel> LoadMoreUser(LoadUserInputModel input)
        {
            var creationsCount = GlobalConstants.CreationsPerPage;

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == input.ElementId)
            {
                var creations = await this.creationsService.GetCreationsIncudingPrivateByCreatorIdAsync<CreationsViewModel>(input.ElementId, input.PageNumber, creationsCount);
                return new LoadResponseModel { Creations = creations };
            }
            else
            {
                var creations = await this.creationsService.GetCreationsByCreatorIdAsync<CreationsViewModel>(input.ElementId, input.PageNumber, creationsCount);
                return new LoadResponseModel { Creations = creations };
            }
        }
    }
}
