using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class ChooseTeamViewModel
    {
        public ObjectivePart Objective { get; set; }

        public IEnumerable<TeamPart> Teams { get; set; }
    }
}