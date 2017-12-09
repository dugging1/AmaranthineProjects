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
		private int currentLoc;
		private QuestLocationEntry currentLocation { get { return currentEntry.Locations[currentLoc]; } set { currentEntry.addLocation(value); } }

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			gbLocationEvents.Hide();
			gbLocationRequirements.Hide();
			gbLocation.Hide();
		}

		#region RightPanelQuestEditor

		#region LocationsDropDown

		private void cbQuestLocations_SelectedIndexChanged(object sender, EventArgs e) {
			if (cbQuestLocations.Text=="Add new location") {
				tbLocationName.Text="New Quest";
				currentLocation=new QuestLocationEntry();
				currentLoc=currentEntry.Locations.Length-1;
				currentEntry.Locations[currentLoc].Name="New Quest";
				populateFields();
			} else {
				if (currentEntry.Locations!=null) {
					for (int i = 0; i<currentEntry.Locations.Length; i++) {
						if (convertToTitle(currentEntry.Locations[i].Name, currentEntry.Locations[i].Label)==cbQuestLocations.Text) {
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
		}

		private void populateFields() {
			if (currentEntry.Locations!=null) {
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
			if (currentEntry.Locations!=null) {
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
			if (currentEntry.Locations!=null) {
				currentEntry.Locations[currentLoc].Name=tbLocationName.Text;
			}
			updateLocationDDText(oldStr);
		}
		private void tbLocationLabel_TextChanged(object sender, EventArgs e) {
			string oldStr = getOldStr();
			if (currentEntry.Locations!=null) {
				currentEntry.Locations[currentLoc].Label=tbLocationLabel.Text;
			}
			updateLocationDDText(oldStr);
		}
		private void updateLocationDDText(string oldStr) {
			cbQuestLocations.Items.Remove(oldStr);
			cbQuestLocations.Items.Add(convertToTitle(tbLocationName.Text, tbLocationLabel.Text));
			cbQuestLocations.SelectedItem=cbQuestLocations.Items[cbQuestLocations.Items.IndexOf(convertToTitle(tbLocationName.Text, tbLocationLabel.Text))];
		}

		private void btnRemoveLocation_Click(object sender, EventArgs e) {
			cbQuestLocations.Items.Remove(getOldStr());
			currentEntry.removeLocation(currentLoc);
			currentLoc=0;
			if (currentEntry.Locations!=null) {
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
			if (cbLocationRequirements.Text == "Add new requirement") {
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
				currentEntry.Locations[currentLoc].addRequirement(new Dictionary<string, object>());
				cbLocationRequirements.SelectedItem=cbLocationRequirements.Items[cbLocationRequirements.Items.IndexOf(s)];
			} else {
				currentRequirement=cbLocationRequirements.Items.IndexOf(cbLocationRequirements.Text)-1;
				populateRequirement();
				gbLocationRequirements.Show();
			}
		}

		private void populateRequirement() {
			if (!currentLocation.Requirements[currentRequirement].ContainsKey("type")) return;
			string typeLower = currentLocation.Requirements[currentRequirement]["type"].ToString();
			string typeUpper;
			if (typeLower=="journal progress") {
				typeUpper="Journal Progress";
			} else {
				typeUpper = Char.ToUpper(typeLower[0]).ToString()+typeLower.Substring(1);
			}
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
			} catch (Exception) {}
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
			} catch (Exception) {}

			if (currentEntry.Locations[currentLoc].Requirements!=null) {
				currentEntry.Locations[currentLoc].Requirements[currentRequirement]=require;
			} else {
				currentEntry.Locations[currentLoc].addRequirement(require);
			}
		}

		private void btnLocationRemoveRequirement_Click(object sender, EventArgs e) {
			if (currentLocation.Requirements==null) return;
			currentEntry.Locations[currentLoc].removeRequirement(currentRequirement);
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

		private void tbLocationDescription_TextChanged(object sender, EventArgs e) {
			if (currentEntry.Locations!=null) {
				currentEntry.Locations[currentLoc].Description=tbLocationDescription.Text;
			}
		}

		#endregion
	}
}
