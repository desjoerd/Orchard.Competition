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
    public class TeamAdminController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamAdminController(
            ITeamService teamService,
            IOrchardServices services)
        {
            this._teamService = teamService;
            this.Services = services;
        }

        public IOrchardServices Services { get; set; }

        //
        // GET: /TeamAdmin/

        public ActionResult List()
        {
            var teams = _teamService.Get(VersionOptions.Latest);
            var list = Services.New.List();
            
            list.AddRange(teams.Select(team => Services.ContentManager.BuildDisplay(team, "SummaryAdmin")));

            dynamic viewModel = Services.New.ViewModel()
                .ContentItems(list);
            // Casting to avoid invalid (under medium trust) reflection over the protected View method and force a static invocation.
            return View((object)viewModel);
        }

    }
}
