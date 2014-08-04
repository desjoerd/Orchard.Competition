using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class ObjectiveResultPresetRecord
    {
        public virtual int Id { get; set; }

        public virtual ObjectivePartRecord ObjectivePartRecord { get; set; }

        public virtual int Points { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual int Position { get; set; }
    }
}