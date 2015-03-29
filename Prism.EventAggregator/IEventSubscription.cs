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
// <copyright file="IEventSubscription.cs" company="Tethys">
// Copyright (c) Microsoft Corporation. All rights reserved. 
// Copyright for modification by Thomas Graf.
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

namespace Prism.EventAggregator
{
    using System;

    /// <summary>
    /// Defines a contract for an event subscription to be used by <see cref="EventBase"/>.
    /// </summary>
    public interface IEventSubscription
    {
        /// <summary>
        /// Gets or sets a <see cref="SubscriptionToken"/> that identifies 
        /// this <see cref="IEventSubscription"/>.
        /// </summary>
        /// <value>A token that identifies this <see cref="IEventSubscription"/>.</value>
        SubscriptionToken SubscriptionToken { get; set; }

        /// <summary>
        /// Gets the execution strategy to publish this event.
        /// </summary>
        /// <returns>An <see cref="Action{T}"/> with the execution strategy, 
        /// or <see langword="null" /> if the <see cref="IEventSubscription"/>
        /// is no longer valid.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", 
            "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "Best solution here")]
        Action<object[]> GetExecutionStrategy();
    } // IEventSubscription
} // Prism.EventAggregator