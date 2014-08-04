using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.ViewModels;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DeSjoerd.Competition.Services
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IContentManager _contentManager;
        private readonly IGameService _gameService;
        private readonly IRepository<ObjectiveResultPresetRecord> _objectiveResultPresetRepository;

        public ObjectiveService(
            IContentManager contentManager,
            IGameService gameService,
            IRepository<ObjectiveResultPresetRecord> objeciveResultOptionRepository)
        {
            this._contentManager = contentManager;
            this._gameService = gameService;
            this._objectiveResultPresetRepository = objeciveResultOptionRepository;
        }

        public IEnumerable<ObjectivePart> Get()
        {
            return Get(VersionOptions.Published);
        }

        public IEnumerable<ObjectivePart> Get(VersionOptions versionOptions)
        {
            return _contentManager.Query<ObjectivePart>(versionOptions).List();
        }

        public IEnumerable<ObjectivePart> Get(IContent container)
        {
            return Get(container, VersionOptions.Published);
        }

        public IEnumerable<ObjectivePart> Get(IContent container, VersionOptions versionOptions)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            return _contentManager.Query<ObjectivePart>(versionOptions)
                    .Join<CommonPartRecord>()
                    .Where<CommonPartRecord>(c => c.Container.Id == container.Id)
                    .List();
        }

        public ObjectivePart Get(int id)
        {
            return Get(id, VersionOptions.Published);
        }

        public ObjectivePart Get(int id, VersionOptions versionOptions)
        {
            return _contentManager.Get<ObjectivePart>(id, versionOptions);
        }

        public IEnumerable<string> GetObjectiveContentTypes()
        {
            return _contentManager.GetContentTypeDefinitions()
                .Where(contentDefinition => contentDefinition.Parts.Any(part => part.PartDefinition.Name == "ObjectivePart"))
                .Select(contentDefinition => contentDefinition.Name);
        }

        public void UpdateObjective(ObjectiveEditorViewModel viewModel, ObjectivePart objectivePart)
        {
            foreach (var objectiveResultPreset in objectivePart.ObjectiveResultPresets)
            {
                _objectiveResultPresetRepository.Delete(objectiveResultPreset);
            }
            objectivePart.ObjectiveResultPresets.Clear();

            for (int i = 0; i < viewModel.ObjectiveResultPresets.Count; i++)
            {
                var objectiveResultPreset = viewModel.ObjectiveResultPresets[i];

                var newPreset = new ObjectiveResultPresetRecord
                {
                    DisplayName = objectiveResultPreset.DisplayName,
                    Points = objectiveResultPreset.Points,
                    Position = i,
                    ObjectivePartRecord = objectivePart.Record,
                };

                _objectiveResultPresetRepository.Create(newPreset);

                objectivePart.ObjectiveResultPresets.Add(newPreset);
            }

            var game = _gameService.Get(viewModel.GameId);
            objectivePart.Game = game.Record;
        }
    }
}