using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class GamePart : ContentPart<GamePartRecord>
    {
        public string Title
        {
            get { return this.As<ITitleAspect>().Title; }
        }

        public IList<ObjectivePartRecord> Objectives
        {
            get
            {
                return Record.Objectives;
            }
        }

        public bool SortObjectivesByAlphabet
        {
            get { return Record.SortObjectivesByAlphabet; }
            set { Record.SortObjectivesByAlphabet = value; }
        }

        public bool UseTinyDisplayType
        {
            get { return Record.UseTinyDisplayType; }
            set { Record.UseTinyDisplayType = value; }
        }
    }
}