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
// <copyright file="SubscriptionToken.cs" company="Tethys">
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
    /// Subscription token returned from <see cref="EventBase"/> on subscribe.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", 
        "CA1063:ImplementIDisposableCorrectly", 
        Justification = "Should never have a need for a finalizer, hence no need for Dispole(bool)")]
    public class SubscriptionToken : IEquatable<SubscriptionToken>, IDisposable
    {
        /// <summary>
        /// The token.
        /// </summary>
        private readonly Guid token;

        /// <summary>
        /// The unsubscribe action-
        /// </summary>
        private Action<SubscriptionToken> unsubscribeAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionToken" /> class.
        /// </summary>
        /// <param name="unsubscribeAction">The unsubscribe action.</param>
        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            this.unsubscribeAction = unsubscribeAction;
            this.token = Guid.NewGuid();
        } // SubscriptionToken()

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the current object is equal to the <paramref name="other" /> 
        /// parameter; otherwise, <see langword="false"/>.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(SubscriptionToken other)
        {
            if (other == null)
            {
                return false;
            } // if

            return object.Equals(this.token, other.token);
        } // Equals()

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal 
        /// to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object" /> is equal to the 
        /// current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with 
        /// the current <see cref="T:System.Object" />. </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" />
        /// parameter is null.</exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            } // if

            return this.Equals(obj as SubscriptionToken);
        } // Equals()

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            return this.token.GetHashCode();
        } // GetHashCode()

        /// <summary>
        /// Disposes the SubscriptionToken, removing the subscription from the
        /// corresponding <see cref="EventBase"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", 
            "CA1063:ImplementIDisposableCorrectly", 
            Justification = "Should never have need for a finalizer, hence no need for Dispose(bool).")]
        public virtual void Dispose()
        {
            // While the SubsctiptionToken class implements IDisposable, in the
            // case of weak subscriptions (i.e. keepSubscriberReferenceAlive set 
            // to false in the Subscribe method) it's not necessary to unsubscribe,
            // as no resources should be kept alive by the event subscription. 
            // In such cases, if a warning is issued, it could be suppressed.
            if (this.unsubscribeAction != null)
            {
                this.unsubscribeAction(this);
                this.unsubscribeAction = null;
            } // if

            GC.SuppressFinalize(this);
        } // Dispose()
    } // SubscriptionToken
} // Prism.EventAggregator