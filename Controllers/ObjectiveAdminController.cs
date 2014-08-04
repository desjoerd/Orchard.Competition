using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeSjoerd.Competition.Controllers
{
    [Admin]
    public class ObjectiveAdminController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IObjectiveService _objectiveService;

        public ObjectiveAdminController(
            IGameService gameService,
            IObjectiveService objectiveService,
            IOrchardServices services)
        {
            this._gameService = gameService;
            this._objectiveService = objectiveService;

            this.Services = services;

            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }

        public Localizer T { get; set; }

        public ActionResult List(int? gameId)
        {
            GamePart game = null;
            if (gameId != null)
            {
                game = _gameService.Get(gameId.Value, VersionOptions.Latest);
            }

            if (game == null)
            {
                return RedirectToAction("ChooseGame");
            }

            IEnumerable<ObjectivePart> objectives;
            if(game != null) {
                objectives = _objectiveService.Get(game, VersionOptions.Latest);
            } else {
                objectives = _objectiveService.Get(VersionOptions.Latest);
            }
            
            var list = Services.New.List();

            list.AddRange(objectives.Select(objective => Services.ContentManager.BuildDisplay(objective, "SummaryAdmin")));

            dynamic viewModel = Services.New.ViewModel()
                .ContentItems(list)
                .Game(game)
                .AllGames(_gameService.Get(VersionOptions.Latest));

            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }

        public ActionResult ChooseGame()
        {
            var vm = Services.New.ViewModel();
            var games = _gameService.Get(VersionOptions.Latest);

            vm.Games(games);

            return View((object)vm);
        }

        public ActionResult ChooseObjectiveType(int? gameId)
        {
            var objectiveContentTypes = _objectiveService.GetObjectiveContentTypes();

            var viewModel = new ChooseObjectiveViewModel
            {
                GameId = gameId,
                ObjectiveContentTypes = objectiveContentTypes,
            };

            return View(viewModel);
        }

        public ActionResult Create(string objectiveType, int? gameId)
        {
            if (string.IsNullOrEmpty(objectiveType))
                return HttpNotFound();

            var contentItem = Services.ContentManager.New(objectiveType);

            if (!Services.Authorizer.Authorize(Orchard.Core.Contents.Permissions.EditContent, contentItem, T("Cannot create content")))
                return new HttpUnauthorizedResult();

            if (gameId.HasValue)
            {
                var game =  _gameService.Get(gameId.Value, VersionOptions.Latest); // todo check
                var common = contentItem.As<CommonPart>();
                if (common != null)
                {
                    common.Container = game;
                }
                var objectivePart = contentItem.As<ObjectivePart>();
                objectivePart.Game = game.Record;
            }

            dynamic model = Services.ContentManager.BuildEditor(contentItem);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)model);
        }
    }
}
