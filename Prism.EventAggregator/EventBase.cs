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
// <copyright file="EventBase.cs" company="Tethys">
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
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Defines a base class to publish and subscribe to events.
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// The subscriptions.
        /// </summary>
        private readonly List<IEventSubscription> subscriptions 
            = new List<IEventSubscription>();

        /// <summary>
        /// Gets or sets the SynchronizationContext (by the EventAggregator
        /// for UI Thread Dispatching).
        /// </summary>
        public SynchronizationContext SynchronizationContext { get; set; }

        /// <summary>
        /// Gets the list of current subscriptions.
        /// </summary>
        /// <value>The current subscribers.</value>
        protected ICollection<IEventSubscription> Subscriptions
        {
            get { return this.subscriptions; }
        }

        /// <summary>
        /// Adds the specified <see cref="IEventSubscription"/> to the
        /// subscribers' collection.
        /// </summary>
        /// <param name="eventSubscription">The subscriber.</param>
        /// <returns>The <see cref="SubscriptionToken"/> that uniquely
        /// identifies every subscriber.</returns>
        /// <remarks>
        /// Adds the subscription to the internal list and assigns it a
        /// new <see cref="SubscriptionToken"/>.
        /// </remarks>
        protected virtual SubscriptionToken InternalSubscribe(
            IEventSubscription eventSubscription)
        {
            if (eventSubscription == null)
            {
                throw new ArgumentNullException("eventSubscription");
            } // if

            eventSubscription.SubscriptionToken = new SubscriptionToken(this.Unsubscribe);

            lock (this.Subscriptions)
            {
                this.Subscriptions.Add(eventSubscription);
            } // lock

            return eventSubscription.SubscriptionToken;
        } // InternalSubscribe()

        /// <summary>
        /// Calls all the execution strategies exposed by the list of
        /// <see cref="IEventSubscription"/>.
        /// </summary>
        /// <param name="arguments">The arguments that will be passed to the
        /// listeners.</param>
        /// <remarks>Before executing the strategies, this class will prune all
        /// the subscribers from the
        /// list that return a <see langword="null" /> <see cref="Action{T}"/>
        /// when calling the <see cref="IEventSubscription.GetExecutionStrategy"/>
        /// method.</remarks>
        protected virtual void InternalPublish(params object[] arguments)
        {
            var executionStrategies = this.PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            } // foreach
        } // InternalPublish()

        /// <summary>
        /// Removes the subscriber matching the <see cref="SubscriptionToken"/>.
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> 
        /// returned by <see cref="EventBase"/> while subscribing to the 
        /// event.</param>
        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (this.Subscriptions)
            {
                var subscription = this.Subscriptions.FirstOrDefault(
                    evt => evt.SubscriptionToken == token);
                if (subscription != null)
                {
                    this.Subscriptions.Remove(subscription);
                } // if
            } // lock
        } // Unsubscribe()

        /// <summary>
        /// Returns <see langword="true"/> if there is a subscriber matching
        /// <see cref="SubscriptionToken"/>.
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> returned by
        /// <see cref="EventBase"/> while subscribing to the event.</param>
        /// <returns><see langword="true"/> if there is a <see cref="SubscriptionToken"/>
        /// that matches; otherwise <see langword="false"/>.</returns>
        public virtual bool Contains(SubscriptionToken token)
        {
            lock (this.Subscriptions)
            {
                var subscription = this.Subscriptions.FirstOrDefault(
                    evt => evt.SubscriptionToken == token);
                return subscription != null;
            } // lock
        } // Contains()

        /// <summary>
        /// Prunes the and return strategies.
        /// </summary>
        /// <returns>A list of actions.</returns>
        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            var returnList = new List<Action<object[]>>();

            lock (this.Subscriptions)
            {
                for (var i = this.Subscriptions.Count - 1; i >= 0; i--)
                {
                    var listItem =
                        this.subscriptions[i].GetExecutionStrategy();

                    if (listItem == null)
                    {
                        // Prune from main list. Log?
                        this.subscriptions.RemoveAt(i);
                    }
                    else
                    {
                        returnList.Add(listItem);
                    } // if
                } // if
            } // lock

            return returnList;
        } // PruneAndReturnStrategies()
    } // EventBase
} // Prism.EventAggregator