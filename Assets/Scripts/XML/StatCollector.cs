using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections;

[XmlRoot("StatCollection")]
public class StatCollector {

	[XmlArray("PlayerStats"), XmlArrayItem("Player")]
	public Player[] Players;

	[XmlArray("TopTen"), XmlArrayItem("Ranking")]
	public Ranking[] TopTen;

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(StatCollector));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}

	public static StatCollector Load(string path)
	{
		var serializer = new XmlSerializer(typeof(StatCollector));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as StatCollector;
		}
			
	}

	public void SetTopTen(List<Ranking> TopTen2) {
		Ranking[] TT = new Ranking[10];
		for (int i = 0; i < TopTen2.Count; i++) {
			TT [i] = TopTen2 [i];
		}

		TopTen = TT;
	}

	public void SetPlayers(Player[] p) {
		Players = p;
	}

	public void SetPlayer(Player p) {
		Players[0] = p;
	}
}
