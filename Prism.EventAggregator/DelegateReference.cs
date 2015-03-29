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
// <copyright file="DelegateReference.cs" company="Tethys">
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
    using System.Reflection;

    /// <summary>
    /// Represents a reference to a <see cref="Delegate"/> that may contain a
    /// <see cref="WeakReference"/> to the target. This class is used
    /// internally by the Prism Library.
    /// </summary>
    public class DelegateReference : IDelegateReference
    {
        /// <summary>
        /// The delegate.
        /// </summary>
        private readonly Delegate theDelegate;

        /// <summary>
        /// The weak reference.
        /// </summary>
        private readonly WeakReference weakReference;

        /// <summary>
        /// The method.
        /// </summary>
        private readonly MethodInfo method;

        /// <summary>
        /// The delegate type.
        /// </summary>
        private readonly Type delegateType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateReference"/> class.
        /// </summary>
        /// <param name="delegate">The original <see cref="Delegate"/> to create a 
        /// reference for.</param>
        /// <param name="keepReferenceAlive">If <see langword="false" /> the class 
        /// will create a weak reference to the delegate, allowing it to be garbage 
        /// collected. Otherwise it will keep a strong reference to the target.</param>
        /// <exception cref="ArgumentNullException">If the passed <paramref name="delegate"/>
        /// is not assignable to <see cref="Delegate"/>.</exception>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            } // if

            if (keepReferenceAlive)
            {
                this.theDelegate = @delegate;
            }
            else
            {
                this.weakReference = new WeakReference(@delegate.Target);
                this.method = @delegate.Method;
                this.delegateType = @delegate.GetType();
            } // if
        } // DelegateReference()

        /// <summary>
        /// Gets the <see cref="Delegate" /> (the target) referenced by the 
        /// current <see cref="DelegateReference"/> object.
        /// </summary>
        /// <value><see langword="null"/> if the object referenced by the 
        /// current <see cref="DelegateReference"/> object has been garbage 
        /// collected; otherwise, a reference to the <see cref="Delegate"/> 
        /// referenced by the current <see cref="DelegateReference"/> object.
        /// </value>
        public Delegate Target
        {
            get
            {
                if (this.theDelegate != null)
                {
                    return this.theDelegate;
                } // if

                return this.TryGetDelegate();
            }
        }

        /// <summary>
        /// Tries to get the delegate.
        /// </summary>
        /// <returns>A <see cref="Delegate"/> or null.</returns>
        private Delegate TryGetDelegate()
        {
            if (this.method.IsStatic)
            {
                return Delegate.CreateDelegate(this.delegateType, null, this.method);
            } // if

            var target = this.weakReference.Target;
            if (target != null)
            {
                return Delegate.CreateDelegate(this.delegateType, target, this.method);
            } // if

            return null;
        } // TryGetDelegate()
    } // DelegateReference
} // Prism.EventAggregator