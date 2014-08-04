using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class ObjectiveResultPartRecord : ContentPartVersionRecord
    {
        public virtual int Points { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual TeamPartRecord TeamPartRecord { get; set; }

        public virtual ObjectivePartRecord ObjectivePartRecord { get; set; }
    }
}