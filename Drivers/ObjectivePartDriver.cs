using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeSjoerd.Competition.Extensions;

namespace DeSjoerd.Competition.Drivers
{
    public class ObjectivePartDriver : ContentPartDriver<ObjectivePart>
    {
        private readonly Lazy<IGameService> _gameService;
        private readonly Lazy<IObjectiveService> _objectiveService;
        private readonly Lazy<IObjectiveResultService> _objectiveResultService;
        private readonly Lazy<ITeamService> _teamService;
        private readonly Lazy<IAuthorizer> _authorizer;

        public ObjectivePartDriver(
            Lazy<IGameService> gameService,
            Lazy<IObjectiveService> objectiveService,
            Lazy<IObjectiveResultService> objectiveResultService,
            Lazy<ITeamService> teamService,
            Lazy<IAuthorizer> authorizer)
        {
            this._gameService = gameService;
            this._objectiveService = objectiveService;
            this._objectiveResultService = objectiveResultService;
            this._teamService = teamService;
            this._authorizer = authorizer;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix
        {
            get
            {
                return "Objective";
            }
        }

        protected override DriverResult Display(ObjectivePart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_Objective_Admin_Actions", () => shapeHelper.Parts_Objective_Admin_Actions(ObjectivePart: part)),
                ContentShape("Parts_Objective_Results", () =>
                {
                    var results = _objectiveResultService.Value.Get(part)
                        .Where(result => _authorizer.Value.Authorize(Permissions.ViewAllResults, result))
                        .ToList();

                    var teams = _teamService.Value.Get().ToList();

                    var viewModel = new ObjectiveResultsViewModel
                    {
                        Objective = part,
                        Presets = part.ObjectiveResultPresets
                                      .OrderBy(preset => preset.Position)
                                      .Select(preset => new ObjectiveResultPresetViewModel { DisplayName = preset.DisplayName, Points = preset.Points }),
                        ObjectiveResults = results.Highest()
                                                  .Select(result => Tuple.Create(teams.Where(team => team.Id == result.Team.ContentItemRecord.Id).FirstOrDefault(), result))
                                                  .Where(t => t.Item1 != null)
                                                  .OrderBy(t => t.Item1.Title)
                                                  .OrderByDescending(t => t.Item2.Points)
                                                  .ToList()
                    };

                    return shapeHelper.Parts_Objective_Results(ObjectiveResults: viewModel);
                })
                );
        }

        protected override DriverResult Editor(ObjectivePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Objective_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts/Objective", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        protected override DriverResult Editor(ObjectivePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new ObjectiveEditorViewModel();
            updater.TryUpdateModel(viewModel, Prefix, null, null);

            var game = _gameService.Value.Get(viewModel.GameId);
            part.Game = game != null ? game.Record : null;

            _objectiveService.Value.UpdateObjective(viewModel, part);

            return Editor(part, shapeHelper);
        }

        private ObjectiveEditorViewModel BuildEditorViewModel(ObjectivePart part)
        {
            return new ObjectiveEditorViewModel
            {
                GameId = part.Game != null ? part.Game.ContentItemRecord.Id : 0,
                ObjectiveResultPresets = part.ObjectiveResultPresets
                                             .OrderBy(record => record.Position)
                                             .Select(record =>
                                                 new ObjectiveResultPresetViewModel
                                                 {
                                                     DisplayName = record.DisplayName,
                                                     Points = record.Points
                                                 })
                                            .ToList()
            };
        }
    }
}