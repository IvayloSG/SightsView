namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SightsView.Common;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Likes;
    using SightsView.Web.ViewModels.Load;

    [ApiController]
    [Route("api/[controller]")]
    public class LoadController : ControllerBase
    {
        private readonly ICreationsService creationsService;

        public LoadController(ICreationsService creationsService)
        {
            this.creationsService = creationsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<LoadResponseModel> LoadMore(LoadInputModel input)
        {
            // TODO: Constant value
            var creationsCount = GlobalConstants.CreationsPerPage;

            var creations = await this.creationsService.GetCreationByCountryAsync<CreationsViewModel>(input.ElementId, input.PageNumber, creationsCount);

            return new LoadResponseModel { Creations = creations };
        }
    }
}
