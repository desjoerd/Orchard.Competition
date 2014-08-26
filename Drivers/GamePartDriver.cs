using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Drivers
{
    public class GamePartDriver : ContentPartDriver<GamePart>
    {
        private readonly Lazy<IObjectiveService> _objectiveService;
        private readonly Lazy<IContentManager> _contentManager;
        private readonly Lazy<IGameService> _gameService;

        public GamePartDriver(
            Lazy<IObjectiveService> objectiveService,
            Lazy<IContentManager> contentManager,
            Lazy<IGameService> gameService) 
        {
            this._objectiveService = objectiveService;
            this._contentManager = contentManager;
            this._gameService = gameService;
        }

        protected override string Prefix
        {
            get
            {
                return "Game";
            }
        }

        protected override DriverResult Display(GamePart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_Game_Admin_Actions", () => shapeHelper.Parts_Game_Admin_Actions(GamePart: part)),
                ContentShape("Parts_Game_Objectives", () => {

                    var objectives = _objectiveService.Value.Get(part, VersionOptions.Published);

                    if(part.SortObjectivesByAlphabet) {
                        objectives = objectives.OrderBy(o => o.Title);
                    }

                    var objectiveDisplayType = part.UseTinyDisplayType ? "Tiny" : "Summary";

                    var objectiveShapes = objectives.Select(o => _contentManager.Value.BuildDisplay(o, objectiveDisplayType));

                    var list = shapeHelper.List(Classes: new string[] { "no-bullet" });
                    list.AddRange(objectiveShapes);

                    return (object)shapeHelper.Parts_Game_Objectives(Objectives: list);
                }));
        }

        protected override DriverResult Editor(GamePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Game_Edit", () =>
                shapeHelper.EditorTemplate(TemplateName: "Parts/Game", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        protected override DriverResult Editor(GamePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new GameEditorViewModel();
            updater.TryUpdateModel(viewModel, Prefix, null, null);

            _gameService.Value.UpdateGame(viewModel, part);

            return Editor(part, shapeHelper);
        }

        private GameEditorViewModel BuildEditorViewModel(GamePart part)
        {
            return new GameEditorViewModel
            {
                SortObjectivesByAlphabet = part.SortObjectivesByAlphabet,
                UseTinyDisplayType = part.UseTinyDisplayType
            };
        }
    }
}