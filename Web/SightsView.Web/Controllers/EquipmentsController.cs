namespace SightsView.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Equipments;

    public class EquipmentsController : Controller
    {
        private readonly IEquipmentsService equipmentService;
        private readonly ICreationsService creationsService;

        public EquipmentsController(IEquipmentsService equipmentService, ICreationsService creationsService)
        {
            this.equipmentService = equipmentService;
            this.creationsService = creationsService;
        }

        [Authorize]
        public IActionResult Add(string id)
        {
            // TODO: Make a redirect to edit action in case equipment exists
            var viewModel = new EquipmentsAddInputModel()
            {
                CreationId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(EquipmentsAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new EquipmentsAddInputModel()
                {
                    CreationId = input.CreationId,
                };
                return this.View(viewModel);
            }

            var creatorId = await this.creationsService.GetCreatorIdByCreationIdAsync(input.CreationId);
            var currentUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (creatorId != currentUserID)
            {
                return this.BadRequest(ExceptionMessages.InvalidUser);
            }

            var equipmentId = await this.equipmentService.AddEquipmentAsync(input.Brand, input.Model, input.Accessoaries, input.Notes);

            await this.creationsService.AddEquipmentToCreationAsync(input.CreationId, equipmentId);

            return this.RedirectToAction("Equipment", "Creations", new { id = input.CreationId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.creationsService.GetCreationModelByIdAsync<EquipmentsEditInputModel>(id);
            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CreationNotFound, id));
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EquipmentsEditInputModel input)
        {
            try
            {
                var viewModel = await this.creationsService.GetCreationModelByIdAsync<EquipmentsEditInputModel>(input.Id);
                if (!this.ModelState.IsValid)
                {
                    return this.View(viewModel);
                }

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId != input.CreatorId)
                {
                    return this.BadRequest();
                }

                if (input.EquipmentId == null)
                {
                    return this.RedirectToAction(nameof(this.Add), new { id = input.Id });
                }

                var isUpdateSuccessful = await this.equipmentService.UpdateEquipmentAsync(input.EquipmentId, input.EquipmentBrand, input.EquipmentModel, input.EquipmentAccessoaries, input.EquipmentNotes);

                return this.RedirectToAction("Equipment", "Creations", new { id = input.Id });
            }
            catch (NullReferenceException nre)
            {
                return this.BadRequest(nre.Message);
            }
        }
    }
}
