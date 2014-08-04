using DeSjoerd.Competition.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.UI.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeSjoerd.Competition.Controllers
{
    [Admin]
    public class GameAdminController : Controller
    {
        private readonly IGameService _gameService;

        public GameAdminController(
            IGameService gameService,
            IOrchardServices services)
        {
            this._gameService = gameService;
            this.Services = services;
        }

        public IOrchardServices Services { get; set; }


        public ActionResult List()
        {
            var games = _gameService.Get(VersionOptions.Latest);
            var list = Services.New.List();

            list.AddRange(games.Select(game => Services.ContentManager.BuildDisplay(game, "SummaryAdmin")));

            dynamic viewModel = Services.New.ViewModel()
                .ContentItems(list);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }

    }
}
