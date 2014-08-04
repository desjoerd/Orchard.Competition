using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class CompetitionPermissionsAdminViewModel
    {
        public IEnumerable<Tuple<GamePart, CompetitionPermissionsPart, IEnumerable<Tuple<ObjectivePart, CompetitionPermissionsPart>>>> Permissions { get; set; }
    }
}