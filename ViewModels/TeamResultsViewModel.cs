using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class TeamResultsViewModel
    {
        public IEnumerable<Tuple<GamePart, IEnumerable<Tuple<ObjectivePart, ObjectiveResultPart>>>> Results { get; set; }
    }
}