using DeSjoerd.Competition.Models;
using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Services
{
    public interface IObjectiveResultService : IDependency
    {
        /// <summary>
        /// Gets all the published objectiveResults
        /// </summary>
        /// <returns></returns>
        IEnumerable<ObjectiveResultPart> Get();

        /// <summary>
        /// Gets all the objectiveResults with the given version
        /// </summary>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        IEnumerable<ObjectiveResultPart> Get(VersionOptions versionOptions);

        /// <summary>
        /// Gets all the published objectiveResults from the given container
        /// </summary>
        /// <param name="objective"></param>
        /// <returns></returns>
        IEnumerable<ObjectiveResultPart> Get(IContent objective);

        /// <summary>
        /// Gets all the objectiveResults from the given container with the given version
        /// </summary>
        /// <param name="objective"></param>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        IEnumerable<ObjectiveResultPart> Get(IContent objective, VersionOptions versionOptions);

        IEnumerable<ObjectiveResultPart> GetResultsOfTeam(TeamPart team);

        IEnumerable<ObjectiveResultPart> GetResultsOfTeam(TeamPart team, VersionOptions versionOptions);

        IEnumerable<ObjectiveResultPart> GetHighestResults();

        IEnumerable<ObjectiveResultPart> GetHighestResults(VersionOptions versionOptions);

        //IEnumerable<ObjectiveResultPart> Get(IContent objective, TeamPart team);

        //IEnumerable<ObjectiveResultPart> Get(IContent objective, TeamPart team, VersionOptions versionOptions);
    }
}