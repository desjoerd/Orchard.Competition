using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class CompetitionSettingsPartRecord : ContentPartRecord
    {
        public virtual bool HideResults { get; set; }

        public virtual DateTime? HideResultsFromUtc { get; set; }
    }
}