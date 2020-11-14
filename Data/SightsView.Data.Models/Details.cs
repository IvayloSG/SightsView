namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Details : BaseModel<int>
    {
        public Details()
        {
            this.Creations = new HashSet<Creation>();
        }

        [MaxLength(DataValidation.DetailsAperatureLength)]
        public string Apereture { get; set; }

        [MaxLength(DataValidation.DetailsShutterSpeedLength)]
        public string ShutterSpeed { get; set; }

        [MaxLength(DataValidation.DetailsISOLength)]
        public string ISO { get; set; }

        [MaxLength(DataValidation.DetailsResolutionLength)]
        public string Resolution { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }
    }
}
