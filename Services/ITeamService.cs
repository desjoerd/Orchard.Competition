using DeSjoerd.Competition.Models;
using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeSjoerd.Competition.Services
{
    public interface ITeamService : IDependency
    {
        /// <summary>
        /// Gets all the published teams
        /// </summary>
        /// <returns></returns>
        IEnumerable<TeamPart> Get();

        /// <summary>
        /// Gets all the teams with the given version
        /// </summary>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        IEnumerable<TeamPart> Get(VersionOptions versionOptions);

        /// <summary>
        /// Gets the published team with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TeamPart Get(int id);

        /// <summary>
        /// Gets the team with the given id and version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        TeamPart Get(int id, VersionOptions versionOptions);

        /// <summary>
        /// Publishes the user bound to this team
        /// </summary>
        /// <param name="part"></param>
        void PublishUser(TeamPart part);
    }
}
