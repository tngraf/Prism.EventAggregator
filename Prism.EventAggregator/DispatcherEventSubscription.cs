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
// <copyright file="DispatcherEventSubscription.cs" company="Tethys">
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
    using System.Threading;

    /// <summary>
    /// Extends <see cref="EventSubscription{TPayload}"/> to invoke the
    /// <see cref="EventSubscription{TPayload}.Action"/> delegate
    /// in a specific <see cref="SynchronizationContext"/>.
    /// </summary>
    /// <typeparam name="TPayload">The type to use for the generic
    /// <see cref="System.Action{TPayload}"/> and <see cref="Predicate{TPayload}"/>
    /// types.</typeparam>
    public class DispatcherEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        /// <summary>
        /// The synchronization context.
        /// </summary>
        private readonly SynchronizationContext syncContext;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DispatcherEventSubscription{TPayload}"/> class.
        /// </summary>
        /// <param name="actionReference">A reference to a delegate of type
        /// <see cref="System.Action{TPayload}"/>.</param>
        /// <param name="filterReference">A reference to a delegate of type
        /// <see cref="Predicate{TPayload}"/>.</param>
        /// <param name="context">The synchronization context to use for
        /// UI thread dispatching.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="actionReference"/>
        /// or <see paramref="filterReference"/> are <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">When the target of
        /// <paramref name="actionReference"/> is not of type
        /// <see cref="System.Action{TPayload}"/>,
        /// or the target of <paramref name="filterReference"/> is not of type
        /// <see cref="Predicate{TPayload}"/>.</exception>
        public DispatcherEventSubscription(IDelegateReference actionReference, 
            IDelegateReference filterReference, SynchronizationContext context)
            : base(actionReference, filterReference)
        {
            this.syncContext = context;
        } // DispatcherEventSubscription()

        /// <summary>
        /// Invokes the specified <see cref="System.Action{TPayload}"/> asynchronously
        /// in the specified <see cref="SynchronizationContext"/>.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="argument">The payload to pass <paramref name="action"/> while invoking it.</param>
        public override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            this.syncContext.Post(o => action((TPayload)o), argument);
        } // InvokeAction()
    } // DispatcherEventSubscription()
} // Prism.EventAggregator