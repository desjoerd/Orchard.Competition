using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class CompetitionPermissionsPart : ContentPart<CompetitionPermissionsPartRecord>
    {
        public string Title
        {
            get { return this.As<TitlePart>().Title; }
        }

        public IList<CompetitionTeamPermissionsRecord> TeamPermissions
        {
            get { return Record.TeamPermissions; }
        }
    }
}