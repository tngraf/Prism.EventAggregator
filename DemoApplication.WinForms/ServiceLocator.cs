#region Header
// --------------------------------------------------------------------------
// Prism.EventAggregator
// ==========================================================================
//
// This library contains the event aggregator part of Microsoft Prism 5.
// Prism has been released by Microsoft as open source software, licensed
// under the Apache License, Version 2.0.
//
// ===========================================================================
//
// <copyright file="ServiceLocator.cs" company="Tethys">
// Copyright (c) 2015 Thomas Graf.
//            All rights reserved.
//            Licensed under the Apache License, Version 2.0.
//            Unless required by applicable law or agreed to in writing, 
//            software distributed under the License is distributed on an
//            "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
//            either express or implied. 
// </copyright>
//
// ---------------------------------------------------------------------------
#endregion

namespace Prism.EventAggregator.DemoApplication.WinForms
{
    using Prism.EventAggregator;

    /// <summary>
    /// A very simplified version of a service locator.
    /// </summary>
    public static class ServiceLocator
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        public static IEventAggregator EventAggregator { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Initializes the ServiceLocator.
        /// </summary>
        public static void Initialize()
        {
            EventAggregator = new EventAggregator();
        } // Initialize()
        #endregion // PUBLIC METHODS
    } // ServiceLocator
} // Prism.EventAggregator.DemoApplication.WinForms
