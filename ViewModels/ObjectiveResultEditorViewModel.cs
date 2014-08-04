using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class ObjectiveResultEditorViewModel
    {
        public IEnumerable<ObjectiveResultPresetViewModel> Presets { get; set; }

        [Required]
        public int ObjectiveId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}