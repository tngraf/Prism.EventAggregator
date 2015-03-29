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
// <copyright file="IMessage.cs" company="Tethys">
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
    /// <summary>
    /// A very simple interface for message to get published.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the message text.
        /// </summary>
        string MessageText { get; }
    } // IMessage
} // Prism.EventAggregator.DemoApplication.WinForms
