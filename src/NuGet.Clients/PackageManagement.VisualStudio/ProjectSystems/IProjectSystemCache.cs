﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using NuGet.ProjectManagement;
using NuGet.ProjectModel;

namespace NuGet.PackageManagement.VisualStudio
{
    /// <summary>
    /// Project system data cache that stores project metadata indexed by multiple names,
    /// e.g. EnvDTE.Project can be retrieved by name (if non-conflicting), unique name or 
    /// custom unique name.
    /// </summary>
    public interface IProjectSystemCache
    {
        /// <summary>
        /// Retrieves instance of <see cref="NuGetProject"/> associated with project name.
        /// </summary>
        /// <param name="name">Project name, full path or unique name.</param>
        /// <param name="nuGetProject">Desired project object, not initialized if not found.</param>
        /// <returns>True if found, false otherwise.</returns>
        bool TryGetNuGetProject(string name, out NuGetProject nuGetProject);

        /// <summary>
        /// Retrieves instance of <see cref="EnvDTE.Project"/> associated with project name.
        /// </summary>
        /// <param name="name">Project name, full path or unique name.</param>
        /// <param name="dteProject">Desired project object, not initialized if not found.</param>
        /// <returns>True if found, false otherwise.</returns>
        bool TryGetDTEProject(string name, out EnvDTE.Project dteProject);

        /// <summary>
        /// Retrieves project restore info as of <see cref="PackageSpec"/> associated with project name.
        /// </summary>
        /// <param name="name">Project name, full path or unique name.</param>
        /// <param name="packageSpec">Desired restore info object, not initialized if not found.</param>
        /// <returns>True if found, false otherwise.</returns>
        bool TryGetProjectRestoreInfo(string name, out PackageSpec packageSpec);

        /// <summary>
        /// Finds a project name by short name, unique name or custom unique name.
        /// </summary>
        /// <param name="name">Project name, full path or unique name.</param>
        /// <param name="projectNames">Primary key if found.</param>
        /// <returns>True if the project name with the specified name is found.</returns>
        bool TryGetProjectNames(string name, out ProjectNames projectNames);

        /// <summary>
        /// Tries to find a project by its short name. Returns the project name if and only if it is non-ambiguous.
        /// </summary>
        /// <param name="name">Project short name.</param>
        /// <param name="projectNames">Primary key if found</param>
        /// <returns>True if the project name with the specified short name is found.</returns>
        bool TryGetProjectNameByShortName(string name, out ProjectNames projectNames);

        /// <summary>
        /// Checks if cache contains a project associated with given name or full name.
        /// </summary>
        /// <param name="name">Project name, full path or unique name.</param>
        /// <returns>True if the project name with the specified name is found.</returns>
        bool ContainsKey(string name);

        /// <summary>
        /// Retrieves collection of all project instances stored in the cache.
        /// </summary>
        /// <returns>Collection of projects</returns>
        IReadOnlyList<NuGetProject> GetNuGetProjects();

        /// <summary>
        /// Retrieves collection of all project instances stored in the cache.
        /// </summary>
        /// <returns>Collection of projects</returns>
        IReadOnlyList<EnvDTE.Project> GetEnvDTEProjects();

        /// <summary>
        /// Determines if a short name is ambiguous.
        /// </summary>
        /// <param name="shortName">Short name of the project</param>
        /// <returns>True if there are multiple projects with the specified short name.</returns>
        bool IsAmbiguous(string shortName);

        /// <summary>
        /// Adds or updates a project to the project cache.
        /// </summary>
        /// <param name="projectNames">The project name.</param>
        /// <param name="dteProject">The VS project.</param>
        /// <param name="nuGetProject">The NuGet project.</param>
        /// <returns>Returns true if the project was successfully added to the cache.</returns>
        bool AddProject(ProjectNames projectName, EnvDTE.Project dteProject, NuGetProject nuGetProject);

        /// <summary>
        /// Adds or updates project restore info in the project cache.
        /// </summary>
        /// <param name="projectName">Primary key.</param>
        /// <param name="packageSpec">The project restore info.</param>
        /// <returns>Return true if operation succeeded.</returns>
        bool AddProjectRestoreInfo(ProjectNames projectName, PackageSpec packageSpec);

        /// <summary>
        /// Removes a project associated with given name out of the cache.
        /// </summary>
        /// <param name="name">Project name, full path or unique name.</param>
        void RemoveProject(string name);

        /// <summary>
        /// Clears all project cache data.
        /// </summary>
        void Clear();
    }
}
