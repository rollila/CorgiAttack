using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class Ranking {
	[XmlElement("name")]
	public string name;

	[XmlElement("score")]
	public int score;

	public Ranking() {}
	public Ranking(string Name, int Score) {
		name = Name;
		score = Score;
	}
}
