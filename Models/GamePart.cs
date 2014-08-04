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
    }
}