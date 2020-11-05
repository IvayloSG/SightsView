﻿namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common.Models;

    public class Country
    {
        public Country()
        {
            this.Creations = new HashSet<Creation>();
            this.Creators = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }

        public virtual ICollection<ApplicationUser> Creators { get; set; }
    }
}