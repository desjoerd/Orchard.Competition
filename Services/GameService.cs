using DeSjoerd.Competition.Models;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Services
{
    public class GameService : IGameService
    {
        private readonly IContentManager _contentManager;

        public GameService(
            IContentManager contentManager)
        {
            this._contentManager = contentManager;
        }

        public GamePart Get(int id)
        {
            return Get(id, VersionOptions.Published);
        }

        public GamePart Get(int id, VersionOptions versionOptions)
        {
            return _contentManager.Get(id, versionOptions).As<GamePart>();
        }

        public IEnumerable<GamePart> Get()
        {
            return Get(VersionOptions.Published);
        }

        public IEnumerable<GamePart> Get(VersionOptions versionOptions)
        {
            return _contentManager.Query<GamePart>(versionOptions).List();
        }
    }
}