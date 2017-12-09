namespace QuestEditor {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing&&(components!=null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Quests");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.btnRemoveLocation = new System.Windows.Forms.Button();
			this.gbLocation = new System.Windows.Forms.GroupBox();
			this.btnLocationRemoveEvent = new System.Windows.Forms.Button();
			this.btnLocationRemoveRequirement = new System.Windows.Forms.Button();
			this.cbLocationEvents = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.gbLocationEvents = new System.Windows.Forms.GroupBox();
			this.gbLocationRequirements = new System.Windows.Forms.GroupBox();
			this.flbRequirementRight = new System.Windows.Forms.FlowLayoutPanel();
			this.flbRequirementLeft = new System.Windows.Forms.FlowLayoutPanel();
			this.cbRequirement = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.cbLocationRequirements = new System.Windows.Forms.ComboBox();
			this.tbLocationDescription = new System.Windows.Forms.TextBox();
			this.tbLocationLabel = new System.Windows.Forms.TextBox();
			this.tbLocationName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbQuestLocations = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbQuestLabel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.btnLoadData = new System.Windows.Forms.ToolStripMenuItem();
			this.btnSaveData = new System.Windows.Forms.ToolStripMenuItem();
			this.btnNewQueset = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.pRightPanel = new System.Windows.Forms.Panel();
			this.cblLocationChoices = new System.Windows.Forms.CheckedListBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.gbLocation.SuspendLayout();
			this.gbLocationRequirements.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.pRightPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pRightPanel);
			this.splitContainer1.Size = new System.Drawing.Size(1043, 731);
			this.splitContainer1.SplitterDistance = 232;
			this.splitContainer1.TabIndex = 0;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			treeNode8.Name = "TreeRoot";
			treeNode8.Text = "Quests";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
			this.treeView1.Size = new System.Drawing.Size(232, 731);
			this.treeView1.TabIndex = 0;
			// 
			// btnRemoveLocation
			// 
			this.btnRemoveLocation.Location = new System.Drawing.Point(523, 46);
			this.btnRemoveLocation.Name = "btnRemoveLocation";
			this.btnRemoveLocation.Size = new System.Drawing.Size(135, 23);
			this.btnRemoveLocation.TabIndex = 5;
			this.btnRemoveLocation.Text = "Remove Location";
			this.btnRemoveLocation.UseVisualStyleBackColor = true;
			this.btnRemoveLocation.Click += new System.EventHandler(this.btnRemoveLocation_Click);
			// 
			// gbLocation
			// 
			this.gbLocation.Controls.Add(this.cblLocationChoices);
			this.gbLocation.Controls.Add(this.btnLocationRemoveEvent);
			this.gbLocation.Controls.Add(this.btnLocationRemoveRequirement);
			this.gbLocation.Controls.Add(this.cbLocationEvents);
			this.gbLocation.Controls.Add(this.label8);
			this.gbLocation.Controls.Add(this.gbLocationEvents);
			this.gbLocation.Controls.Add(this.gbLocationRequirements);
			this.gbLocation.Controls.Add(this.cbLocationRequirements);
			this.gbLocation.Controls.Add(this.tbLocationDescription);
			this.gbLocation.Controls.Add(this.tbLocationLabel);
			this.gbLocation.Controls.Add(this.tbLocationName);
			this.gbLocation.Controls.Add(this.label7);
			this.gbLocation.Controls.Add(this.label6);
			this.gbLocation.Controls.Add(this.label5);
			this.gbLocation.Controls.Add(this.label4);
			this.gbLocation.Controls.Add(this.label3);
			this.gbLocation.Location = new System.Drawing.Point(10, 99);
			this.gbLocation.Name = "gbLocation";
			this.gbLocation.Size = new System.Drawing.Size(787, 616);
			this.gbLocation.TabIndex = 4;
			this.gbLocation.TabStop = false;
			this.gbLocation.Text = "Location";
			// 
			// btnLocationRemoveEvent
			// 
			this.btnLocationRemoveEvent.Location = new System.Drawing.Point(513, 328);
			this.btnLocationRemoveEvent.Name = "btnLocationRemoveEvent";
			this.btnLocationRemoveEvent.Size = new System.Drawing.Size(135, 23);
			this.btnLocationRemoveEvent.TabIndex = 13;
			this.btnLocationRemoveEvent.Text = "Remove Location";
			this.btnLocationRemoveEvent.UseVisualStyleBackColor = true;
			// 
			// btnLocationRemoveRequirement
			// 
			this.btnLocationRemoveRequirement.Location = new System.Drawing.Point(513, 291);
			this.btnLocationRemoveRequirement.Name = "btnLocationRemoveRequirement";
			this.btnLocationRemoveRequirement.Size = new System.Drawing.Size(135, 23);
			this.btnLocationRemoveRequirement.TabIndex = 6;
			this.btnLocationRemoveRequirement.Text = "Remove Requirement";
			this.btnLocationRemoveRequirement.UseVisualStyleBackColor = true;
			this.btnLocationRemoveRequirement.Click += new System.EventHandler(this.btnLocationRemoveRequirement_Click);
			// 
			// cbLocationEvents
			// 
			this.cbLocationEvents.FormattingEnabled = true;
			this.cbLocationEvents.Items.AddRange(new object[] {
            "Add new Event"});
			this.cbLocationEvents.Location = new System.Drawing.Point(146, 328);
			this.cbLocationEvents.Name = "cbLocationEvents";
			this.cbLocationEvents.Size = new System.Drawing.Size(361, 21);
			this.cbLocationEvents.TabIndex = 12;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(19, 328);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 13);
			this.label8.TabIndex = 11;
			this.label8.Text = "Events";
			// 
			// gbLocationEvents
			// 
			this.gbLocationEvents.Location = new System.Drawing.Point(6, 360);
			this.gbLocationEvents.Name = "gbLocationEvents";
			this.gbLocationEvents.Size = new System.Drawing.Size(772, 247);
			this.gbLocationEvents.TabIndex = 10;
			this.gbLocationEvents.TabStop = false;
			this.gbLocationEvents.Text = "Event";
			// 
			// gbLocationRequirements
			// 
			this.gbLocationRequirements.Controls.Add(this.flbRequirementRight);
			this.gbLocationRequirements.Controls.Add(this.flbRequirementLeft);
			this.gbLocationRequirements.Controls.Add(this.cbRequirement);
			this.gbLocationRequirements.Controls.Add(this.label9);
			this.gbLocationRequirements.Location = new System.Drawing.Point(513, 49);
			this.gbLocationRequirements.Name = "gbLocationRequirements";
			this.gbLocationRequirements.Size = new System.Drawing.Size(265, 236);
			this.gbLocationRequirements.TabIndex = 9;
			this.gbLocationRequirements.TabStop = false;
			this.gbLocationRequirements.Text = "Requirement";
			// 
			// flbRequirementRight
			// 
			this.flbRequirementRight.Location = new System.Drawing.Point(95, 47);
			this.flbRequirementRight.Name = "flbRequirementRight";
			this.flbRequirementRight.Size = new System.Drawing.Size(164, 183);
			this.flbRequirementRight.TabIndex = 3;
			// 
			// flbRequirementLeft
			// 
			this.flbRequirementLeft.Location = new System.Drawing.Point(6, 47);
			this.flbRequirementLeft.Name = "flbRequirementLeft";
			this.flbRequirementLeft.Size = new System.Drawing.Size(83, 183);
			this.flbRequirementLeft.TabIndex = 2;
			// 
			// cbRequirement
			// 
			this.cbRequirement.FormattingEnabled = true;
			this.cbRequirement.Items.AddRange(new object[] {
            "Location",
            "Flag",
            "Item",
            "Journal Progress",
            "Level"});
			this.cbRequirement.Location = new System.Drawing.Point(63, 20);
			this.cbRequirement.Name = "cbRequirement";
			this.cbRequirement.Size = new System.Drawing.Size(196, 21);
			this.cbRequirement.TabIndex = 1;
			this.cbRequirement.SelectedIndexChanged += new System.EventHandler(this.cbRequirement_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(31, 13);
			this.label9.TabIndex = 0;
			this.label9.Text = "Type";
			// 
			// cbLocationRequirements
			// 
			this.cbLocationRequirements.FormattingEnabled = true;
			this.cbLocationRequirements.Items.AddRange(new object[] {
            "Add new requirement"});
			this.cbLocationRequirements.Location = new System.Drawing.Point(146, 293);
			this.cbLocationRequirements.Name = "cbLocationRequirements";
			this.cbLocationRequirements.Size = new System.Drawing.Size(361, 21);
			this.cbLocationRequirements.TabIndex = 8;
			this.cbLocationRequirements.SelectedIndexChanged += new System.EventHandler(this.cbLocationRequirements_SelectedIndexChanged);
			// 
			// tbLocationDescription
			// 
			this.tbLocationDescription.Location = new System.Drawing.Point(146, 106);
			this.tbLocationDescription.Multiline = true;
			this.tbLocationDescription.Name = "tbLocationDescription";
			this.tbLocationDescription.Size = new System.Drawing.Size(361, 61);
			this.tbLocationDescription.TabIndex = 7;
			this.tbLocationDescription.TextChanged += new System.EventHandler(this.tbLocationDescription_TextChanged);
			// 
			// tbLocationLabel
			// 
			this.tbLocationLabel.Location = new System.Drawing.Point(146, 80);
			this.tbLocationLabel.Name = "tbLocationLabel";
			this.tbLocationLabel.Size = new System.Drawing.Size(361, 20);
			this.tbLocationLabel.TabIndex = 6;
			this.tbLocationLabel.TextChanged += new System.EventHandler(this.tbLocationLabel_TextChanged);
			// 
			// tbLocationName
			// 
			this.tbLocationName.Location = new System.Drawing.Point(146, 49);
			this.tbLocationName.Name = "tbLocationName";
			this.tbLocationName.Size = new System.Drawing.Size(361, 20);
			this.tbLocationName.TabIndex = 5;
			this.tbLocationName.TextChanged += new System.EventHandler(this.tbLocationName_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(16, 180);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(123, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Choices (Uses locations)";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 106);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Description";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 301);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Requirements";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Label (Displayed in game)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Name (Internal name)";
			// 
			// cbQuestLocations
			// 
			this.cbQuestLocations.FormattingEnabled = true;
			this.cbQuestLocations.Items.AddRange(new object[] {
            "Add new location"});
			this.cbQuestLocations.Location = new System.Drawing.Point(127, 46);
			this.cbQuestLocations.Name = "cbQuestLocations";
			this.cbQuestLocations.Size = new System.Drawing.Size(361, 21);
			this.cbQuestLocations.TabIndex = 3;
			this.cbQuestLocations.SelectedIndexChanged += new System.EventHandler(this.cbQuestLocations_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Location";
			// 
			// tbQuestLabel
			// 
			this.tbQuestLabel.Location = new System.Drawing.Point(127, 12);
			this.tbQuestLabel.Name = "tbQuestLabel";
			this.tbQuestLabel.Size = new System.Drawing.Size(361, 20);
			this.tbQuestLabel.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(26, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Quest Label";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.btnNewQueset});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(122, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadData,
            this.btnSaveData});
			this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
			this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(41, 22);
			this.toolStripSplitButton1.Text = "File";
			// 
			// btnLoadData
			// 
			this.btnLoadData.Name = "btnLoadData";
			this.btnLoadData.Size = new System.Drawing.Size(161, 22);
			this.btnLoadData.Text = "Load Quest Data";
			// 
			// btnSaveData
			// 
			this.btnSaveData.Name = "btnSaveData";
			this.btnSaveData.Size = new System.Drawing.Size(161, 22);
			this.btnSaveData.Text = "Save Quest Data";
			// 
			// btnNewQueset
			// 
			this.btnNewQueset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnNewQueset.Image = ((System.Drawing.Image)(resources.GetObject("btnNewQueset.Image")));
			this.btnNewQueset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNewQueset.Name = "btnNewQueset";
			this.btnNewQueset.Size = new System.Drawing.Size(69, 22);
			this.btnNewQueset.Text = "New Quest";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1043, 731);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(1043, 756);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// pRightPanel
			// 
			this.pRightPanel.Controls.Add(this.label1);
			this.pRightPanel.Controls.Add(this.btnRemoveLocation);
			this.pRightPanel.Controls.Add(this.tbQuestLabel);
			this.pRightPanel.Controls.Add(this.gbLocation);
			this.pRightPanel.Controls.Add(this.label2);
			this.pRightPanel.Controls.Add(this.cbQuestLocations);
			this.pRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pRightPanel.Location = new System.Drawing.Point(0, 0);
			this.pRightPanel.Name = "pRightPanel";
			this.pRightPanel.Size = new System.Drawing.Size(807, 731);
			this.pRightPanel.TabIndex = 1;
			// 
			// cblLocationChoices
			// 
			this.cblLocationChoices.FormattingEnabled = true;
			this.cblLocationChoices.Location = new System.Drawing.Point(146, 180);
			this.cblLocationChoices.Name = "cblLocationChoices";
			this.cblLocationChoices.ScrollAlwaysVisible = true;
			this.cblLocationChoices.Size = new System.Drawing.Size(361, 94);
			this.cblLocationChoices.TabIndex = 14;
			this.cblLocationChoices.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cblLocationChoices_ItemCheck);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1043, 756);
			this.Controls.Add(this.toolStripContainer1);
			this.DoubleBuffered = true;
			this.Name = "Form1";
			this.Text = "Amarantine Quest Editor";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.gbLocation.ResumeLayout(false);
			this.gbLocation.PerformLayout();
			this.gbLocationRequirements.ResumeLayout(false);
			this.gbLocationRequirements.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.pRightPanel.ResumeLayout(false);
			this.pRightPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnNewQueset;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ComboBox cbQuestLocations;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbQuestLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbLocation;
		private System.Windows.Forms.ComboBox cbLocationRequirements;
		private System.Windows.Forms.TextBox tbLocationDescription;
		private System.Windows.Forms.TextBox tbLocationLabel;
		private System.Windows.Forms.TextBox tbLocationName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox gbLocationRequirements;
		private System.Windows.Forms.ComboBox cbLocationEvents;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox gbLocationEvents;
		private System.Windows.Forms.Button btnRemoveLocation;
		private System.Windows.Forms.Button btnLocationRemoveEvent;
		private System.Windows.Forms.Button btnLocationRemoveRequirement;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		private System.Windows.Forms.ToolStripMenuItem btnLoadData;
		private System.Windows.Forms.ToolStripMenuItem btnSaveData;
		private System.Windows.Forms.ComboBox cbRequirement;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.FlowLayoutPanel flbRequirementRight;
		private System.Windows.Forms.FlowLayoutPanel flbRequirementLeft;
		private System.Windows.Forms.Panel pRightPanel;
		private System.Windows.Forms.CheckedListBox cblLocationChoices;
	}
}

