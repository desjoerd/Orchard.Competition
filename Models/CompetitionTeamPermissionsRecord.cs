using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class CompetitionTeamPermissionsRecord
    {
        public virtual int Id { get; set; }

        public virtual CompetitionPermissionsPartRecord CompetitionPermissionsPartRecord { get; set; }

        public virtual TeamPartRecord TeamPartRecord { get; set; }

        public virtual string GrantedPermissions { get; set; }
    }
}