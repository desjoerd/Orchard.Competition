using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Models
{
    public class TeamPart : ContentPart<TeamPartRecord>
    {
        public string Title { get { return this.As<ITitleAspect>().Title; } }

        [Required]
        public string UserName
        {
            get { return Record.UserName; }
            set { Record.UserName = value; }
        }

        [Required]
        public string Password
        {
            get { return Record.Password; }
            set { Record.Password = value; }
        }
    }
}