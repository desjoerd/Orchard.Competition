using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Localization;
using Orchard.UI.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeSjoerd.Competition.Controllers
{
    [Admin]
    public class ObjectiveResultAdminController : Controller
    {
        private readonly IObjectiveService _objectiveService;
        private readonly IObjectiveResultService _objectiveResultService;
        private readonly ITeamService _teamService;

        public ObjectiveResultAdminController(
            IObjectiveService objectiveService,
            IObjectiveResultService objectiveResultService,
            ITeamService teamService,
            IOrchardServices services)
        {
            this._objectiveService = objectiveService;
            this._objectiveResultService = objectiveResultService;
            this._teamService = teamService;

            this.Services = services;
            this.T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }

        public Localizer T { get; set; }

        public ActionResult List(int? objectiveId)
        {
            ObjectivePart objective = objectiveId != null ? _objectiveService.Get(objectiveId.Value, VersionOptions.Latest) : null;
            if (objective != null)
            {
                var objectiveResults = _objectiveResultService.Get(objective, VersionOptions.Latest);
                var list = Services.New.List();

                list.AddRange(objectiveResults.Select(objectiveResult => Services.ContentManager.BuildDisplay(objectiveResult, "SummaryAdmin")));

                dynamic viewModel = Services.New.ViewModel()
                    .ContentItems(list)
                    .Objective(objective);

                return View((object)viewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult ChooseTeam(int objectiveId)
        {
            var objective = _objectiveService.Get(objectiveId, VersionOptions.Latest);
            if (objective == null)
            {
                return HttpNotFound();
            }

            var teams = _teamService.Get(VersionOptions.Latest);

            return View(new ChooseTeamViewModel
            {
                Objective = objective,
                Teams = teams
            });
        }

        public ActionResult Create(int objectiveId, int teamId)
        {
            var objectiveResult = Services.ContentManager.New("SimpleObjectiveResult");

            if (!Services.Authorizer.Authorize(Orchard.Core.Contents.Permissions.EditContent, objectiveResult, T("Cannot create content")))
                return new HttpUnauthorizedResult();

            var objective = _objectiveService.Get(objectiveId, VersionOptions.Latest);
            var team = _teamService.Get(teamId, VersionOptions.Latest);

            if (team == null || objective == null)
            {
                return HttpNotFound(T("Team or objective not found").Text);
            }

            var commonPart = objectiveResult.As<CommonPart>();
            commonPart.Container = objective;

            var objectiveResultPart = objectiveResult.As<ObjectiveResultPart>();
            objectiveResultPart.Objective = objective.Record;
            objectiveResultPart.Team = team.Record;

            dynamic model = Services.ContentManager.BuildEditor(objectiveResult);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }
    }
}
