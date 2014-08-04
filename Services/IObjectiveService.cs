using DeSjoerd.Competition.Models;
using DeSjoerd.Competition.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeSjoerd.Competition.Services
{
    public interface IObjectiveService : IDependency
    {
        /// <summary>
        /// Gets all the published objectives
        /// </summary>
        /// <returns></returns>
        IEnumerable<ObjectivePart> Get();

        /// <summary>
        /// Gets all the objectives with the given version
        /// </summary>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        IEnumerable<ObjectivePart> Get(VersionOptions versionOptions);

        /// <summary>
        /// Gets all the published objectives linked to the given game
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        IEnumerable<ObjectivePart> Get(IContent container);

        /// <summary>
        /// Gets all the objectives with the given version and linked to the given game
        /// </summary>
        /// <param name="container"></param>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        IEnumerable<ObjectivePart> Get(IContent container, VersionOptions versionOptions);

        /// <summary>
        /// Gets the published objective with the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ObjectivePart Get(int id);

        /// <summary>
        /// Gets the objective with given id and the requested version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        ObjectivePart Get(int id, VersionOptions versionOptions);

        /// <summary>
        /// gives all the contentTypes with an objectivePart
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetObjectiveContentTypes();

        /// <summary>
        /// Updates the objective
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="objectivePart"></param>
        void UpdateObjective(ObjectiveEditorViewModel viewModel, ObjectivePart objectivePart);
    }
}