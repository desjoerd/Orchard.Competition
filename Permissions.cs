using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ViewAllResults = new Permission { Description = "View all results", Name = "ViewAllResults" };

        public static readonly Permission ViewResults = new Permission { };

        public static readonly Permission ViewObjective = new Permission { };
        public static readonly Permission ViewObjectiveTitle = new Permission { };
        public static readonly Permission ViewObjectiveBody = new Permission { };
        public static readonly Permission CreateSubmit = new Permission { };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                ViewAllResults
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Anonymous",
                    Permissions = new[] { ViewAllResults }
                },
                new PermissionStereotype {
                    Name = "Authenticated",
                    Permissions = new[] { ViewAllResults }
                },
            };
        }

    }
}