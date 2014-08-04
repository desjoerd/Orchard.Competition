using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
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
    public class CompetitionPermissionsAdminController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IObjectiveService _objectiveService;

        public CompetitionPermissionsAdminController(
            IGameService gameService,
            IObjectiveService objectiveService)
        {
            _gameService = gameService;
            _objectiveService = objectiveService;
        }

        //
        // GET: /CompetitionPermissionsAdmin/

        public ActionResult Index()
        {
            var games = _gameService.Get().Where(g => g.Is<CompetitionPermissionsPart>()).ToList();
            var objectives = _objectiveService.Get().ToList();

            var vm = new ViewModels.CompetitionPermissionsAdminViewModel
            {
                Permissions = games.Select(g => Tuple.Create(
                    g, 
                    g.As<CompetitionPermissionsPart>(), 
                    objectives.Select(o => Tuple.Create(
                        o, 
                        o.As<CompetitionPermissionsPart>()
                        )).ToList().AsEnumerable()
                    )).ToList().AsEnumerable()
            };

            return View(vm);
        }

    }
}
