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
// <copyright file="DelegateReferenceFixture.cs" company="Tethys">
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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Prism.EventAggregator;

    [TestClass]
    public class DelegateReferenceFixture
    {
        [TestMethod]
        public void KeepAlivePreventsDelegateFromBeingCollected()
        {
            var delegates = new SomeClassHandler();
            var delegateReference = new DelegateReference(
                (Action<string>)delegates.DoEvent, true);

            delegates = null;
            GC.Collect();

            Assert.IsNotNull(delegateReference.Target);
        }

        [TestMethod]
        public void NotKeepAliveAllowsDelegateToBeCollected()
        {
            var delegates = new SomeClassHandler();
            var delegateReference = new DelegateReference(
                (Action<string>)delegates.DoEvent, false);

            delegates = null;
            GC.Collect();

            Assert.IsNull(delegateReference.Target);
        }

        [TestMethod]
        public void NotKeepAliveKeepsDelegateIfStillAlive()
        {
            var delegates = new SomeClassHandler();
            var delegateReference = new DelegateReference(
                (Action<string>)delegates.DoEvent, false);

            GC.Collect();

            Assert.IsNotNull(delegateReference.Target);

            // Makes delegates ineligible for garbage collection until this point 
            // (to prevent oompiler optimizations that may release the referenced
            // object prematurely).
            GC.KeepAlive(delegates);
            delegates = null;
            GC.Collect();

            Assert.IsNull(delegateReference.Target);
        }

        [TestMethod]
        public void TargetShouldReturnAction()
        {
            var classHandler = new SomeClassHandler();
            Action<string> myAction = classHandler.MyAction;

            var weakAction = new DelegateReference(myAction, false);

            ((Action<string>)weakAction.Target)("payload");
            Assert.AreEqual("payload", classHandler.MyActionArg);
        }

        [TestMethod]
        public void ShouldAllowCollectionOfOriginalDelegate()
        {
            var classHandler = new SomeClassHandler();
            Action<string> myAction = classHandler.MyAction;

            var weakAction = new DelegateReference(myAction, false);

            var originalAction = new WeakReference(myAction);
            myAction = null;
            GC.Collect();
            Assert.IsFalse(originalAction.IsAlive);

            ((Action<string>)weakAction.Target)("payload");
            Assert.AreEqual("payload", classHandler.MyActionArg);
        }

        [TestMethod]
        public void ShouldReturnNullIfTargetNotAlive()
        {
            var handler = new SomeClassHandler();
            var weakHandlerRef = new WeakReference(handler);

            var action = new DelegateReference(
                (Action<string>)handler.DoEvent, false);

            handler = null;
            GC.Collect();
            Assert.IsFalse(weakHandlerRef.IsAlive);

            Assert.IsNull(action.Target);
        }

        [TestMethod]
        public void WeakDelegateWorksWithStaticMethodDelegates()
        {
            var action = new DelegateReference(
                (Action)SomeClassHandler.StaticMethod, false);

            Assert.IsNotNull(action.Target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDelegateThrows()
        {
            var action = new DelegateReference(null, true);
        }

        public class SomeClassHandler
        {
            public string MyActionArg;

            public void DoEvent(string value)
            {
                var myValue = value;
            }

            public static void StaticMethod()
            {
#pragma warning disable 0219
                int i = 0;
#pragma warning restore 0219
            }

            public void MyAction(string arg)
            {
                this.MyActionArg = arg;
            }
        }
    }
}
