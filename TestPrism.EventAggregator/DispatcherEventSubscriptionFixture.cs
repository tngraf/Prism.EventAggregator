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
// <copyright file="DispatcherEventSubscriptionFixture.cs" company="Tethys">
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

namespace TestPrism.EventAggregator
{
    using System;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Prism.EventAggregator;

    [TestClass]
    public class DispatcherEventSubscriptionFixture
    {
        [TestMethod]
        public void ShouldCallInvokeOnDispatcher()
        {
            IDelegateReference actionDelegateReference = new MockDelegateReference
            {
                Target = (Action<object>)(arg =>
                {
                    return;
                })
            };

            IDelegateReference filterDelegateReference = new MockDelegateReference
            {
                Target = (Predicate<object>)(arg => true)
            };
            var mockSyncContext = new MockSynchronizationContext();

            var eventSubscription = new DispatcherEventSubscription<object>(
                actionDelegateReference, filterDelegateReference, mockSyncContext);

            eventSubscription.GetExecutionStrategy().Invoke(new object[0]);

            Assert.IsTrue(mockSyncContext.InvokeCalled);
        }

        [TestMethod]
        public void ShouldPassParametersCorrectly()
        {
            IDelegateReference actionDelegateReference = new MockDelegateReference
            {
                Target =
                    (Action<object>)(arg1 =>
                    {
                        return;
                    })
            };
            IDelegateReference filterDelegateReference = new MockDelegateReference
            {
                Target = (Predicate<object>)(arg => true)
            };

            var mockSyncContext = new MockSynchronizationContext();

            DispatcherEventSubscription<object> eventSubscription = new DispatcherEventSubscription<object>(actionDelegateReference, filterDelegateReference, mockSyncContext);

            var executionStrategy = eventSubscription.GetExecutionStrategy();
            Assert.IsNotNull(executionStrategy);

            object argument1 = new object();

            executionStrategy.Invoke(new[] { argument1 });

            Assert.AreSame(argument1, mockSyncContext.InvokeArg);
        }
    }

    internal class MockSynchronizationContext : SynchronizationContext
    {
        public bool InvokeCalled;
        public object InvokeArg;

        public override void Post(SendOrPostCallback d, object state)
        {
            this.InvokeCalled = true;
            this.InvokeArg = state;
        }
    }
}
