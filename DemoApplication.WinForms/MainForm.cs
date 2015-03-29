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
// <copyright file="MainForm.cs" company="Tethys">
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
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The token for subscriber 1.
        /// </summary>
        private SubscriptionToken tokenSubscriber1;

        /// <summary>
        /// The token for subscriber 2.
        /// </summary>
        private SubscriptionToken tokenSubscriber2;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        } // MainForm()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region UI HANDLING
        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance 
        /// containing the event data.</param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            ServiceLocator.Initialize();

            this.checkSubscribe1.Checked = true;
        } // MainFormLoad()

        /// <summary>
        /// Handles the Click event of the <c>btnPublisher1</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance 
        /// containing the event data.</param>
        private void BtnPublisher1Click(object sender, EventArgs e)
        {
            ServiceLocator.EventAggregator.GetEvent<SimpleMessageEvent>().Publish(
                new global::Prism.EventAggregator.DemoApplication.WinForms.Message("Message from Subscriber 1"));
        } // BtnPublisher1Click()

        /// <summary>
        /// Handles the Click event of the <c>btnPublisher2</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance 
        /// containing the event data.</param>
        private void BtnPublisher2Click(object sender, EventArgs e)
        {
            ServiceLocator.EventAggregator.GetEvent<SimpleMessageEvent>().Publish(
                new global::Prism.EventAggregator.DemoApplication.WinForms.Message("Message from Subscriber 2"));
        } // BtnPublisher2Click()

        /// <summary>
        /// Handles the CheckedChanged event of the checkSubscribe1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance 
        /// containing the event data.</param>
        private void CheckSubscribe1CheckedChanged(object sender, EventArgs e)
        {
            this.DoSubscribe1(this.checkSubscribe1.Checked);
        } // CheckSubscribe1CheckedChanged()

        /// <summary>
        /// Handles the CheckedChanged event of the checkSubscribe2 control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckSubscribe2CheckedChanged(object sender, EventArgs e)
        {
            this.DoSubscribe2(this.checkSubscribe2.Checked);
        } // CheckSubscribe2CheckedChanged()

        /// <summary>
        /// Handles the Click event of the <c>btnOpenDialog</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnOpenDialogClick(object sender, EventArgs e)
        {
            var form = new SimpleSubscriberForm();
            form.Show(this);
        } // BtnOpenDialogClick()

        /// <summary>
        /// Handles the Click event of the <c>btnForceGC</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnForceGcClick(object sender, EventArgs e)
        {
            GC.Collect();
        } // BtnForceGcClick()
        #endregion // UI HANDLING

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Does the subscription for subscriber 1.
        /// </summary>
        /// <param name="subscribe">if set to <c>true</c> subscribes.</param>
        private void DoSubscribe1(bool subscribe)
        {
            var evt = ServiceLocator.EventAggregator.GetEvent<SimpleMessageEvent>();
            if (subscribe)
            {
                this.tokenSubscriber1 = evt.Subscribe(this.OnMessageForSubscriber1, 
                    ThreadOption.UIThread);
            }
            else
            {
                if (this.tokenSubscriber1 != null)
                {
                    evt.Unsubscribe(this.tokenSubscriber1);
                } // if
            } // if
        } // DoSubscribe1()

        /// <summary>
        /// Does the subscription for subscriber 2.
        /// </summary>
        /// <param name="subscribe">if set to <c>true</c> subscribes.</param>
        private void DoSubscribe2(bool subscribe)
        {
            var evt = ServiceLocator.EventAggregator.GetEvent<SimpleMessageEvent>();
            if (subscribe)
            {
                this.tokenSubscriber2 = evt.Subscribe(this.OnMessageForSubscriber2, 
                    ThreadOption.UIThread);
            }
            else
            {
                if (this.tokenSubscriber2 != null)
                {
                    evt.Unsubscribe(this.tokenSubscriber2);
                } // if
            } // if
        } // DoSubscribe2()

        /// <summary>
        /// Called when a message for subscriber 1 has been published.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void OnMessageForSubscriber1(IMessage msg)
        {
            this.txtSubscriber1.AppendText(msg.MessageText + "\r\n");
        } // OnMessageForSubscriber1()

        /// <summary>
        /// Called when a message for subscriber 2 has been published.
        /// </summary>
        /// <param name="msg">The message.</param>
        private void OnMessageForSubscriber2(IMessage msg)
        {
            this.txtSubscriber2.AppendText(msg.MessageText + "\r\n");
        } // OnMessageForSubscriber2()
        #endregion // PRIVATE METHODS
     } // MainForm
} // Prism.EventAggregator.DemoApplication.WinForms
