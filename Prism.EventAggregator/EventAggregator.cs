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
// <copyright file="EventAggregator.cs" company="Tethys">
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
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Implements <see cref="IEventAggregator"/>.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// The events.
        /// </summary>
        private readonly Dictionary<Type, EventBase> events 
            = new Dictionary<Type, EventBase>();

        /// <summary>
        /// Captures the sync context for the UI thread when constructed on 
        /// the UI thread in a platform agnostic way so it can be used for
        /// UI thread dispatching
        /// </summary>
        private readonly SynchronizationContext syncContext 
            = SynchronizationContext.Current;

        /// <summary>
        /// Gets the single instance of the event managed by this EventAggregator.
        /// Multiple calls to this method with the same <typeparamref name="TEventType"/>
        /// returns the same event instance.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to get. This must inherit
        /// from <see cref="EventBase"/>.</typeparam>
        /// <returns>A singleton instance of an event object of type
        /// <typeparamref name="TEventType"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", 
            "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "Best solution here")]
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            lock (this.events)
            {
                EventBase existingEvent;

                if (!this.events.TryGetValue(typeof(TEventType), out existingEvent))
                {
                    var newEvent = new TEventType();
                    newEvent.SynchronizationContext = this.syncContext;
                    this.events[typeof(TEventType)] = newEvent;

                    return newEvent;
                } // if

                return (TEventType)existingEvent;
            } // lock
        } // GetEvent()
    } // EventAggregator
} // Prism.EventAggregator
