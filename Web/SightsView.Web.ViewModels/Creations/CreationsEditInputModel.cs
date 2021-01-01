namespace SightsView.Web.ViewModels.Creations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CreationsEditInputModel : IMapFrom<Creation>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [StringLength(150, ErrorMessage = GlobalConstants.CreationTitleLengthError, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = GlobalConstants.CreationDescriptionLengthError, MinimumLength = 3)]
        public string Description { get; set; }

        public string Privacy { get; set; }

        public string CountryName { get; set; }

        public string CategoryName { get; set; }

        public string CreatorId { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public IList<SelectListItem> Countries { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Creation, CreationsEditInputModel>()
                .ForMember(x => x.Privacy, opt => opt.MapFrom(
                    x => x.IsPrivate == true ? "Private" : "Public"));
        }
    }
}
