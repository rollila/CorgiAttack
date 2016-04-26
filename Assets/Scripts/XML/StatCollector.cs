using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("StatCollection")]
public class StatCollector {

	[XmlArray("PlayerStats"), XmlArrayItem("Player")]
	public Player[] Players;

	//[XmlArray("TopTen"), XmlArrayItem("Ranking")]
	//public Ranking[] TopTen;

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
}
