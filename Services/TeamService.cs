using DeSjoerd.Competition.Models;
using Orchard.ContentManagement;
using Orchard.Security;
using Orchard.Users.Models;
using Orchard.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DeSjoerd.Competition.Services
{
    public class TeamService : ITeamService
    {
        private readonly IContentManager _contentManager;
        private readonly IMembershipService _membershipService;

        public TeamService(
            IContentManager contentManager,
            IMembershipService membershipService)
        {
            this._contentManager = contentManager;
            this._membershipService = membershipService;
        }

        public IEnumerable<TeamPart> Get()
        {
            return Get(VersionOptions.Published);
        }

        public IEnumerable<TeamPart> Get(VersionOptions versionOptions)
        {
            return _contentManager.Query<TeamPart>(versionOptions).List();
        }

        public TeamPart Get(int id)
        {
            return Get(id, VersionOptions.Published);
        }

        public TeamPart Get(int id, VersionOptions versionOptions)
        {
            return _contentManager.Get<TeamPart>(id, versionOptions);
        }

        public void PublishUser(TeamPart part)
        {
            var foundUser = _membershipService.GetUser(part.UserName);
            if (foundUser != null && foundUser.Id != part.Id)
            {
                const string nameFormat = "{0}{1}";
                int n = 2;
                string newUserName;
                do
                {
                    newUserName = string.Format(nameFormat, part.UserName, n);
                    n++;

                    foundUser = _membershipService.GetUser(newUserName);
                } while (foundUser != null);

                part.UserName = newUserName;
            }

            var userPart = part.As<UserPart>();
            userPart.UserName = part.UserName;
            userPart.NormalizedUserName = part.UserName.ToLowerInvariant();
            userPart.Email = part.UserName;
            userPart.RegistrationStatus = UserStatus.Approved;
            userPart.EmailStatus = UserStatus.Approved;

            userPart.Record.Password = part.Password;
            userPart.Record.PasswordFormat = MembershipPasswordFormat.Clear;
            userPart.Record.PasswordSalt = null;
        }
    }
}