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
		public string Label { get; set; }
		public QuestLocationEntry[] Locations { get; set; }

		public static QuestEntry DecodeJson(string json) {
			QuestEntry s = JsonConvert.DeserializeObject<QuestEntry>(json);

			for (int i = 0; i<s.Locations.Length; i++) {
				s.Locations[i].TrueChoices=new QuestChoicesEntry[s.Locations[i].Choices.Count];
				for (int p = 0; p<s.Locations[i].Choices.Count; p++) {
					s.Locations[i].TrueChoices[p]=JsonConvert.DeserializeObject<QuestChoicesEntry>(s.Locations[i].Choices[p].ToString());
				}
			}
			return s;
		}
	}

	internal struct QuestLocationEntry {
		public string Name { get; set; }
		public string Label { get; set; }
		public Dictionary<string,string>[] Requirements { get; set; }
		public string Description { get; set; }
		public QuestChoicesEntry[] TrueChoices { get; set; }

		public List<JObject> Choices { get; set; }
	}

	internal struct QuestChoicesEntry {
		public string Label { get; set; }
		public string Description { get; set; }
		public QuestChoicesEntry[] Choices { get; set; }
		public Dictionary<string, string>[] Requirements { get; set; }
		public Dictionary<string, string>[] Events { get; set; }
	}
}
