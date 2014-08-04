using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class ObjectiveResultPart : ContentPart<ObjectiveResultPartRecord>
    {
        public int Points
        {
            get { return Record.Points; }
            set { Record.Points = value; }
        }

        public string DisplayName
        {
            get { return Record.DisplayName; }
            set { Record.DisplayName = value; }
        }

        public TeamPartRecord Team
        {
            get { return Record.TeamPartRecord; }
            set { Record.TeamPartRecord = value; }
        }

        public ObjectivePartRecord Objective
        {
            get { return Record.ObjectivePartRecord; }
            set { Record.ObjectivePartRecord = value; }
        }
    }
}