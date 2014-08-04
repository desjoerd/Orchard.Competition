using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.Services;
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

        public GamePartDriver(
            Lazy<IObjectiveService> objectiveService,
            Lazy<IContentManager> contentManager) 
        {
            this._objectiveService = objectiveService;
            this._contentManager = contentManager;
        }

        protected override DriverResult Display(GamePart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_Game_Admin_Actions", () => shapeHelper.Parts_Game_Admin_Actions(GamePart: part)),
                ContentShape("Parts_Game_Objectives", () => {

                    var objectives = _objectiveService.Value.Get(part, VersionOptions.Published)
                                                            .Select(o => _contentManager.Value.BuildDisplay(o, "Summary"));

                    var list = shapeHelper.List(Classes: new string[] { "no-bullet" });
                    list.AddRange(objectives);

                    return (object)shapeHelper.Parts_Game_Objectives(Objectives: list);
                }));
        }
    }
}