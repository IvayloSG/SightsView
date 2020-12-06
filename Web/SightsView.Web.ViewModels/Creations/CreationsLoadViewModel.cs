namespace SightsView.Web.ViewModels.Creations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SightsView.Data.Models;
    using SightsView.Web.ViewModels.Comments;

    public class CreationsLoadViewModel
    {
        public CreationsViewModel Creation { get; set; }

        public IEnumerable<CommentsAllViewModel> Comments { get; set; }
    }
}
