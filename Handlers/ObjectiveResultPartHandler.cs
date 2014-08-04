using DeSjoerd.Competition.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Handlers
{
    public class ObjectiveResultPartHandler : ContentHandler
    {
        public ObjectiveResultPartHandler(
            IRepository<ObjectiveResultPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}