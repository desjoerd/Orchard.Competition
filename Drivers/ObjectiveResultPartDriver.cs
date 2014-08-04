using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Drivers
{
    public class ObjectiveResultPartDriver : ContentPartDriver<ObjectiveResultPart>
    {
        private readonly IObjectiveService _objectiveService;
        private readonly ITeamService _teamService;

        public ObjectiveResultPartDriver(
            IObjectiveService objectiveService,
            ITeamService teamService)
        {
            this._objectiveService = objectiveService;
            this._teamService = teamService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix
        {
            get
            {
                return "ObjectiveResult";
            }
        }

        protected override DriverResult Display(ObjectiveResultPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_ObjectiveResult_SummaryAdmin", () =>
            {
                var team = _teamService.Get(part.Team.ContentItemRecord.Id, VersionOptions.Latest);

                if (team == null)
                {
                    return null;
                }
                return shapeHelper.Parts_ObjectiveResult_SummaryAdmin(ObjectiveResultPart: part, TeamPart: team);
            });
        }

        protected override DriverResult Editor(ObjectiveResultPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ObjectiveResult_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts/ObjectiveResult", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        protected override DriverResult Editor(ObjectiveResultPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new ObjectiveResultEditorViewModel();
            if (updater.TryUpdateModel(viewModel, Prefix, null, new string[] { "Presets" }))
            {
                var objective = _objectiveService.Get(viewModel.ObjectiveId, VersionOptions.Latest);
                if (objective == null)
                {
                    updater.AddModelError("ObjectiveId", T("Objective not found"));
                }
                var team = _teamService.Get(viewModel.TeamId, VersionOptions.Latest);
                if (team == null)
                {
                    updater.AddModelError("TeamId", T("Team not found"));
                }

                if (objective != null && team != null)
                {
                    part.Points = viewModel.Points;
                    part.DisplayName = viewModel.DisplayName;
                    part.Team = team.Record;
                    part.Objective = objective.Record;
                }
            }

            return Editor(part, (object)shapeHelper);
        }

        private ObjectiveResultEditorViewModel BuildEditorViewModel(ObjectiveResultPart part)
        {
            return new ObjectiveResultEditorViewModel
            {
                Points = part.Points,
                DisplayName = part.DisplayName,
                ObjectiveId = part.Objective.ContentItemRecord.Id,
                TeamId = part.Team.ContentItemRecord.Id,
                Presets = part.Objective.ObjectiveResultPresets
                                        .OrderBy(p => p.Position)
                                        .Select(p => new ObjectiveResultPresetViewModel
                                        {
                                            DisplayName = p.DisplayName,
                                            Points = p.Points
                                        })
            };
        }
    }
}