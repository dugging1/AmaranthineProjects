﻿using System;
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
using System.IO;

namespace QuestEditor {
	public partial class Form1 : Form {
		private QuestEntry[] Quests;
		private int currentEnt = 0;
		private int currentLoc;
		private QuestLocationEntry currentLocation { get { return Quests[currentEnt].Locations[currentLoc]; } }

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			pRightPanel.Hide();
			gbLocationEvents.Hide();
			gbLocationRequirements.Hide();
			gbLocation.Hide();
			QuestTree.NodeMouseDoubleClick+=QuestTree_NodeMouseDoubleClick;
		}

		private void addQuest(QuestEntry item) {
			if (Quests!=null) {
				QuestEntry[] q = new QuestEntry[Quests.Length+1];
				for (int i = 0; i<Quests.Length; i++) {
					q[i]=Quests[i];
				}
				q[q.Length-1]=item;
				Quests=q;
			} else {
				Quests=new QuestEntry[] { item };
			}
		}
		private void removeQuest(int pos) {
			if (Quests==null) return;
			string name = "";
			if (Quests.Length>1) {
				QuestEntry[] q = new QuestEntry[Quests.Length-1];
				bool passed = false;
				for (int i = 0; i<Quests.Length; i++) {
					if (i==pos) {
						passed=true;
						name=Quests[i].Label;
						continue;
					} else {
						if (passed) q[i-1]=Quests[i];
						else q[i]=Quests[i];
					}
				}
				Quests=q;
			} else {
				name=Quests[0].Label;
				Quests=null;
				pRightPanel.Hide();
			}
			int t=-1;
			for (int i = 0; i<QuestTree.Nodes[0].Nodes.Count; i++) {
				if (QuestTree.Nodes[0].Nodes[i].Text==name) {
					t=i;
				}
			}
			if (t!=-1) QuestTree.Nodes[0].Nodes[t].Remove();
			currentEnt=0;
			currentLoc=0;
			clearRightPanel();
			pRightPanel.Hide();
		}


		#region RightPanelQuestEditor

		#region LocationsDropDown

		private void cbQuestLocations_SelectedIndexChanged(object sender, EventArgs e) {
			if (cbQuestLocations.Text=="Add new location") {
				clearRightPanel();
				tbLocationName.Text="New Quest";
				Quests[currentEnt].addLocation(new QuestLocationEntry());
				currentLoc=Quests[currentEnt].Locations.Length-1;
				Quests[currentEnt].Locations[currentLoc].Name="New Quest";
				populateFields();
			} else {
				if (Quests[currentEnt].Locations!=null) {
					for (int i = 0; i<Quests[currentEnt].Locations.Length; i++) {
						if (convertToTitle(Quests[currentEnt].Locations[i].Name, Quests[currentEnt].Locations[i].Label)==cbQuestLocations.Text) {
							currentLoc=i;
							populateFields();
							break;
						}
					}
				}
			}
			if (cbQuestLocations.Text=="()"||cbQuestLocations.Text=="()") {
				gbLocation.Hide();
			} else {
				gbLocation.Text="Location: "+cbQuestLocations.Text;
				gbLocation.Show();
			}
			populateChoicesBox();
		}

		private void populateFields() {
			if (Quests[currentEnt].Locations!=null) {
				tbLocationDescription.Text=currentLocation.Description;
				tbLocationLabel.Text=currentLocation.Label;
				tbLocationName.Text=currentLocation.Name;
			} else {
				tbLocationDescription.Text="";
				tbLocationLabel.Text="";
				tbLocationName.Text="";
				cbQuestLocations.Items.Remove("New Quest()");
			}
		}

		private string convertToTitle(string name, string label) {
			return name+"("+label+")";
		}

		private string getOldStr() {
			if (Quests[currentEnt].Locations!=null) {
				string n = currentLocation.Name;
				string l = currentLocation.Label;
				if (n==null) n="";
				if (l==null) l="";
				return convertToTitle(n, l);
			} else {
				return convertToTitle("", "");
			}
		}

		private void tbLocationName_TextChanged(object sender, EventArgs e) {
			string oldStr = getOldStr();
			if (Quests[currentEnt].Locations!=null) {
				Quests[currentEnt].Locations[currentLoc].Name=tbLocationName.Text;
			}
			updateLocationDDText(oldStr);
		}
		private void tbLocationLabel_TextChanged(object sender, EventArgs e) {
			string oldStr = getOldStr();
			if (Quests[currentEnt].Locations!=null) {
				Quests[currentEnt].Locations[currentLoc].Label=tbLocationLabel.Text;
			}
			updateLocationDDText(oldStr);
		}
		private void updateLocationDDText(string oldStr) {
			cbQuestLocations.Items.Remove(oldStr);
			if (Quests[currentEnt].Locations!=null) {
				cbQuestLocations.Items.Add(convertToTitle(Quests[currentEnt].Locations[currentLoc].Name, Quests[currentEnt].Locations[currentLoc].Label));
				cbQuestLocations.SelectedItem=cbQuestLocations.Items[cbQuestLocations.Items.IndexOf(convertToTitle(Quests[currentEnt].Locations[currentLoc].Name, Quests[currentEnt].Locations[currentLoc].Label))];
			} else {
				cbQuestLocations.Items.Add(convertToTitle(tbLocationName.Text, tbLocationLabel.Text));
				cbQuestLocations.SelectedItem=cbQuestLocations.Items[cbQuestLocations.Items.IndexOf(convertToTitle(tbLocationName.Text, tbLocationLabel.Text))];
			}
		}

		private void btnRemoveLocation_Click(object sender, EventArgs e) {
			cbQuestLocations.Items.Remove(getOldStr());
			Quests[currentEnt].removeLocation(currentLoc);
			currentLoc=0;
			if (Quests[currentEnt].Locations!=null) {
				cbQuestLocations.SelectedItem=cbQuestLocations.Items[cbQuestLocations.Items.IndexOf(getOldStr())];
				populateFields();
			} else {
				cbQuestLocations.SelectedItem=null;
				cbQuestLocations.Text="";
				populateFields();
			}
			cbQuestLocations.Items.Remove("()");
		}

		#endregion

		#region Requirement

		private void cbLocationRequirements_SelectedIndexChanged(object sender, EventArgs e) {
			saveRequirement();
			clearRequirement();
			currentRequirementType=null;
			cbRequirement.Text="";
			if (cbLocationRequirements.Text=="Add new requirement") {
				string s;
				if (currentLocation.Requirements!=null) {
					s="Requirement "+(currentLocation.Requirements.Length+1);
					cbLocationRequirements.Items.Add(s);
					currentRequirement=currentLocation.Requirements.Length-1;
				} else {
					s="Requirement 1";
					cbLocationRequirements.Items.Add(s);
					currentRequirement=0;
				}
				Quests[currentEnt].Locations[currentLoc].addRequirement(new Dictionary<string, object>());
				cbLocationRequirements.SelectedItem=cbLocationRequirements.Items[cbLocationRequirements.Items.IndexOf(s)];
			} else {
				currentRequirement=cbLocationRequirements.Items.IndexOf(cbLocationRequirements.Text)-1;
				gbLocationRequirements.Show();
				populateRequirement();
			}
		}

		private void populateRequirement() {
			if (Quests[currentEnt].Locations==null) return;
			if (Quests[currentEnt].Locations[currentLoc].Requirements==null) return;
			if (cbLocationRequirements.Items.Count-1!=Quests[currentEnt].Locations[currentLoc].Requirements.Length) {
				cbLocationRequirements.Items.Clear();
				cbLocationRequirements.Items.Add("Add new requirement");
				for (int i = 0; i<Quests[currentEnt].Locations[currentLoc].Requirements.Length; i++) {
					cbLocationRequirements.Items.Add("Requirement "+(i+1));
				}
			}
			if (!currentLocation.Requirements[currentRequirement].ContainsKey("type")) return;
			string typeLower = currentLocation.Requirements[currentRequirement]["type"].ToString();
			string typeUpper;
			if (typeLower=="journal progress") {
				typeUpper="Journal Progress";
			} else {
				typeUpper=Char.ToUpper(typeLower[0]).ToString()+typeLower.Substring(1);
			}
			cbRequirement.SelectedItem=null;
			cbRequirement.SelectedItem=cbRequirement.Items[cbRequirement.Items.IndexOf(typeUpper)];
			try {
				if (typeUpper=="Location") {
					RequirementControls["Location"].Text=currentLocation.Requirements[currentRequirement]["location"].ToString();
				} else if (typeUpper=="Flag") {
					RequirementControls["Flag"].Text=currentLocation.Requirements[currentRequirement]["flag"].ToString();
					RequirementControls["Owner"].Text=currentLocation.Requirements[currentRequirement]["owner"].ToString();
					RequirementControls["Comparison"].Text=compTrans.FirstOrDefault(x => x.Value.ToString()==currentLocation.Requirements[currentRequirement]["comparison"].ToString()).Key.ToString();
					RequirementControls["Value"].Text=currentLocation.Requirements[currentRequirement]["value"].ToString();
				} else if (typeUpper=="Item") {
					RequirementControls["Item"].Text=currentLocation.Requirements[currentRequirement]["item"].ToString();
					RequirementControls["Comparison"].Text=compTrans.FirstOrDefault(x => x.Value.ToString()==currentLocation.Requirements[currentRequirement]["comparison"].ToString()).Key.ToString();
					RequirementControls["Count"].Text=currentLocation.Requirements[currentRequirement]["count"].ToString();
				} else if (typeUpper=="Journal Progress") {
					RequirementControls["Name"].Text=currentLocation.Requirements[currentRequirement]["name"].ToString();
					RequirementControls["Owner"].Text=currentLocation.Requirements[currentRequirement]["owner"].ToString();
					RequirementControls["Comparison"].Text=compTrans.FirstOrDefault(x => x.Value.ToString()==currentLocation.Requirements[currentRequirement]["comparison"].ToString()).Key.ToString();
					RequirementControls["Count"].Text=currentLocation.Requirements[currentRequirement]["count"].ToString();
				} else if (typeUpper=="Level") {
					RequirementControls["Skill"].Text=currentLocation.Requirements[currentRequirement]["skill"].ToString();
					RequirementControls["Comparison"].Text=compTrans.FirstOrDefault(x => x.Value.ToString()==currentLocation.Requirements[currentRequirement]["comparison"].ToString()).Key.ToString();
					RequirementControls["Level"].Text=currentLocation.Requirements[currentRequirement]["level"].ToString();
				}
			} catch (Exception) { }
		}

		private void clearRequirement() {
			foreach (Control c in RequirementControls.Values) {
				c.Dispose();
			}
			currentRequirementType=null;
		}

		Dictionary<string, Control> RequirementControls = new Dictionary<string, Control>();
		string currentRequirementType;
		int currentRequirement;
		private void cbRequirement_SelectedIndexChanged(object sender, EventArgs e) {
			clearRequirement();
			currentRequirementType=cbRequirement.Text;
			RequirementControls=new Dictionary<string, Control>();
			if (cbRequirement.Text=="Location") {
				Label l1 = new Label() { Text="Location", Parent=flbRequirementLeft };
				TextBox t1 = new TextBox() { Parent=flbRequirementRight };

				RequirementControls.Add("LocationL", l1);
				RequirementControls.Add("Location", t1);
			} else if (cbRequirement.Text=="Flag") {
				Label l1 = new Label() { Text="Flag", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t1 = new TextBox() { Parent=flbRequirementRight };

				Label l2 = new Label() { Text="Owner", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				ComboBox t2 = new ComboBox() { Parent=flbRequirementRight };
				t2.Items.Add("Player");
				t2.Items.Add("Guild");

				Label l3 = new Label() { Text="Comparison", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				ComboBox t3 = new ComboBox() { Parent=flbRequirementRight };
				t3.Items.AddRange(new string[] { "Equal To", "Not Equal To", "Greater Than", "Greater Or Equal To", "Less Than", "Less or Equal To" });

				Label l4 = new Label() { Text="Value", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t4 = new TextBox() { Parent=flbRequirementRight };

				RequirementControls.Add("Flag", t1);
				RequirementControls.Add("Owner", t2);
				RequirementControls.Add("Comparison", t3);
				RequirementControls.Add("Value", t4);

				RequirementControls.Add("FlagL", l1);
				RequirementControls.Add("OwnerL", l2);
				RequirementControls.Add("ComparisonL", l3);
				RequirementControls.Add("ValueL", l4);
			} else if (cbRequirement.Text=="Item") {
				Label l1 = new Label() { Text="Item", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t1 = new TextBox() { Parent=flbRequirementRight };

				Label l2 = new Label() { Text="Comparison", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				ComboBox t2 = new ComboBox() { Parent=flbRequirementRight };
				t2.Items.AddRange(new string[] { "Equal To", "Not Equal To", "Greater Than", "Greater Or Equal To", "Less Than", "Less or Equal To" });

				Label l3 = new Label() { Text="Count", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t3 = new TextBox() { Parent=flbRequirementRight };

				RequirementControls.Add("Item", t1);
				RequirementControls.Add("Comparison", t2);
				RequirementControls.Add("Count", t3);

				RequirementControls.Add("ItemL", l1);
				RequirementControls.Add("ComparisonL", l2);
				RequirementControls.Add("CountL", l3);
			} else if (cbRequirement.Text=="Journal Progress") {
				Label l1 = new Label() { Text="Name", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t1 = new TextBox() { Parent=flbRequirementRight };

				Label l2 = new Label() { Text="Owner", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				ComboBox t2 = new ComboBox() { Parent=flbRequirementRight };
				t2.Items.Add("Player");
				t2.Items.Add("Guild");

				Label l3 = new Label() { Text="Comparison", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				ComboBox t3 = new ComboBox() { Parent=flbRequirementRight };
				t3.Items.AddRange(new string[] { "Equal To", "Not Equal To", "Greater Than", "Greater Or Equal To", "Less Than", "Less or Equal To" });

				Label l4 = new Label() { Text="Count", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t4 = new TextBox() { Parent=flbRequirementRight };

				RequirementControls.Add("Name", t1);
				RequirementControls.Add("Owner", t2);
				RequirementControls.Add("Comparison", t3);
				RequirementControls.Add("Count", t4);

				RequirementControls.Add("NameL", l1);
				RequirementControls.Add("OwnerL", l2);
				RequirementControls.Add("ComparisonL", l3);
				RequirementControls.Add("CountL", l4);
			} else if (cbRequirement.Text=="Level") {
				Label l1 = new Label() { Text="Skill", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t1 = new TextBox() { Parent=flbRequirementRight };

				Label l2 = new Label() { Text="Comparison", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				ComboBox t2 = new ComboBox() { Parent=flbRequirementRight };
				t2.Items.AddRange(new string[] { "Equal To", "Not Equal To", "Greater Than", "Greater Or Equal To", "Less Than", "Less or Equal To" });

				Label l3 = new Label() { Text="Level", Parent=flbRequirementLeft, Size=new Size(200, 30) };
				TextBox t3 = new TextBox() { Parent=flbRequirementRight };

				RequirementControls.Add("Skill", t1);
				RequirementControls.Add("Comparison", t2);
				RequirementControls.Add("Level", t3);

				RequirementControls.Add("SkillL", l1);
				RequirementControls.Add("ComparisonL", l2);
				RequirementControls.Add("LevelL", l3);
			}
		}

		private readonly Dictionary<string, string> compTrans = new Dictionary<string, string>() { { "Equal To", "=" }, { "Not Equal To", "<>" }, { "Greater Than", ">" }, { "Greater Or Equal To", ">=" }, { "Less Than", "<" }, { "Less or Equal To", "<=" } };
		private void saveRequirement() {
			if (currentRequirementType==null) return;
			Dictionary<string, object> require = new Dictionary<string, object>();
			try {
				if (currentRequirementType=="Location") {
					require.Add("type", "location");
					require.Add("location", ((TextBox)RequirementControls["Location"]).Text);
				} else if (currentRequirementType=="Flag") {
					require.Add("type", "flag");
					require.Add("flag", ((TextBox)RequirementControls["Flag"]).Text);
					require.Add("owner", ((ComboBox)RequirementControls["Owner"]).SelectedItem.ToString());
					require.Add("comparison", compTrans[((ComboBox)RequirementControls["Comparison"]).SelectedItem.ToString()]);
					if (((TextBox)RequirementControls["Value"]).Text=="") {
						require.Add("value", null);
					} else {
						require.Add("value", Convert.ToInt32(((TextBox)RequirementControls["Value"]).Text));
					}
				} else if (currentRequirementType=="Item") {
					require.Add("type", "item");
					require.Add("item", ((TextBox)RequirementControls["Count"]).Text);
					require.Add("comparison", compTrans[((ComboBox)RequirementControls["Comparison"]).SelectedItem.ToString()]);
					require.Add("count", Convert.ToInt32(((TextBox)RequirementControls["Count"]).Text));
				} else if (currentRequirementType=="Journal Progress") {
					require.Add("type", "journal progress");
					require.Add("owner", ((ComboBox)RequirementControls["Owner"]).SelectedItem.ToString());
					require.Add("comparison", compTrans[((ComboBox)RequirementControls["Comparison"]).SelectedItem.ToString()]);
					require.Add("count", Convert.ToInt32(((TextBox)RequirementControls["Count"]).Text));
				} else if (currentRequirementType=="Level") {
					require.Add("type", "level");
					require.Add("skill", ((TextBox)RequirementControls["Skill"]).Text);
					require.Add("comparison", compTrans[((ComboBox)RequirementControls["Comparison"]).SelectedItem.ToString()]);
					require.Add("level", Convert.ToInt32(((TextBox)RequirementControls["Level"]).Text));
				}
			} catch (Exception) { }

			if (Quests[currentEnt].Locations[currentLoc].Requirements!=null) {
				Quests[currentEnt].Locations[currentLoc].Requirements[currentRequirement]=require;
			} else {
				Quests[currentEnt].Locations[currentLoc].addRequirement(require);
			}
		}

		private void btnLocationRemoveRequirement_Click(object sender, EventArgs e) {
			if (currentLocation.Requirements==null) return;
			Quests[currentEnt].Locations[currentLoc].removeRequirement(currentRequirement);
			cbLocationRequirements.Items.Remove("Requirement "+(currentRequirement+1));
			clearRequirement();
			currentRequirement=0;
			if (currentLocation.Requirements!=null) {
				cbLocationRequirements.SelectedItem=cbLocationRequirements.Items[1];
				currentRequirement=cbLocationRequirements.Items.IndexOf(cbLocationRequirements.SelectedItem)-1;
			}
			if (currentLocation.Requirements==null) gbLocationRequirements.Hide();
		}

		#endregion

		#region Choices

		private void cblLocationChoices_ItemCheck(object sender, ItemCheckEventArgs e) {
			for (int j = 0; j<cblLocationChoices.Items.Count; j++) {
				QuestLocationEntry q = new QuestLocationEntry();
				string name = cblLocationChoices.Items[j].ToString();
				for (int i = 0; i<Quests[currentEnt].Locations.Length; i++) {
					if (convertToTitle(Quests[currentEnt].Locations[i].Name, Quests[currentEnt].Locations[i].Label)==name) {
						q=Quests[currentEnt].Locations[i];
						break;
					}
				}
				QuestChoicesEntry choicesEntry = new QuestChoicesEntry() {
					Description=q.Description,
					Label=q.Label,
					Requirements=q.Requirements,
					Choices=q.TrueChoices,
					Events=q.Events
				};
				bool isChecked = cblLocationChoices.GetItemChecked(cblLocationChoices.Items.IndexOf(name));
				if (Quests[currentEnt].Locations[currentLoc].TrueChoices==null) Quests[currentEnt].Locations[currentLoc].TrueChoices=new List<QuestChoicesEntry>();
				if (!isChecked&&!Quests[currentEnt].Locations[currentLoc].TrueChoices.Contains(choicesEntry)) Quests[currentEnt].Locations[currentLoc].TrueChoices.Add(choicesEntry);
				else if (isChecked&&Quests[currentEnt].Locations[currentLoc].TrueChoices.Contains(choicesEntry)) Quests[currentEnt].Locations[currentLoc].TrueChoices.Remove(choicesEntry);
			}
		}

		private void populateChoicesBox() {
			cblLocationChoices.Items.Clear();
			if (Quests[currentEnt].Locations==null) return;
			for (int i = 0; i<Quests[currentEnt].Locations.Length; i++) {
				if (i==currentLoc) continue;
				string name = convertToTitle(Quests[currentEnt].Locations[i].Name, Quests[currentEnt].Locations[i].Label);
				cblLocationChoices.Items.Add(name);
				if (Quests[currentEnt].Locations[currentLoc].TrueChoices==null) continue;
				for (int j = 0; j<Quests[currentEnt].Locations[currentLoc].TrueChoices.Count; j++) {
					if (Quests[currentEnt].Locations[i].Label==Quests[currentEnt].Locations[currentLoc].TrueChoices[j].Label) {
						cblLocationChoices.SetItemChecked(cblLocationChoices.Items.IndexOf(name), true);
					}
				}
			}
		}

		#endregion

		#region Event

		Dictionary<string, Control> EventControls = new Dictionary<string, Control>();
		string currentEventType;
		int currentEvent;
		private void cbLocationEvents_SelectedIndexChanged(object sender, EventArgs e) {
			saveEvent();
			clearEvent();
			currentEventType=null;
			cbEvent.Text="";
			cbEvent.SelectedItem=null;
			if (cbLocationEvents.Text=="Add new event") {
				string s;
				if (currentLocation.Events!=null) {
					s="Event "+(currentLocation.Events.Length+1);
					currentEvent=currentLocation.Events.Length;
				} else {
					s="Event 1";
					currentEvent=0;
				}
				cbLocationEvents.Items.Add(s);
				Quests[currentEnt].Locations[currentLoc].addEvent(new Dictionary<string, object>());
				cbLocationEvents.SelectedItem=cbLocationEvents.Items[cbLocationEvents.Items.IndexOf(s)];
			} else {
				currentEvent=cbLocationEvents.Items.IndexOf(cbLocationEvents.Text)-1;
				populateEvent();
				gbLocationEvents.Show();
			}
		}

		readonly string[] AJItem = new string[] { "name", "description", "item", "total", "ordering", "owner", "name", "label" };
		private void saveEvent() {
			if (currentEventType==null) return;
			Dictionary<string, object> ev = new Dictionary<string, object>();
			try {
				switch (currentEventType) {
					case ("Add Item"):
						ev["type"]="add item";
						ev["item"]=((TextBox)EventControls["item"]).Text;
						ev["count"]=((TextBox)EventControls["count"]).Text;
						break;
					case ("Remove Item"):
						ev["type"]="remove item";
						ev["item"]=((TextBox)EventControls["item"]).Text;
						ev["count"]=((TextBox)EventControls["count"]).Text;
						break;
					case ("Jump"):
						ev["type"]="jump";
						ev["label"]=((TextBox)EventControls["label"]).Text;
						break;
					case ("Add Journal"):
						ev["type"]="add journal";
						for (int i = 0; i<AJItem.Length; i++) {
							ev[AJItem[i]]=((TextBox)EventControls[AJItem[i]]).Text;
						}
						ev["progress type"]=((ComboBox)EventControls["progress type"]).SelectedIndex;
						break;
					case ("Remove Journal"):
						ev["type"]="remove journal";
						ev["owner"]=((TextBox)EventControls["owner"]).Text;
						ev["name"]=((TextBox)EventControls["name"]).Text;
						break;
					case ("Add Experience"):
						ev["type"]="add experience";
						ev["skill"]=((TextBox)EventControls["skill"]).Text;
						ev["count"]=((TextBox)EventControls["count"]).Text;
						break;
					case ("Set Flag"):
						ev["type"]="set flag";
						ev["owner"]=((TextBox)EventControls["owner"]).Text;
						ev["flag"]=((TextBox)EventControls["flag"]).Text;
						ev["value"]=((TextBox)EventControls["value"]).Text;
						break;
					case ("Clear Entry"):
						ev["type"]="clear entry";
						break;
				}
			} catch (KeyNotFoundException) { }

			if (Quests[currentEnt].Locations[currentLoc].Events!=null) {
				Quests[currentEnt].Locations[currentLoc].Events[currentEvent]=ev;
			} else {
				Quests[currentEnt].Locations[currentLoc].addEvent(ev);
			}
		}

		private void clearEvent() {
			foreach (Control c in EventControls.Values) {
				c.Dispose();
			}
			currentEventType=null;
		}

		private void populateEvent() {
			if (Quests[currentEnt].Locations==null) return;
			if (currentLocation.Events==null) return;
			if (cbLocationEvents.Items.Count-1!=Quests[currentEnt].Locations[currentLoc].Events.Length) {
				cbLocationEvents.Items.Clear();
				cbLocationEvents.Items.Add("Add new event");
				for (int i = 0; i<Quests[currentEnt].Locations[currentLoc].Events.Length; i++) {
					cbLocationEvents.Items.Add("Event "+(i+1));
				}
			}
			if (!currentLocation.Events[currentEvent].ContainsKey("type")) return;
			string typeLower = currentLocation.Events[currentEvent]["type"].ToString();
			string typeUpper = "";
			cbEvent.SelectedItem=null;
			try {
				switch (typeLower) {
					case ("add item"):
						typeUpper="Add Item";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						EventControls["count"].Text=currentLocation.Events[currentEvent]["count"].ToString();
						EventControls["item"].Text=currentLocation.Events[currentEvent]["item"].ToString();
						break;
					case ("remove item"):
						typeUpper="Remove Item";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						EventControls["item"].Text=currentLocation.Events[currentEvent]["item"].ToString();
						EventControls["count"].Text=currentLocation.Events[currentEvent]["count"].ToString();
						break;
					case ("jump"):
						typeUpper="Jump";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						EventControls["label"].Text=currentLocation.Events[currentEvent]["label"].ToString();
						break;
					case ("add journal"):
						typeUpper="Add Journal";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						for (int i = 0; i<AJItem.Length; i++) {
							EventControls[AJItem[i]].Text=currentLocation.Events[currentEvent][AJItem[i]].ToString();
						}
							((ComboBox)EventControls["progress type"]).SelectedItem=((ComboBox)EventControls["progress type"]).Items[((ComboBox)EventControls["progress type"]).Items.IndexOf(currentLocation.Events[currentEvent]["progress type"].ToString())];
						((ComboBox)EventControls["owner"]).SelectedItem=((ComboBox)EventControls["owner"]).Items[((ComboBox)EventControls["owner"]).Items.IndexOf(currentLocation.Events[currentEvent]["owner"].ToString())];
						break;
					case ("remove journal"):
						typeUpper="Remove Journal";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						EventControls["name"].Text=currentLocation.Events[currentEvent]["name"].ToString();
						((ComboBox)EventControls["owner"]).SelectedItem=((ComboBox)EventControls["owner"]).Items[((ComboBox)EventControls["owner"]).Items.IndexOf(currentLocation.Events[currentEvent]["owner"].ToString())];
						break;
					case ("add experience"):
						typeUpper="Add Experience";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						EventControls["skill"].Text=currentLocation.Events[currentEvent]["skill"].ToString();
						EventControls["count"].Text=currentLocation.Events[currentEvent]["count"].ToString();
						break;
					case ("set flag"):
						typeUpper="Set Flag";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						((ComboBox)EventControls["owner"]).SelectedItem=((ComboBox)EventControls["owner"]).Items[((ComboBox)EventControls["owner"]).Items.IndexOf(currentLocation.Events[currentEvent]["owner"].ToString())];
						EventControls["flag"].Text=currentLocation.Events[currentEvent]["flag"].ToString();
						EventControls["value"].Text=currentLocation.Events[currentEvent]["value"].ToString();
						break;
					case ("clear entry"):
						typeUpper="Clear Entry";
						cbEvent.SelectedItem=cbEvent.Items[cbEvent.Items.IndexOf(typeUpper)];
						break;
				}
			} catch (Exception) { }
		}

		const int labelHeight = 27;
		private void cbEvent_SelectedIndexChanged(object sender, EventArgs e) {
			clearEvent();
			currentEventType=cbEvent.Text;
			EventControls=new Dictionary<string, Control>();
			switch (currentEventType) {
				case ("Add Item"):
				case ("Remove Item"):
					EventControls.Add("item", new TextBox() { Parent=flbEventRight });
					EventControls.Add("itemL", new Label() { Parent=flbEventLeft, Text="Item", Size=new Size(100, labelHeight) });
					EventControls.Add("count", new TextBox() { Parent=flbEventRight });
					EventControls.Add("countL", new Label() { Parent=flbEventLeft, Text="Count", Size=new Size(100, labelHeight) });
					break;
				case ("Jump"):
					EventControls.Add("label", new TextBox() { Parent=flbEventRight });
					EventControls.Add("labelL", new Label() { Parent=flbEventLeft, Text="Label", Size=new Size(100, labelHeight) });
					break;
				case ("Add Journal"):
					EventControls.Add("description", new TextBox() { Parent=flbEventRight });
					EventControls.Add("descriptionL", new Label() { Parent=flbEventLeft, Text="Description", Size=new Size(100, labelHeight) });
					EventControls.Add("item", new TextBox() { Parent=flbEventRight });
					EventControls.Add("itemL", new Label() { Parent=flbEventLeft, Text="Item/Catagory", Size=new Size(100, labelHeight) });
					EventControls.Add("total", new TextBox() { Parent=flbEventRight });
					EventControls.Add("totalL", new Label() { Parent=flbEventLeft, Text="Total", Size=new Size(100, labelHeight) });
					EventControls.Add("ordering", new TextBox() { Parent=flbEventRight });
					EventControls.Add("orderingL", new Label() { Parent=flbEventLeft, Text="Ordering", Size=new Size(100, labelHeight) });
					ComboBox t3 = new ComboBox() { Parent=flbEventRight };
					t3.Items.AddRange(new string[] { "Item", "Item Catagory" });
					EventControls.Add("progress type", t3);
					EventControls.Add("progress typeL", new Label() { Parent=flbEventLeft, Text="Progress Type", Size=new Size(100, labelHeight) });
					goto case "Remove Journal";
				case ("Remove Journal"):
					EventControls.Add("name", new TextBox() { Parent=flbEventRight });
					EventControls.Add("nameL", new Label() { Parent=flbEventLeft, Text="Name", Size=new Size(100, labelHeight) });
					ComboBox t1 = new ComboBox() { Parent=flbEventRight };
					t1.Items.AddRange(new string[] { "Player", "Guild" });
					EventControls.Add("owner", t1);
					EventControls.Add("ownerL", new Label() { Parent=flbEventLeft, Text="Owner", Size=new Size(100, labelHeight) });
					break;
				case ("Add Experience"):
					EventControls.Add("skill", new TextBox() { Parent=flbEventRight });
					EventControls.Add("skillL", new Label() { Parent=flbEventLeft, Text="Skill", Size=new Size(100, labelHeight) });
					EventControls.Add("count", new TextBox() { Parent=flbEventRight });
					EventControls.Add("countL", new Label() { Parent=flbEventLeft, Text="Count", Size=new Size(100, labelHeight) });
					break;
				case ("Set Flag"):
					EventControls.Add("flag", new TextBox() { Parent=flbEventRight });
					EventControls.Add("flagL", new Label() { Parent=flbEventLeft, Text="Flag", Size=new Size(100, labelHeight) });
					EventControls.Add("value", new TextBox() { Parent=flbEventRight });
					EventControls.Add("valueL", new Label() { Parent=flbEventLeft, Text="Value", Size=new Size(100, labelHeight) });
					ComboBox t2 = new ComboBox() { Parent=flbEventRight };
					t2.Items.AddRange(new string[] { "Player", "Guild" });
					EventControls.Add("owner", t2);
					EventControls.Add("ownerL", new Label() { Parent=flbEventLeft, Text="Owner", Size=new Size(100, labelHeight) });
					break;
				case ("Clear Entry"):
					break;
			}
		}

		#endregion

		private void tbLocationDescription_TextChanged(object sender, EventArgs e) {
			if (Quests[currentEnt].Locations!=null) {
				Quests[currentEnt].Locations[currentLoc].Description=tbLocationDescription.Text;
			}
		}

		string oldQuestName;
		private void tbQuestLabel_TextChanged(object sender, EventArgs e) {
			if (oldQuestName==null) oldQuestName="New Quest 1";
			foreach (TreeNode t in QuestTree.Nodes[0].Nodes) {
				if (t.Text==oldQuestName) {
					t.Text=tbQuestLabel.Text;
				}
			}
			Quests[currentEnt].Label=tbQuestLabel.Text;
			oldQuestName=tbQuestLabel.Text;
		}

		#endregion


		#region LeftPanelQuestEditor

		private void QuestTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			string name = e.Node.ToString().Substring(10);
			for (int i = 0; i<Quests.Length; i++) {
				if (Quests[i].Label==name) {
					currentEnt=i;
					oldQuestName=Quests[currentEnt].Label;
					pRightPanel.Show();
					break;
				}
			}
			clearRightPanel();
			populateRightPanel();
		}

		private void populateRightPanel() {
			tbQuestLabel.Text=Quests[currentEnt].Label;
			populateFields();
			populateChoicesBox();
			if (Quests[currentEnt].Locations!=null&&Quests[currentEnt].Locations[currentLoc].Events!=null) gbLocationEvents.Show();
			populateEvent();
			if (Quests[currentEnt].Locations!=null&&Quests[currentEnt].Locations[currentLoc].Requirements!=null) gbLocationRequirements.Show();
			populateRequirement();
			if (cbLocationEvents.SelectedItem==null) gbLocationEvents.Hide();
			if (cbLocationRequirements.SelectedItem==null) gbLocationRequirements.Hide();
		}

		private void clearRightPanel() {
			cbQuestLocations.Items.Clear();
			cbQuestLocations.Items.Add("Add new location");
			clearEvent();
			clearRequirement();
			if (Quests==null) {
				gbLocation.Hide();
				return;
			}
			if (Quests[currentEnt].Locations==null) gbLocation.Hide();
			else {
				if (Quests[currentEnt].Locations[currentLoc].Requirements==null) gbLocationRequirements.Hide();
				if (Quests[currentEnt].Locations[currentLoc].Events==null) gbLocationEvents.Hide();
			}
		}

		private void saveRightPanel() {
			Quests[currentEnt].Label=tbQuestLabel.Text;
			saveRequirement();
			saveEvent();
		}

		#endregion

		#region ToolBar

		private void btnNewQueset_Click(object sender, EventArgs E) {
			clearRightPanel();
			QuestEntry e;
			if (Quests!=null) {
				e=new QuestEntry() { Label="New Quest "+(Quests.Length+1) };
			} else {
				e=new QuestEntry() { Label="New Quest 1" };
			}
			addQuest(e);
			QuestTree.Nodes[0].Nodes.Add(e.Label);
		}

		private void btnLoadData_Click(object sender, EventArgs e) {
			OpenFileDialog f = new OpenFileDialog() {
				DefaultExt="json",
				AddExtension=true
			};
			if (f.ShowDialog()==DialogResult.OK) {
				Quests=null;
				TextReader t = new StreamReader(new FileStream(f.FileName, FileMode.Open));
				string json = t.ReadToEnd();
				Quests=JsonConvert.DeserializeObject<QuestEntry[]>(json);
				t.Dispose();
				QuestTree.Nodes.Clear();
				QuestTree.Nodes.Add("Quest");
				foreach (QuestEntry q in Quests) {
					QuestTree.Nodes[0].Nodes.Add(q.Label);
				}
			}
		}

		private void btnSaveData_Click(object sender, EventArgs e) {
			saveRightPanel();
			SaveFileDialog f = new SaveFileDialog() {
				DefaultExt="json",
				AddExtension=true
			};
			if (f.ShowDialog()==DialogResult.OK) {
				string json = JsonConvert.SerializeObject(Quests);
				TextWriter t = new StreamWriter(new FileStream(f.FileName, FileMode.Create));
				t.Write(json);
				t.Dispose();
			}
		}

		private void btnRemoveQuest_Click(object sender, EventArgs e) {
			removeQuest(currentEnt);
		}

		#endregion
	}
}
