using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class ObjectivePartRecord : ContentPartRecord
    {
        public ObjectivePartRecord()
        {
            ObjectiveResultPresets = new List<ObjectiveResultPresetRecord>();
        }

        public virtual GamePartRecord GamePartRecord { get; set; }

        public virtual IList<ObjectiveResultPresetRecord> ObjectiveResultPresets { get; set; }
    }
}