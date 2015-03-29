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
// <copyright file="BackgroundEventSubscription.cs" company="Tethys">
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
    /// <see cref="EventSubscription{TPayload}.Action"/> delegate in a 
    /// background thread.
    /// </summary>
    /// <typeparam name="TPayload">The type to use for the generic 
    /// <see cref="System.Action{TPayload}"/> and <see cref="Predicate{TPayload}"/> 
    /// types.</typeparam>
    public class BackgroundEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="BackgroundEventSubscription{TPayload}"/> class.
        /// </summary>
        /// <param name="actionReference">A reference to a delegate of type 
        /// <see cref="System.Action{TPayload}"/>.</param>
        /// <param name="filterReference">A reference to a delegate of type 
        /// <see cref="Predicate{TPayload}"/>.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="actionReference"/> 
        /// or <see paramref="filterReference"/> are <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">When the target of 
        /// <paramref name="actionReference"/> is not of type <see cref="System.Action{TPayload}"/>,
        /// or the target of <paramref name="filterReference"/> is not of type 
        /// <see cref="Predicate{TPayload}"/>.</exception>
        public BackgroundEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
            : base(actionReference, filterReference)
        {
        } // BackgroundEventSubscription()

        /// <summary>
        /// Invokes the specified <see cref="System.Action{TPayload}"/> in an 
        /// asynchronous thread by using a <see cref="ThreadPool"/>.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="argument">The payload to pass <paramref name="action"/> 
        /// while invoking it.</param>
        public override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            ThreadPool.QueueUserWorkItem(o => action(argument));
        } // InvokeAction()
    } // BackgroundEventSubscription()
} // Prism.EventAggregator