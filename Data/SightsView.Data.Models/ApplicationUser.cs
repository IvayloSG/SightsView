// ReSharper disable VirtualMemberCallInConstructor
namespace SightsView.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Creations = new HashSet<Creation>();
            this.Comments = new HashSet<Comment>();
            this.Follows = new HashSet<Follow>();
            this.Followers = new HashSet<Follow>();
            this.SentMessages = new HashSet<Message>();
            this.ReceivedMessages = new HashSet<Message>();
        }

        [MaxLength(DataValidation.UserFirstNameLength)]
        public string FirstName { get; set; }

        [MaxLength(DataValidation.UserLastNameLength)]
        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? CountryId { get; set; }

        public bool IsEmailVisible { get; set; }

        public bool IsPhonelVisible { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }

        public virtual ICollection<Follow> Followers { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; }

        public virtual ICollection<Message> ReceivedMessages { get; set; }
    }
}
