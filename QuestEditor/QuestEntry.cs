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
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; } //Internal name
		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; } //Shown name
		[JsonProperty(PropertyName = "requirements")]
		public Dictionary<string,object>[] Requirements { get; set; }
		[JsonProperty(PropertyName="description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName ="events")]
		public Dictionary<string, object>[] Events { get; set; }
		[JsonProperty(PropertyName ="choices")]
		public List<QuestChoicesEntry> TrueChoices { get; set; }

		[JsonIgnore()]
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

		public void addEvent(Dictionary<string, object> item) {
			if (Events!=null) {
				Dictionary<string, object>[] evs = new Dictionary<string, object>[Events.Length+1];
				for (int i = 0; i<Events.Length; i++) {
					evs[i]=Events[i];
				}
				evs[evs.Length-1]=item;
				Events=evs;
			} else {
				Events=new Dictionary<string, object>[] { item };
			}
		}
		public void removeEvent(int pos) {
			if (Events==null) return;
			if (Events.Length>1) {
				Dictionary<string, object>[] evs = new Dictionary<string, object>[Events.Length-1];
				bool passed = false;
				for (int i = 0; i<Events.Length; i++) {
					if (i==pos) {
						passed=true;
						continue;
					} else {
						if (passed) evs[i-1]=Requirements[i];
						else evs[i]=Requirements[i];
					}
				}
				Events=evs;
			} else {
				Events=null;
			}
		}
	}

	internal struct QuestChoicesEntry {
		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "choices")]
		public List<QuestChoicesEntry> Choices { get; set; }
		
		public Dictionary<string, object>[] Requirements { get; set; }
		public Dictionary<string, object>[] Events { get; set; }
	}
}
