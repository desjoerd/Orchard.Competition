using DeSjoerd.Competition.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Users.Models;
using Orchard.Users.Services;
using System.Web.Security;
using DeSjoerd.Competition.Services;
using Orchard;
using Orchard.Localization;
using Orchard.UI.Notify;

namespace DeSjoerd.Competition.Handlers
{
    public class TeamPartHandler : ContentHandler
    {
        private Lazy<ITeamService> _teamService;
        private Lazy<IOrchardServices> _services;

        public TeamPartHandler(
            IRepository<TeamPartRecord> repository,
            Lazy<ITeamService> teamService,
            Lazy<IOrchardServices> services)
        {
            _teamService = teamService;
            _services = services;
            T = NullLocalizer.Instance;

            Filters.Add(StorageFilter.For(repository));

            OnPublishing<TeamPart>(OnPublishingTeam);
        }

        public Localizer T { get; set; }

        private void OnPublishingTeam(PublishContentContext context, TeamPart part)
        {
            var userName = part.UserName;
            _teamService.Value.PublishUser(part);

            if (userName != part.UserName)
            {
                _services.Value.Notifier.Warning(T("UserNames in conflict. \"{0}\" is already set for another user so now it has the UserName \"{1}\"",
                                                 userName, part.UserName));
            }
        }
    }
}