using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections;

public class StatHandler : MonoBehaviour {
	private string playerStatsXMLfilepath = "Scripts/XML/playerStatStorage.xml";

	private Player stat;
	private Ranking rank;

	// Use this for initialization
	void Start () {
		/*
		var Stats = StatCollector.Load(Path.Combine(Application.dataPath, playerStatsXMLfilepath));

		Debug.Log ("Loop through player events");
		for (int i=0; i < Stats.Players.Length; i++) {
			stat = Stats.Players [i];
			Debug.Log (stat.playerName);
			Debug.Log (stat.totalRounds);
			Debug.Log (stat.totalPoints);
			Debug.Log (stat.highScore);
		}*/
	}

	public Player GetStats() {
		var Stats = StatCollector.Load(Path.Combine(Application.dataPath, playerStatsXMLfilepath));
		stat = Stats.Players [0]; //pelaajia on vain yksi
		return stat;
	}
}
