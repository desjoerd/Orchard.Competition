using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeSjoerd.Competition.Extensions;

namespace DeSjoerd.Competition.Controllers
{
    [Themed]
    public class ScoreboardController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IGameService _gameService;
        private readonly IObjectiveService _objectiveService;
        private readonly IObjectiveResultService _objectiveResultService;

        public ScoreboardController(
            ITeamService teamService,
            IGameService gameService,
            IObjectiveService objectiveService,
            IObjectiveResultService objectiveResultService,
            IOrchardServices orchardServices)
        {
            this._teamService = teamService;
            this._gameService = gameService;
            this._objectiveService = objectiveService;
            this._objectiveResultService = objectiveResultService;

            Services = orchardServices;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }

        public Localizer T { get; set; }

        //
        // GET: /Scoreboard/
        //[OutputCache(Duration = 300)]
        public ActionResult Index()
        {
            return View(BuildScoreboardViewModel());
        }

        private ScoreboardViewModel BuildScoreboardViewModel()
        {
            var teams = _teamService.Get().ToList();
            var games = _gameService.Get().ToList();
            var objectives = _objectiveService.Get().ToList();
            //var objectiveResults = _objectiveResultService.Get().Highest().Where(result => Services.Authorizer.Authorize(Permissions.ViewAllResults, result)).ToList();
            var objectiveResults = _objectiveResultService.GetHighestResults().Where(result => Services.Authorizer.Authorize(Permissions.ViewAllResults, result)).ToList();

            objectiveResults = objectiveResults.Where(o => o.Objective != null && o.Objective.GamePartRecord != null).ToList();

            var rankings = from team in teams
                           orderby team.Title ascending
                           select Tuple.Create(team, objectiveResults.Where(result => result.Team.ContentItemRecord.Id == team.ContentItem.Id).Sum(result => result.Points));

            rankings = rankings.OrderByDescending(teamScore => teamScore.Item2);

            var gameRankings = from game in games
                               orderby game.Title ascending
                               select Tuple.Create(game, (from team in teams
                                                         orderby team.Title ascending
                                                         select Tuple.Create(team,
                                                            objectiveResults.Where(result => result.Team.ContentItemRecord.Id == team.ContentItem.Id)
                                                                            .Where(result => game.ContentItem.Id == result.Objective.GamePartRecord.ContentItemRecord.Id)
                                                                            .Sum(result => result.Points)))
                                                         .OrderByDescending(teamPoints => teamPoints.Item2).AsEnumerable());

            return new ScoreboardViewModel
            {
                Rankings = rankings,
                GameRankings = gameRankings
            };
        }
    }
}
