using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Notify;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Drivers
{
    public class CompetitionSettingsPartDriver : ContentPartDriver<CompetitionSettingsPart>
    {
        private const string TemplateName = "Parts/CompetitionSettings";

        private readonly INotifier _notifier;
        private readonly Lazy<CultureInfo> _cultureInfo;

        public CompetitionSettingsPartDriver(
            INotifier notifier,
            IOrchardServices services)
        {
            _notifier = notifier;
            Services = services;
            T = NullLocalizer.Instance;

            _cultureInfo = new Lazy<CultureInfo>(() => CultureInfo.GetCultureInfo(Services.WorkContext.CurrentCulture));
        }

        public IOrchardServices Services { get; set; }

        public Localizer T { get; set; }

        protected override string Prefix
        {
            get
            {
                return "CompetitionSettings";
            }
        }

        protected override DriverResult Editor(CompetitionSettingsPart part, dynamic shapeHelper)
        {
            var viewModel = new CompetitionSettingsEditorViewModel
            {
                Date = part.HideResultsFromUtc != null && part.HideResultsFromUtc != DateTime.MinValue ?
                    TimeZoneInfo.ConvertTimeFromUtc(part.HideResultsFromUtc.Value, Services.WorkContext.CurrentTimeZone).ToString("d", _cultureInfo.Value) : String.Empty,
                Time = part.HideResultsFromUtc != null && part.HideResultsFromUtc != DateTime.MinValue ?
                    TimeZoneInfo.ConvertTimeFromUtc(part.HideResultsFromUtc.Value, Services.WorkContext.CurrentTimeZone).ToString("t", _cultureInfo.Value) : String.Empty,
                HideResults = part.HideResults
            };

            return ContentShape("Parts_CompetitionSettings_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: viewModel, Prefix: Prefix))
                .OnGroup("Competition");
        }

        protected override DriverResult Editor(CompetitionSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel = new CompetitionSettingsEditorViewModel();

            if (updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                part.HideResults = viewModel.HideResults;

                DateTime value;

                string parseDateTime = String.Concat(viewModel.Date, " ", viewModel.Time);

                if (!part.HideResults && String.IsNullOrWhiteSpace(viewModel.Date) && String.IsNullOrWhiteSpace(viewModel.Time))
                {
                    part.HideResultsFromUtc = null;
                }
                else if (DateTime.TryParse(parseDateTime, _cultureInfo.Value, DateTimeStyles.None, out value))
                {
                    part.HideResultsFromUtc = TimeZoneInfo.ConvertTimeToUtc(value, Services.WorkContext.CurrentTimeZone);
                }
                else
                {
                    updater.AddModelError(Prefix, T("The given hide date and time is invalid.", "The given hide date and time"));
                    part.HideResultsFromUtc = null;
                }
            }

            return Editor(part, shapeHelper);
        }
    }
}