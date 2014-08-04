using DeSjoerd.Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeSjoerd.Competition.Extensions
{
    public static class UrlHelperExtensions
    {
        private static string GetReturnUrl()
        {
            var request = HttpContext.Current.Request;
            return request.QueryString["ReturnUrl"] ?? request.RawUrl;
        }

        public static string TeamCreate(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Create", "Admin", new { area = "Contents", id = "Team", ReturnUrl = GetReturnUrl() });
        }

        public static string GameCreate(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Create", "Admin", new { area = "Contents", id = "Game", ReturnUrl = GetReturnUrl() });
        }

        public static string GameObjectives(this UrlHelper urlHelper, GamePart gamePart)
        {
            return urlHelper.Action("List", "ObjectiveAdmin", new { area = "DeSjoerd.Competition", gameId = gamePart.ContentItem.Id });
        }

        public static string ChooseObjectiveType(this UrlHelper urlHelper, int? gameId)
        {
            return urlHelper.Action("ChooseObjectiveType", "ObjectiveAdmin", new { area = "DeSjoerd.Competition", gameId = gameId, ReturnUrl = GetReturnUrl() });
        }

        public static string ObjectiveCreate(this UrlHelper urlHelper, string objectiveContentType, int? gameId)
        {
            return urlHelper.Action("Create", "ObjectiveAdmin", new { area = "DeSjoerd.Competition", objectiveType = objectiveContentType, gameId = gameId, ReturnUrl = GetReturnUrl() });
        }

        public static string ObjectiveResults(this UrlHelper urlHepler, ObjectivePart objectivePart)
        {
            return urlHepler.Action("List", "ObjectiveResultAdmin", new { area = "DeSjoerd.Competition", objectiveId = objectivePart.ContentItem.Id });
        }

        public static string ChooseObjectiveResultTeam(this UrlHelper urlHelper, ObjectivePart objectivePart)
        {
            return urlHelper.Action("ChooseTeam", "ObjectiveResultAdmin", new { area = "DeSjoerd.Competition", objectiveId = objectivePart.ContentItem.Id, ReturnUrl = GetReturnUrl() });
        }

        public static string ObjectiveResultCreate(this UrlHelper urlHelper, TeamPart teamPart, ObjectivePart objectivePart)
        {
            return urlHelper.Action("Create", "ObjectiveResultAdmin", new { area = "DeSjoerd.Competition", teamId = teamPart.ContentItem.Id, objectiveId = objectivePart.ContentItem.Id, ReturnUrl = GetReturnUrl() });
        }
    }
}