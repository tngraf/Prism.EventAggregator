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
// <copyright file="SimpleSubscriberForm.cs" company="Tethys">
// Copyright (c) 2015 Thomas Graf.
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

namespace Prism.EventAggregator.DemoApplication.WinForms
{
    using System;
    using System.Windows.Forms;

    using Prism.EventAggregator;

    /// <summary>
    /// A simple form with a subscriber.
    /// </summary>
    public partial class SimpleSubscriberForm : Form
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The token for the subscriber.
        /// </summary>
        private SubscriptionToken tokenSubscriber;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSubscriberForm"/> class.
        /// </summary>
        public SimpleSubscriberForm()
        {
            this.InitializeComponent();
        } // SimpleSubscriberForm()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region UI HANDLING
        /// <summary>
        /// Handles the Load event of the SimpleSubscriberForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SimpleSubscriberFormLoad(object sender, EventArgs e)
        {
            this.DoSubscribe(true);
        } // SimpleSubscriberFormLoad()

        /// <summary>
        /// Handles the FormClosing event of the SimpleSubscriberForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void SimpleSubscriberFormFormClosing(object sender, FormClosingEventArgs e)
        {
#if !TEST_FOR_PROPER_WEAK_EVENTS
            this.DoSubscribe(false);
#endif
        } // SimpleSubscriberFormFormClosing()
        #endregion // UI HANDLING

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Does the subscription.
        /// </summary>
        /// <param name="subscribe">if set to <c>true</c> subscribes.</param>
        private void DoSubscribe(bool subscribe)
        {
            var evt = ServiceLocator.EventAggregator.GetEvent<SimpleMessageEvent>();
            if (subscribe)
            {
                this.tokenSubscriber = evt.Subscribe(this.OnMessageForSubscriber, ThreadOption.UIThread);
            }
            else
            {
                if (this.tokenSubscriber != null)
                {
                    evt.Unsubscribe(this.tokenSubscriber);
                } // if
            } // if
        } // DoSubscribe()

        /// <summary>
        /// Called when a message for the subscriber has been published.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void OnMessageForSubscriber(IMessage msg)
        {
            this.txtData.AppendText(msg.MessageText + "\r\n");
        } // OnMessageForSubscriber()
        #endregion // PRIVATE METHODS
    } // SimpleSubscriberForm
} // Prism.EventAggregator.DemoApplication.WinForms
