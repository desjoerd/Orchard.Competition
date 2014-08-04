using DeSjoerd.Competition.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Handlers
{
    public class CompetitionSettingsPartHandler : ContentHandler
    {
        public CompetitionSettingsPartHandler(
            IRepository<CompetitionSettingsPartRecord> repository)
        {
            Filters.Add(new ActivatingFilter<CompetitionSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Competition")));
        }
    }
}