using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class Ranking {
	[XmlElement("rank")]
	public int rank;

	[XmlElement("name")]
	public string name;

	[XmlElement("score")]
	public int score;

	public Ranking() {}
	public Ranking(int Rank, string Name, int Score) {
		rank = Rank;
		name = Name;
		score = Score;
	}
}
