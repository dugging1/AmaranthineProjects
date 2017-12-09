using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEditor {
	internal struct QuestEntry {
		public string Label { get; set; } //Shown name
		public QuestLocationEntry[] Locations { get; set; }

		public void addLocation(QuestLocationEntry e) {
			if (Locations!=null) {
				QuestLocationEntry[] L = Locations;
				Locations=new QuestLocationEntry[L.Length+1];
				for (int i = 0; i<L.Length; i++) {
					Locations[i]=L[i];
				}
				Locations[L.Length]=e;
			} else {
				Locations=new QuestLocationEntry[1] { e };
			}
		}

		public void removeLocation(int pos) {
			if (Locations==null) return;
			if (Locations.Length>1) {
				QuestLocationEntry[] Locs = new QuestLocationEntry[Locations.Length-1];
				bool passed = false;
				for (int i = 0; i<Locations.Length; i++) {
					if (i==pos) {
						passed=true;
						continue;
					} else {
						if (passed) Locs[i-1]=Locations[i];
						else Locs[i]=Locations[i];
					}
				}
				Locations=Locs;
			} else {
				Locations=null;
			}
		}

		public static QuestEntry DecodeJson(string json) {
			QuestEntry s = JsonConvert.DeserializeObject<QuestEntry>(json);

			for (int i = 0; i<s.Locations.Length; i++) {
				s.Locations[i].TrueChoices=new List<QuestChoicesEntry>();
				for (int p = 0; p<s.Locations[i].Choices.Count; p++) {
					s.Locations[i].TrueChoices[p]=JsonConvert.DeserializeObject<QuestChoicesEntry>(s.Locations[i].Choices[p].ToString());
				}
			}
			return s;
		}
	}

	internal struct QuestLocationEntry {
		public string Name { get; set; } //Internal name
		public string Label { get; set; } //Shown name
		public Dictionary<string,object>[] Requirements { get; set; }
		public string Description { get; set; }
		public List<QuestChoicesEntry> TrueChoices { get; set; }

		public List<JObject> Choices { get; set; }

		public void addRequirement(Dictionary<string, object> item) {
			if (Requirements!=null) {
				Dictionary<string, object>[] Requires = new Dictionary<string, object>[Requirements.Length+1];
				for (int i = 0; i<Requirements.Length; i++) {
					Requires[i]=Requirements[i];
				}
				Requires[Requires.Length-1]=item;
				Requirements=Requires;
			} else {
				Requirements=new Dictionary<string, object>[] { item };
			}
		}

		public void removeRequirement(int pos) {
			if (Requirements==null) return;
			if (Requirements.Length>1) {
				Dictionary<string, object>[] reqs = new Dictionary<string, object>[Requirements.Length-1];
				bool passed = false;
				for (int i = 0; i<Requirements.Length; i++) {
					if (i==pos) {
						passed=true;
						continue;
					} else {
						if (passed) reqs[i-1]=Requirements[i];
						else reqs[i]=Requirements[i];
					}
				}
				Requirements=reqs;
			} else {
				Requirements=null;
			}
		}
	}

	internal struct QuestChoicesEntry {
		public string Label { get; set; }
		public string Description { get; set; }
		public QuestChoicesEntry[] Choices { get; set; }
		public Dictionary<string, string>[] Requirements { get; set; }
		public Dictionary<string, string>[] Events { get; set; }
	}
}
