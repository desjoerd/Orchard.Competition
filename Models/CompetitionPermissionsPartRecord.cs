using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class CompetitionPermissionsPartRecord : ContentPartRecord
    {
        public CompetitionPermissionsPartRecord()
        {
            TeamPermissions = new List<CompetitionTeamPermissionsRecord>();
        }

        public virtual IList<CompetitionTeamPermissionsRecord> TeamPermissions { get; set; }
    }
}