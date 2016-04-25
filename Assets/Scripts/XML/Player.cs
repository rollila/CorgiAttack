using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Player {
	[XmlAttribute("name")]
	public string playerName;

	[XmlElement("totalPoints")]
	public int totalPoints;

	[XmlElement("totalRounds")]
	public int totalRounds;

	[XmlElement("highScore")]
	public int highScore;
}
