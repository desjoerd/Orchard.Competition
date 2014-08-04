using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Core.Contents.Extensions;
using Orchard.ContentManagement.MetaData;

namespace DeSjoerd.Competition
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("CompetitionSettingsPartRecord", table => table
                .ContentPartRecord()
                .Column<bool>("HideResults", column => column.WithDefault(false))
                .Column<DateTime>("HideResultsFromUtc"));

            SchemaBuilder.CreateTable("TeamPartRecord", table => table
                .ContentPartVersionRecord()
                .Column<string>("UserName")
                .Column<string>("Password"));

            SchemaBuilder.CreateTable("GamePartRecord", table => table
                .ContentPartRecord());

            SchemaBuilder.CreateTable("ObjectivePartRecord", table => table
                .ContentPartRecord()
                .Column<int>("GamePartRecord_Id"));

            SchemaBuilder.CreateTable("ObjectiveResultPresetRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("ObjectivePartRecord_Id")
                .Column<int>("Points")
                .Column<string>("DisplayName")
                .Column<int>("Position"));

            SchemaBuilder.CreateTable("ObjectiveResultPartRecord", table => table
                .ContentPartVersionRecord()
                .Column<int>("Points")
                .Column<string>("DisplayName")
                .Column<int>("TeamPartRecord_Id")
                .Column<int>("ObjectivePartRecord_Id"));

            SchemaBuilder.CreateTable("CompetitionPermissionsPartRecord", table => table
                .ContentPartRecord());

            SchemaBuilder.CreateTable("CompetitionTeamPermissionsRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("CompetitionPermissionsPartRecord_Id")
                .Column<int>("TeamPartRecord_Id")
                .Column<string>("GrantedPermissions"));


            ContentDefinitionManager.AlterTypeDefinition("Team", builder => builder
                .WithPart("CommonPart")
                .WithPart("TitlePart")
                .WithPart("UserPart")
                .WithPart("TeamPart")
                .Draftable());

            ContentDefinitionManager.AlterTypeDefinition("Game", builder => builder
                .WithPart("CommonPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("AutoroutePart", partBuilder => partBuilder
                    .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                    .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                    .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Game-Title', Pattern: 'Games/{Content.Slug}', Description: 'Games/Lego'}]")
                    .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                .WithPart("NavigationPart")
                .WithPart("GamePart")
                .Draftable());

            ContentDefinitionManager.AlterTypeDefinition("SimpleObjective", builder => builder
                .WithPart("CommonPart")
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("AutoroutePart", partBuilder => partBuilder
                    .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                    .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                    .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Game-Title and Objective-Title', Pattern: '{Content.Container.Path}/{Content.Slug}', Description: 'Games/Lego/Ringen-Verzamelen'}]")
                    .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                .WithPart("ObjectivePart")
                .Draftable());

            ContentDefinitionManager.AlterTypeDefinition("SimpleObjectiveResult", builder => builder
                .WithPart("CommonPart")
                .WithPart("ObjectiveResultPart")
                .Draftable());

            return 4;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateTable("ObjectiveResultPartRecord", table => table
                .ContentPartVersionRecord()
                .Column<int>("Points")
                .Column<string>("DisplayName")
                .Column<int>("TeamPartRecord_Id")
                .Column<int>("ObjectivePartRecord_Id"));

            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.CreateTable("CompetitionSettingsPartRecord", table => table
                .ContentPartRecord()
                .Column<bool>("HideResults", column => column.WithDefault(true))
                .Column<DateTime>("HideResultsFromUtc")); 

            return 3;
        }

        public int UpdateFrom3()
        {
            SchemaBuilder.CreateTable("CompetitionPermissionsPartRecord", table => table
                .ContentPartRecord());

            SchemaBuilder.CreateTable("CompetitionTeamPermissionsRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("CompetitionPermissionsPartRecord_Id")
                .Column<int>("TeamPartRecord_Id")
                .Column<string>("GrantedPermissions"));


            return 4;
        }
    }
}