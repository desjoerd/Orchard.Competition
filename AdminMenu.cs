using Orchard.Localization;
using Orchard.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName
        {
            get { return "admin"; }
        }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("Competition"), "5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu)
        {
            //menu.Add(T("Manage Orientation"), "1.0", item =>
    //item.Action("Item", "OrientationAdmin", new { area = "DeSjoerd.Intro", }).LocalNav());

            //menu.Add(T("Games"), "1.1", item =>
            //    item.Action("List", "GameAdmin", new { area = "DeSjoerd.Intro", orientationId = orientationPart.Id }).LocalNav());

            menu.Add(T("Teams"), "1.1", item => item
                .Action("List", "TeamAdmin", new { area = "DeSjoerd.Competition" }).LocalNav());

            menu.Add(T("Games"), "1.2", item => item
                .Action("List", "GameAdmin", new { area = "DeSjoerd.Competition" }).LocalNav());

            menu.Add(T("Objectives"), "1.2", item => item
                .Action("List", "ObjectiveAdmin", new { area = "DeSjoerd.Competition" }).LocalNav());
        }
    }
}