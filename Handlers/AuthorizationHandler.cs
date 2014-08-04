using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using DeSjoerd.Competition.Models;
using Orchard;
using Orchard.Core.Common.Models;
using Orchard.UI.Notify;
using Orchard.Roles.Models;

namespace DeSjoerd.Competition.Handlers
{
    public class AuthorizationHandler : IAuthorizationServiceEventHandler
    {
        private readonly Lazy<WorkContext> _workContext;

        public AuthorizationHandler(
            Lazy<WorkContext> workContext)
        {
            this._workContext = workContext;
        }

        public void Checking(CheckAccessContext context)
        {
        }

        public void Adjust(CheckAccessContext context)
        {
        }

        public void Complete(CheckAccessContext context)
        {
            if (context.Permission == Permissions.ViewAllResults && context.Content.Is<ObjectiveResultPart>())
            {
                var settings = _workContext.Value.CurrentSite.As<CompetitionSettingsPart>();
                if (!settings.HideResults)
                {
                    return;
                }

                var result = context.Content.As<ObjectiveResultPart>();
                if (_workContext.Value.CurrentUser != null)
                {
                    var team = result.Team;
                    if (team != null && team.ContentItemRecord.Id == _workContext.Value.CurrentUser.ContentItem.Id)
                    {
                        return;
                    }
                    else
                    {
                        var roles = _workContext.Value.CurrentUser.As<UserRolesPart>();
                        if (roles != null && roles.Roles.Contains("Administrator"))
                        {
                            return;
                        }
                    }
                }

                var contentCommon = context.Content.As<CommonPart>();
                if (settings.HideResultsFromUtc != null && contentCommon.PublishedUtc != null)
                {
                    if (contentCommon.PublishedUtc.Value > settings.HideResultsFromUtc.Value)
                    {
                        context.Granted = false;
                    }
                }
            }
        }
    }
}