using DeSjoerd.Competition.Models;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Services
{
    public class ObjectiveResultService : IObjectiveResultService
    {
        private readonly IContentManager _contentManager;
        private readonly IRepository<ObjectiveResultPartRecord> _objectiveResultRepository;

        public ObjectiveResultService(
            IContentManager contentManager,
            IRepository<ObjectiveResultPartRecord> objectiveResultRepository)
        {
            _contentManager = contentManager;
            _objectiveResultRepository = objectiveResultRepository;
        }

        public IEnumerable<ObjectiveResultPart> Get()
        {
            return Get(VersionOptions.Published);
        }

        public IEnumerable<ObjectiveResultPart> Get(VersionOptions versionOptions)
        {
            return _contentManager.Query<ObjectiveResultPart>(versionOptions).List();
        }

        public IEnumerable<ObjectiveResultPart> Get(IContent objective)
        {
            return Get(objective, VersionOptions.Published);
        }

        public IEnumerable<ObjectiveResultPart> Get(IContent objective, VersionOptions versionOptions)
        {
            if (objective == null)
            {
                throw new ArgumentNullException("objective");
            }

            return _contentManager.Query<ObjectiveResultPart, ObjectiveResultPartRecord>(versionOptions)
                .Where(o => o.ObjectivePartRecord.Id == objective.Id)
                .List();
        }


        public IEnumerable<ObjectiveResultPart> GetResultsOfTeam(TeamPart team)
        {
            return GetResultsOfTeam(team, VersionOptions.Published);
        }

        public IEnumerable<ObjectiveResultPart> GetResultsOfTeam(TeamPart team, VersionOptions versionOptions)
        {
            Argument.ThrowIfNull(team, "team");

            var teamResultIds = _objectiveResultRepository.Fetch(result => result.TeamPartRecord.ContentItemRecord.Id == team.ContentItem.Id).Select(result => result.ContentItemRecord.Id);
            return _contentManager.GetMany<ObjectiveResultPart>(teamResultIds, versionOptions, new QueryHints());
        }


        public IEnumerable<ObjectiveResultPart> GetHighestResults()
        {
            return GetHighestResults(VersionOptions.Published);
        }

        public IEnumerable<ObjectiveResultPart> GetHighestResults(VersionOptions versionOptions)
        {
                //        return results
                //.GroupBy(r => r.Objective)
                //.SelectMany(objectiveResults => 
                //    objectiveResults.GroupBy(r => r.Team)
                //                    .SelectMany(teamResults => 
                //                        teamResults.OrderByDescending(r => r.Points)
                //                                   .Take(1)));

            var resultIds = _objectiveResultRepository.Table.ToList()
                .GroupBy(result => result.ObjectivePartRecord.ContentItemRecord.Id)
                .SelectMany(objectiveResults =>
                    objectiveResults.GroupBy(r => r.TeamPartRecord.ContentItemRecord.Id)
                                    .SelectMany(teamResults =>
                                        teamResults.OrderByDescending(r => r.Points)
                                                   .Take(1)))
                .Select(result => result.ContentItemRecord.Id);

            return _contentManager.GetMany<ObjectiveResultPart>(resultIds, versionOptions, new QueryHints());
        }
    }
}