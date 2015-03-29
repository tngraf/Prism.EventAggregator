namespace Prism.EventAggregator.DemoApplication.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPublisher1 = new System.Windows.Forms.Button();
            this.txtSubscriber1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPublisher2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkSubscribe2 = new System.Windows.Forms.CheckBox();
            this.checkSubscribe1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubscriber2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenDialog = new System.Windows.Forms.Button();
            this.btnForceGC = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPublisher1
            // 
            this.btnPublisher1.Location = new System.Drawing.Point(14, 24);
            this.btnPublisher1.Name = "btnPublisher1";
            this.btnPublisher1.Size = new System.Drawing.Size(120, 23);
            this.btnPublisher1.TabIndex = 0;
            this.btnPublisher1.Text = "Publisher 1";
            this.btnPublisher1.UseVisualStyleBackColor = true;
            this.btnPublisher1.Click += new System.EventHandler(this.BtnPublisher1Click);
            // 
            // txtSubscriber1
            // 
            this.txtSubscriber1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSubscriber1.Location = new System.Drawing.Point(14, 40);
            this.txtSubscriber1.Multiline = true;
            this.txtSubscriber1.Name = "txtSubscriber1";
            this.txtSubscriber1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubscriber1.Size = new System.Drawing.Size(225, 124);
            this.txtSubscriber1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPublisher2);
            this.groupBox1.Controls.Add(this.btnPublisher1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Publisher ";
            // 
            // btnPublisher2
            // 
            this.btnPublisher2.Location = new System.Drawing.Point(140, 24);
            this.btnPublisher2.Name = "btnPublisher2";
            this.btnPublisher2.Size = new System.Drawing.Size(120, 23);
            this.btnPublisher2.TabIndex = 1;
            this.btnPublisher2.Text = "Publisher 2";
            this.btnPublisher2.UseVisualStyleBackColor = true;
            this.btnPublisher2.Click += new System.EventHandler(this.BtnPublisher2Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkSubscribe2);
            this.groupBox2.Controls.Add(this.checkSubscribe1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSubscriber2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtSubscriber1);
            this.groupBox2.Location = new System.Drawing.Point(12, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 170);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // checkSubscribe2
            // 
            this.checkSubscribe2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSubscribe2.AutoSize = true;
            this.checkSubscribe2.Location = new System.Drawing.Point(339, 23);
            this.checkSubscribe2.Name = "checkSubscribe2";
            this.checkSubscribe2.Size = new System.Drawing.Size(73, 17);
            this.checkSubscribe2.TabIndex = 5;
            this.checkSubscribe2.Text = "Subscribe";
            this.checkSubscribe2.UseVisualStyleBackColor = true;
            this.checkSubscribe2.CheckedChanged += new System.EventHandler(this.CheckSubscribe2CheckedChanged);
            // 
            // checkSubscribe1
            // 
            this.checkSubscribe1.AutoSize = true;
            this.checkSubscribe1.Location = new System.Drawing.Point(158, 23);
            this.checkSubscribe1.Name = "checkSubscribe1";
            this.checkSubscribe1.Size = new System.Drawing.Size(73, 17);
            this.checkSubscribe1.TabIndex = 2;
            this.checkSubscribe1.Text = "Subscribe";
            this.checkSubscribe1.UseVisualStyleBackColor = true;
            this.checkSubscribe1.CheckedChanged += new System.EventHandler(this.CheckSubscribe1CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subscriber 2";
            // 
            // txtSubscriber2
            // 
            this.txtSubscriber2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriber2.Location = new System.Drawing.Point(244, 40);
            this.txtSubscriber2.Multiline = true;
            this.txtSubscriber2.Name = "txtSubscriber2";
            this.txtSubscriber2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubscriber2.Size = new System.Drawing.Size(186, 124);
            this.txtSubscriber2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Subscriber 1";
            // 
            // btnOpenDialog
            // 
            this.btnOpenDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDialog.Location = new System.Drawing.Point(328, 263);
            this.btnOpenDialog.Name = "btnOpenDialog";
            this.btnOpenDialog.Size = new System.Drawing.Size(120, 23);
            this.btnOpenDialog.TabIndex = 2;
            this.btnOpenDialog.Text = "Open Dialog";
            this.btnOpenDialog.UseVisualStyleBackColor = true;
            this.btnOpenDialog.Click += new System.EventHandler(this.BtnOpenDialogClick);
            // 
            // btnForceGC
            // 
            this.btnForceGC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForceGC.Location = new System.Drawing.Point(12, 263);
            this.btnForceGC.Name = "btnForceGC";
            this.btnForceGC.Size = new System.Drawing.Size(120, 23);
            this.btnForceGC.TabIndex = 4;
            this.btnForceGC.Text = "Force GC";
            this.btnForceGC.UseVisualStyleBackColor = true;
            this.btnForceGC.Click += new System.EventHandler(this.BtnForceGcClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 298);
            this.Controls.Add(this.btnForceGC);
            this.Controls.Add(this.btnOpenDialog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "EventAggregator Demo Application";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPublisher1;
        private System.Windows.Forms.TextBox txtSubscriber1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPublisher2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubscriber2;
        private System.Windows.Forms.Button btnOpenDialog;
        private System.Windows.Forms.CheckBox checkSubscribe2;
        private System.Windows.Forms.CheckBox checkSubscribe1;
        private System.Windows.Forms.Button btnForceGC;
    }
}

