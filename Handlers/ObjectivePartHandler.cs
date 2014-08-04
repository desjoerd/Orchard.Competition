using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Handlers
{
    public class ObjectivePartHandler : ContentHandler
    {
        private readonly IRepository<ObjectivePartRecord> _repository;
        private readonly Lazy<IGameService> _gameService;

        public ObjectivePartHandler(
            IRepository<ObjectivePartRecord> repository,
            Lazy<IGameService> gameService)
        {
            _repository = repository;
            _gameService = gameService;

            Filters.Add(StorageFilter.For(repository));

            OnLoaded<ObjectivePart>((loadContext, objectivePart) => FixGamePartRecordReference(objectivePart));
        }

        private void FixGamePartRecordReference(ObjectivePart objective)
        {
            if (objective.Game == null)
            {
                var common = objective.As<CommonPart>();
                if (common != null && common.Container != null)
                {
                    var game = _gameService.Value.Get(common.Container.Id);
                    if (game != null)
                    {
                        objective.Game = game.Record;
                        _repository.Update(objective.Record);
                        _repository.Flush();
                    }
                }
            }
        }
    }
}