namespace FFXIVTeleGather
{
    partial class Form1
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.zoomHack = new System.Windows.Forms.Button();
            this.zonePositions = new System.Windows.Forms.ListBox();
            this.zoneList = new System.Windows.Forms.ComboBox();
            this.addWaypoint = new System.Windows.Forms.Button();
            this.waypointName = new System.Windows.Forms.TextBox();
            this.teleportToWaypoint = new System.Windows.Forms.Button();
            this.deleteWaypoint = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.worldBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.worldBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // zoomHack
            // 
            this.zoomHack.Location = new System.Drawing.Point(272, 226);
            this.zoomHack.Name = "zoomHack";
            this.zoomHack.Size = new System.Drawing.Size(75, 23);
            this.zoomHack.TabIndex = 1;
            this.zoomHack.Text = "Zoom Hack";
            this.zoomHack.UseVisualStyleBackColor = true;
            this.zoomHack.Visible = false;
            this.zoomHack.Click += new System.EventHandler(this.zoomHack_Click);
            // 
            // zonePositions
            // 
            this.zonePositions.DisplayMember = "info";
            this.zonePositions.FormattingEnabled = true;
            this.zonePositions.Location = new System.Drawing.Point(12, 32);
            this.zonePositions.Name = "zonePositions";
            this.zonePositions.Size = new System.Drawing.Size(121, 160);
            this.zonePositions.TabIndex = 3;
            // 
            // zoneList
            // 
            this.zoneList.DisplayMember = "zoneName";
            this.zoneList.FormattingEnabled = true;
            this.zoneList.Location = new System.Drawing.Point(12, 5);
            this.zoneList.Name = "zoneList";
            this.zoneList.Size = new System.Drawing.Size(121, 21);
            this.zoneList.TabIndex = 4;
            this.zoneList.SelectedIndexChanged += new System.EventHandler(this.zoneList_SelectedIndexChanged);
            // 
            // addWaypoint
            // 
            this.addWaypoint.Location = new System.Drawing.Point(84, 197);
            this.addWaypoint.Name = "addWaypoint";
            this.addWaypoint.Size = new System.Drawing.Size(49, 19);
            this.addWaypoint.TabIndex = 5;
            this.addWaypoint.Text = "Add Waypoint";
            this.addWaypoint.UseVisualStyleBackColor = true;
            this.addWaypoint.Click += new System.EventHandler(this.addWaypoint_Click);
            // 
            // waypointName
            // 
            this.waypointName.Location = new System.Drawing.Point(12, 197);
            this.waypointName.Name = "waypointName";
            this.waypointName.Size = new System.Drawing.Size(66, 20);
            this.waypointName.TabIndex = 6;
            this.waypointName.Text = "Waypoint Name";
            // 
            // teleportToWaypoint
            // 
            this.teleportToWaypoint.Location = new System.Drawing.Point(12, 222);
            this.teleportToWaypoint.Name = "teleportToWaypoint";
            this.teleportToWaypoint.Size = new System.Drawing.Size(66, 19);
            this.teleportToWaypoint.TabIndex = 7;
            this.teleportToWaypoint.Text = "Teleport!";
            this.teleportToWaypoint.UseVisualStyleBackColor = true;
            this.teleportToWaypoint.Click += new System.EventHandler(this.teleportToWaypoint_Click);
            // 
            // deleteWaypoint
            // 
            this.deleteWaypoint.Location = new System.Drawing.Point(84, 222);
            this.deleteWaypoint.Name = "deleteWaypoint";
            this.deleteWaypoint.Size = new System.Drawing.Size(49, 19);
            this.deleteWaypoint.TabIndex = 8;
            this.deleteWaypoint.Text = "Delete Waypoint";
            this.deleteWaypoint.UseVisualStyleBackColor = true;
            this.deleteWaypoint.Click += new System.EventHandler(this.deleteWaypoint_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(259, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // worldBindingSource
            // 
            this.worldBindingSource.DataSource = typeof(FFXIVTeleGather.World);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(259, 107);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 241);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(346, 276);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.deleteWaypoint);
            this.Controls.Add(this.teleportToWaypoint);
            this.Controls.Add(this.waypointName);
            this.Controls.Add(this.addWaypoint);
            this.Controls.Add(this.zoneList);
            this.Controls.Add(this.zonePositions);
            this.Controls.Add(this.zoomHack);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.worldBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button zoomHack;
        private System.Windows.Forms.ListBox zonePositions;
        private System.Windows.Forms.ComboBox zoneList;
        private System.Windows.Forms.Button addWaypoint;
        private System.Windows.Forms.TextBox waypointName;
        private System.Windows.Forms.Button teleportToWaypoint;
        private System.Windows.Forms.BindingSource worldBindingSource;
        private System.Windows.Forms.Button deleteWaypoint;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

