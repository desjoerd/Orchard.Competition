using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class CompetitionSettingsEditorViewModel
    {
        public string Date { get; set; }
        public string Time { get; set; }

        public bool HideResults { get; set; }
    }
}