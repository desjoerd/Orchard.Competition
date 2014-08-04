using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class ObjectiveResultsViewModel
    {
        public ObjectivePart Objective { get; set; }

        public IEnumerable<Tuple<TeamPart, ObjectiveResultPart>> ObjectiveResults { get; set; }

        public IEnumerable<ObjectiveResultPresetViewModel> Presets { get; set; }
    }
}