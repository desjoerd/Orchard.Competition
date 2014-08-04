using DeSjoerd.Competition.Models;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Drivers
{
    public class CompetitionPermissionsPartDriver : ContentPartDriver<CompetitionPermissionsPart>
    {
        protected override DriverResult Display(CompetitionPermissionsPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_CompetitionPermissions_Edit", () => shapeHelper.Parts_CompetitionPermissions_Edit(Model: part));
        }
    }
}