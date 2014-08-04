using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class CompetitionSettingsPart : ContentPart<CompetitionSettingsPartRecord>
    {
        public DateTime? HideResultsFromUtc {
            get { return Record.HideResultsFromUtc; }
            set { Record.HideResultsFromUtc = value; }
        }

        public bool HideResults {
            get { return Record.HideResults; }
            set { Record.HideResults = value; }
        }
    }
}