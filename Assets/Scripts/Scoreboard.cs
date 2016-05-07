using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Score))]
[RequireComponent (typeof(FirebaseAPI))]
public class Scoreboard : MonoBehaviour
{

    public static Score player;
    public GameObject globalScoreBoard;
	public GameObject localScoreBoard;

    //Scripts
    private FirebaseAPI _firebase;
	private TopTenHandler _xml;

	//Maybe need global scoreboards?
	public List<Ranking>TopTen;
	public List<Score> scores;

    void Awake()
    {
        _firebase = GetComponent<FirebaseAPI>();
		_xml = GetComponent<TopTenHandler> ();
    }

	void Start() {
		//ShowScores ();
	}

    public void SetPlayer(string name, int points)
    {
        //Score-object of this player is in player
        player = new Score();
        player.name = name;
        player.points = points;
    }

	public List<Ranking> GetTopTen() {
		TopTen = _xml.GetTopTen ();
		return TopTen;
	}

	public void ResetLocal() {
		int i = 1;
		foreach (Ranking r in TopTen) {
			Ranking rank = new Ranking ("cat", 0);
			TopTen [i - 1] = rank;
			i++;
		}
		_xml.StoreTopTen (TopTen);	
	}

	public void SaveLocal(int playerScore, string playerName) {
		TopTen = _xml.GetTopTen ();
		Ranking rank = new Ranking (playerName, playerScore);
		TopTen [9] = rank;
		TopTen.Sort((Ranking x, Ranking y) => { return x.score.CompareTo(y.score); });
		TopTen.Reverse ();
		_xml.StoreTopTen (TopTen);
	}

	public void ShowLocal() {
		TopTen = _xml.GetTopTen ();
		int i = 1;
		foreach (Ranking r in TopTen) {
			Transform ScorePanel = localScoreBoard.transform.Find("ScorePanel (" + i+")");
			ScorePanel.transform.Find("Rank").GetComponent<Text>().text = i + "";
			ScorePanel.transform.Find("Score").GetComponent<Text>().text = r.score + "";
			ScorePanel.transform.Find("Name").GetComponent<Text>().text = r.name;
			i++;
		}
	}

	public void SaveGlobal(int playerScore, string playerName) {
		_firebase.SendScore (playerName, playerScore);
	}

    public void ShowScores()
    {
        scores = _firebase.GetScores();

        //Firebasen pitäis olla järjestyksessä mutta ei ollu. Kunnes kexin kuinka Firebasen saa järjestykseen ni tää pitää sorttaa:
        scores.Sort((Score x, Score y) => { return x.points.CompareTo(y.points); });

        scores.Reverse();


        //Debug.Log("Got a list with " + scores.Count);
        int i = 0;
        foreach (Score s in scores)
        {
            if (i >= 10 || i > scores.Count-1)
            {
                break;
            }
            //Debug.Log(i + "" + s.name + ""+ scores.Count);
            Transform ScorePanel = globalScoreBoard.transform.Find("ScorePanel (" + i+")");
            ScorePanel.transform.Find("Rank").GetComponent<Text>().text = i+1 + "";
            ScorePanel.transform.Find("Score").GetComponent<Text>().text = s.points + "";
            ScorePanel.transform.Find("Name").GetComponent<Text>().text = s.name;
            i++;
        }
    }
}


