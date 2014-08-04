using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Drivers
{
    public class TeamPartDriver : ContentPartDriver<TeamPart>
    {
        private readonly Lazy<IGameService> _gameService;
        private readonly Lazy<IObjectiveService> _objectiveService;
        private readonly Lazy<IObjectiveResultService> _objectiveResultService;

        public TeamPartDriver(
            Lazy<IGameService> gameService,
            Lazy<IObjectiveService> objectiveService,
            Lazy<IObjectiveResultService> objectiveResultService,
            IOrchardServices services)
        {
            _gameService = gameService;
            _objectiveService = objectiveService;
            _objectiveResultService = objectiveResultService;
            Services = services;
        }

        public IOrchardServices Services { get; set; }

        protected override string Prefix
        {
            get
            {
                return "Team";
            }
        }

        protected override DriverResult Display(TeamPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Team_Results", () =>
            {
                var results = _objectiveResultService.Value.GetResultsOfTeam(part)
                    .ToList()
                    .Where(result => Services.Authorizer.Authorize(Permissions.ViewAllResults, result))
                    .Where(result => result.Objective != null && result.Objective.GamePartRecord != null)
                    .GroupBy(result => result.Objective)
                    .GroupBy(resultObjective => resultObjective.Key.GamePartRecord)
                    .ToList();

                var groupedResults = results.Select(gameObjectiveResult => 
                    Tuple.Create(
                        _gameService.Value.Get(gameObjectiveResult.Key.ContentItemRecord.Id), 
                        gameObjectiveResult.Select(objectiveResult => 
                            Tuple.Create(
                                _objectiveService.Value.Get(objectiveResult.Key.ContentItemRecord.Id, VersionOptions.Latest), 
                                objectiveResult.OrderByDescending(result => result.Points).FirstOrDefault()
                            )
                        )
                    )
                );

                var vm = new TeamResultsViewModel
                {
                    Results = groupedResults.ToList()
                };

                return shapeHelper.Parts_Team_Results(ViewModel: vm);
            });
        }

        protected override DriverResult Editor(TeamPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Team_Edit", () =>
                    shapeHelper.EditorTemplate(TemplateName: "Parts/Team", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(TeamPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, new string[] { "UserName", "Password" }, null);

            return Editor(part, shapeHelper);
        }
    }
}