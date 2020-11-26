namespace SightsView.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Creation : BaseDeletableModel<string>
    {
        public Creation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = new HashSet<Like>();
            this.Tags = new HashSet<TagCreation>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(DataValidation.CreationTitleLength)]
        public string Title { get; set; }

        [MaxLength(DataValidation.CreationDescriptionLength)]
        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        [Required]
        public string StorageAddress { get; set; }

        [Required]
        public string CreationDataUrl { get; set; }

        public string CloudPublicId { get; set; }

        public int Views { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int? EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public int? DetailsId { get; set; }

        public virtual Details Details { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<TagCreation> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
