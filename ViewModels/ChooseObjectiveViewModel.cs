using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class ChooseObjectiveViewModel
    {
        public int? GameId { get; set; }

        public IEnumerable<string> ObjectiveContentTypes { get; set; }
    }
}