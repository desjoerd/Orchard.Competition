using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class ObjectivePart : ContentPart<ObjectivePartRecord>
    {
        public string Title { get { return this.As<ITitleAspect>().Title; } }

        public GamePartRecord Game
        {
            get { return Record.GamePartRecord; }
            set { Record.GamePartRecord = value; }
        }

        //public IContent Game
        //{
        //    get { return this.As<CommonPart>().Container; }
        //    set { this.As<CommonPart>().Container = value; }
        //}

        public IList<ObjectiveResultPresetRecord> ObjectiveResultPresets
        {
            get { return this.Record.ObjectiveResultPresets; }
        }
    }
}