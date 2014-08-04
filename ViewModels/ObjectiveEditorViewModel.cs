using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class ObjectiveEditorViewModel
    {
        public ObjectiveEditorViewModel()
        {
            ObjectiveResultPresets = new List<ObjectiveResultPresetViewModel>();
        }

        public int GameId { get; set; }

        public IList<ObjectiveResultPresetViewModel> ObjectiveResultPresets { get; set; }
    }
}