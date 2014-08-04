using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.ViewModels
{
    public class ScoreboardViewModel
    {
        public IEnumerable<Tuple<TeamPart, int>> Rankings { get; set; }

        public IEnumerable<Tuple<GamePart, IEnumerable<Tuple<TeamPart, int>>>> GameRankings { get; set; }
    }
}