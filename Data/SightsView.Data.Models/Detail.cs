namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Detail
    {
        public Detail()
        {
            this.Creations = new HashSet<Creation>();
        }

        public int Id { get; set; }

        [MaxLength(10)]
        public string Apereture { get; set; }

        public int? ShutterSpeed { get; set; }

        [MaxLength(10)]
        public string ISO { get; set; }

        [MaxLength(20)]
        public string Resolution { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }
    }
}
