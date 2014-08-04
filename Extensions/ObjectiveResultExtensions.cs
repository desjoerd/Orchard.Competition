using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Extensions
{
    public static class ObjectiveResultExtensions
    {
        public static IEnumerable<ObjectiveResultPart> Highest(this IEnumerable<ObjectiveResultPart> results)
        {
            return results
                .GroupBy(r => r.Objective)
                .SelectMany(objectiveResults => 
                    objectiveResults.GroupBy(r => r.Team)
                                    .SelectMany(teamResults => 
                                        teamResults.OrderByDescending(r => r.Points)
                                                   .Take(1)));
        }
    }
}