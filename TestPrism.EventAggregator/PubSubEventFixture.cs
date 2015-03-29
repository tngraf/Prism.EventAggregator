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
// <copyright file="PubSubEventFixture.cs" company="Tethys">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Prism.EventAggregator;

    [TestClass]
    public class PubSubEventFixture
    {
        [TestMethod]
        public void CanSubscribeAndRaiseEvent()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            bool published = false;
            pubSubEvent.Subscribe(delegate { published = true; }, 
                ThreadOption.PublisherThread, true, delegate { return true; });
            pubSubEvent.Publish(null);

            Assert.IsTrue(published);
        }

        [TestMethod]
        public void CanSubscribeAndRaiseCustomEvent()
        {
            var customEvent = new TestablePubSubEvent<Payload>();
            Payload payload = new Payload();
            var action = new ActionHelper();
            customEvent.Subscribe(action.Action);

            customEvent.Publish(payload);

            Assert.AreSame(action.ActionArg<Payload>(), payload);
        }

        [TestMethod]
        public void CanHaveMultipleSubscribersAndRaiseCustomEvent()
        {
            var customEvent = new TestablePubSubEvent<Payload>();
            Payload payload = new Payload();
            var action1 = new ActionHelper();
            var action2 = new ActionHelper();
            customEvent.Subscribe(action1.Action);
            customEvent.Subscribe(action2.Action);

            customEvent.Publish(payload);

            Assert.AreSame(action1.ActionArg<Payload>(), payload);
            Assert.AreSame(action2.ActionArg<Payload>(), payload);
        }

        [TestMethod]
        public void SubscribeTakesExecuteDelegateThreadOptionAndFilter()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();
            var action = new ActionHelper();
            pubSubEvent.Subscribe(action.Action);

            pubSubEvent.Publish("test");

            Assert.AreEqual("test", action.ActionArg<string>());

        }

        [TestMethod]
        public void FilterEnablesActionTarget()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();
            var goodFilter = new MockFilter { FilterReturnValue = true };
            var actionGoodFilter = new ActionHelper();
            var badFilter = new MockFilter { FilterReturnValue = false };
            var actionBadFilter = new ActionHelper();
            pubSubEvent.Subscribe(actionGoodFilter.Action, ThreadOption.PublisherThread, 
                true, goodFilter.FilterString);
            pubSubEvent.Subscribe(actionBadFilter.Action, ThreadOption.PublisherThread, 
                true, badFilter.FilterString);

            pubSubEvent.Publish("test");

            Assert.IsTrue(actionGoodFilter.ActionCalled);
            Assert.IsFalse(actionBadFilter.ActionCalled);

        }

        [TestMethod]
        public void SubscribeDefaultsThreadOptionAndNoFilter()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext calledSyncContext = null;
            var myAction = new ActionHelper
            {
                ActionToExecute =
                    () => calledSyncContext = SynchronizationContext.Current
            };
            pubSubEvent.Subscribe(myAction.Action);

            pubSubEvent.Publish("test");

            Assert.AreEqual(SynchronizationContext.Current, calledSyncContext);
        }

        [TestMethod]
        public void ShouldUnsubscribeFromPublisherThread()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var actionEvent = new ActionHelper();
            pubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.PublisherThread);

            Assert.IsTrue(pubSubEvent.Contains(actionEvent.Action));
            pubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(pubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void UnsubscribeShouldNotFailWithNonSubscriber()
        {
            TestablePubSubEvent<string> pubSubEvent = new TestablePubSubEvent<string>();

            Action<string> subscriber = delegate { };
            pubSubEvent.Unsubscribe(subscriber);
        }

        [TestMethod]
        public void ShouldUnsubscribeFromBackgroundThread()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var actionEvent = new ActionHelper();
            pubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.BackgroundThread);

            Assert.IsTrue(pubSubEvent.Contains(actionEvent.Action));
            pubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(pubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeFromUIThread()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            pubSubEvent.SynchronizationContext = new SynchronizationContext();

            var actionEvent = new ActionHelper();
            pubSubEvent.Subscribe(
                actionEvent.Action,
                ThreadOption.UIThread);

            Assert.IsTrue(pubSubEvent.Contains(actionEvent.Action));
            pubSubEvent.Unsubscribe(actionEvent.Action);
            Assert.IsFalse(pubSubEvent.Contains(actionEvent.Action));
        }

        [TestMethod]
        public void ShouldUnsubscribeASingleDelegate()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var callCount = 0;

            var actionEvent = new ActionHelper
            {
                ActionToExecute = () => callCount++
            };
            pubSubEvent.Subscribe(actionEvent.Action);
            pubSubEvent.Subscribe(actionEvent.Action);

            pubSubEvent.Publish(null);
            Assert.AreEqual(2, callCount);

            callCount = 0;
            pubSubEvent.Unsubscribe(actionEvent.Action);
            pubSubEvent.Publish(null);
            Assert.AreEqual(1, callCount);
        }

        [TestMethod]
        public void ShouldNotExecuteOnGarbageCollectedDelegateReferenceWhenNotKeepAlive()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var externalAction = new ExternalAction();
            pubSubEvent.Subscribe(externalAction.ExecuteAction);

            pubSubEvent.Publish("testPayload");
            Assert.AreEqual("testPayload", externalAction.PassedValue);

            var actionEventReference = new WeakReference(externalAction);
            externalAction = null;
            GC.Collect();
            Assert.IsFalse(actionEventReference.IsAlive);

            pubSubEvent.Publish("testPayload");
        }

        [TestMethod]
        public void ShouldNotExecuteOnGarbageCollectedFilterReferenceWhenNotKeepAlive()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var wasCalled = false;
            var actionEvent = new ActionHelper
            {
                ActionToExecute = () => wasCalled = true
            };

            var filter = new ExternalFilter();
            pubSubEvent.Subscribe(actionEvent.Action, ThreadOption.PublisherThread, 
                false, filter.AlwaysTrueFilter);

            pubSubEvent.Publish("testPayload");
            Assert.IsTrue(wasCalled);

            wasCalled = false;
            var filterReference = new WeakReference(filter);
            filter = null;
            GC.Collect();
            Assert.IsFalse(filterReference.IsAlive);

            pubSubEvent.Publish("testPayload");
            Assert.IsFalse(wasCalled);
        }

        [TestMethod]
        public void CanAddSubscriptionWhileEventIsFiring()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var emptyAction = new ActionHelper();
            var subscriptionAction = new ActionHelper
            {
                ActionToExecute = () => pubSubEvent.Subscribe(emptyAction.Action)
            };

            pubSubEvent.Subscribe(subscriptionAction.Action);

            Assert.IsFalse(pubSubEvent.Contains(emptyAction.Action));

            pubSubEvent.Publish(null);

            Assert.IsTrue(pubSubEvent.Contains(emptyAction.Action));
        }

        [TestMethod]
        public void InlineDelegateDeclarationsDoesNotGetCollectedIncorrectlyWithWeakReferences()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            var published = false;
            pubSubEvent.Subscribe(delegate { published = true; }, 
                ThreadOption.PublisherThread, false, delegate { return true; });
            GC.Collect();
            pubSubEvent.Publish(null);

            Assert.IsTrue(published);
        }

        [TestMethod]
        public void ShouldNotGarbageCollectDelegateReferenceWhenUsingKeepAlive()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();

            var externalAction = new ExternalAction();
            pubSubEvent.Subscribe(externalAction.ExecuteAction, ThreadOption.PublisherThread, true);

            var actionEventReference = new WeakReference(externalAction);
            externalAction = null;
            GC.Collect();
            GC.Collect();
            Assert.IsTrue(actionEventReference.IsAlive);

            pubSubEvent.Publish("testPayload");

            Assert.AreEqual("testPayload", ((ExternalAction)actionEventReference.Target).PassedValue);
        }

        [TestMethod]
        public void RegisterReturnsTokenThatCanBeUsedToUnsubscribe()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            var emptyAction = new ActionHelper();

            var token = pubSubEvent.Subscribe(emptyAction.Action);
            pubSubEvent.Unsubscribe(token);

            Assert.IsFalse(pubSubEvent.Contains(emptyAction.Action));
        }

        [TestMethod]
        public void ContainsShouldSearchByToken()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            var emptyAction = new ActionHelper();
            var token = pubSubEvent.Subscribe(emptyAction.Action);

            Assert.IsTrue(pubSubEvent.Contains(token));

            pubSubEvent.Unsubscribe(emptyAction.Action);
            Assert.IsFalse(pubSubEvent.Contains(token));
        }

        [TestMethod]
        public void SubscribeDefaultsToPublisherThread()
        {
            var pubSubEvent = new TestablePubSubEvent<string>();
            Action<string> action = delegate { };
            var token = pubSubEvent.Subscribe(action, true);

            Assert.AreEqual(1, pubSubEvent.BaseSubscriptions.Count);
            Assert.AreEqual(typeof(EventSubscription<string>), 
                pubSubEvent.BaseSubscriptions.ElementAt(0).GetType());
        }

        public class ExternalFilter
        {
            public bool AlwaysTrueFilter(string value)
            {
                return true;
            }
        }

        public class ExternalAction
        {
            public string PassedValue;
            public void ExecuteAction(string value)
            {
                this.PassedValue = value;
            }
        }

        public class TestablePubSubEvent<TPayload> : PubSubEvent<TPayload>
        {
            public ICollection<IEventSubscription> BaseSubscriptions
            {
                get { return base.Subscriptions; }
            }
        }

        public class Payload { }
    }

    public class ActionHelper
    {
        public bool ActionCalled;
        public Action ActionToExecute = null;
        private object actionArg;

        public T ActionArg<T>()
        {
            return (T)this.actionArg;
        }

        public void Action(PubSubEventFixture.Payload arg)
        {
            this.Action((object)arg);
        }

        public void Action(string arg)
        {
            this.Action((object)arg);
        }

        public void Action(object arg)
        {
            this.actionArg = arg;
            this.ActionCalled = true;
            if (this.ActionToExecute != null)
            {
                this.ActionToExecute.Invoke();
            }
        }
    }

    public class MockFilter
    {
        public bool FilterReturnValue;

        public bool FilterString(string arg)
        {
            return this.FilterReturnValue;
        }
    }
}