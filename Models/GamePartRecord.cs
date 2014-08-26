using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class GamePartRecord : ContentPartRecord
    {
        public GamePartRecord()
        {
            Objectives = new List<ObjectivePartRecord>();
        }

        public virtual IList<ObjectivePartRecord> Objectives { get; set; }



        public virtual bool SortObjectivesByAlphabet { get; set; }

        public virtual bool UseTinyDisplayType { get; set; }
    }
}