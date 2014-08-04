using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class TeamPartRecord : ContentPartVersionRecord
    {
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
    }
}