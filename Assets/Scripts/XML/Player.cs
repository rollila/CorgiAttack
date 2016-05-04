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

	[XmlElement("lessDoge")]
	public bool lessDoge;

	[XmlElement("moreDoge")]
	public bool moreDoge;

	[XmlElement("musicVol")]
	public int musicVol;
}
