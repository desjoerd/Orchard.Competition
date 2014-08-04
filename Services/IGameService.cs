using DeSjoerd.Competition.Models;
using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeSjoerd.Competition.Services
{
    public interface IGameService : IDependency
    {
        /// <summary>
        /// Gets the published game with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GamePart Get(int id);

        /// <summary>
        /// Gets the game with the given id and version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        GamePart Get(int id, VersionOptions versionOptions);

        /// <summary>
        /// Gets all the published games
        /// </summary>
        /// <returns></returns>
        IEnumerable<GamePart> Get();

        /// <summary>
        /// Gets all the games with the given version
        /// </summary>
        /// <param name="versionOptions"></param>
        /// <returns></returns>
        IEnumerable<GamePart> Get(VersionOptions versionOptions);
    }
}
