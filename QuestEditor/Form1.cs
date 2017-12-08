using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QuestEditor {
	public partial class Form1 : Form {
		private List<QuestEntry> Quests = new List<QuestEntry>();
		private QuestEntry currentEntry = new QuestEntry();
		private QuestLocationEntry currentLocation;

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			
		}

		private void resetLocationGB() {
			tbLocationDescription.Text="";
			tbLocationLabel.Text="";
			tbLocationName.Text="";
			cbLocationRequirements.Items.Clear();
			cbLocationRequirements.Items.Add("Add new requirement");
			cbLocationEvents.Items.Clear();
			cbLocationEvents.Items.Add("Add new event");
		}

		#region LocationsDropDown

		private void cbQuestLocations_SelectedIndexChanged(object sender, EventArgs e) {
			if(cbQuestLocations.Text =="Add new location") {
				cbQuestLocations.Items.Add("New Quest()");
				resetLocationGB();
				tbLocationName.Text="New Quest";
				currentLocation=new QuestLocationEntry();
				currentLocation.Name="New Quest";
				currentEntry.addLocation(currentLocation);
			} else {
				if (currentEntry.Locations!=null) {
					for (int i = 0; i<currentEntry.Locations.Length; i++) {
						if (convertToTitle(currentEntry.Locations[i].Label, currentEntry.Locations[i].Name)==cbQuestLocations.Text) {
							currentLocation=currentEntry.Locations[i];
							populateFields();
							break;
						}
					}
				}
			}
			gbLocation.Text="Location: "+cbQuestLocations.Text;
		}

		private void populateFields() {
			tbLocationDescription.Text=currentLocation.Description;
			tbLocationLabel.Text=currentLocation.Label;
			tbLocationName.Text=currentLocation.Name;
		}

		private string convertToTitle(string name, string label) {
			return name+"("+label+")";
		}

		private void tbLocationName_TextChanged(object sender, EventArgs e) {
			updateLocationDDText();
			currentLocation.Name=tbLocationName.Text;
		}
		private void tbLocationLabel_TextChanged(object sender, EventArgs e) {
			updateLocationDDText();
			currentLocation.Label=tbLocationLabel.Text;
		}
		private void updateLocationDDText() {
			cbLocationEvents.Items.Remove(convertToTitle(currentLocation.Name, currentLocation.Label));
			cbLocationEvents.Items.Add(convertToTitle(tbLocationName.Text, tbLocationLabel.Text));
			cbQuestLocations.Text=convertToTitle(tbLocationName.Text,tbLocationLabel.Text);
		}

		#endregion

		#region Requirement

		List<Control> RequirementControls = new List<Control>();
		private void cbRequirement_SelectedIndexChanged(object sender, EventArgs e) {
			foreach (Control c in RequirementControls) {
				c.Dispose();
			}
			if (cbRequirement.Text=="Location") {
				
				Label l1 = new Label() { Text="Location", Parent=flbRequirementLeft};
				TextBox t1 = new TextBox() { Parent=flbRequirementRight };
				RequirementControls.Add(l1);
				RequirementControls.Add(t1);
			}
		}

		#endregion

		private void tbLocationDescription_TextChanged(object sender, EventArgs e) {
			currentLocation.Description=tbLocationDescription.Text;
		}
	}
}
