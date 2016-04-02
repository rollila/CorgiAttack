using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Score))]
[RequireComponent (typeof(FirebaseAPI))]
public class Scoreboard : MonoBehaviour
{

    public static Score player;
    public GameObject ScoreBoard;

    //Scripts
    public FirebaseAPI _firebase;

    void Awake()
    {
        _firebase = GetComponent<FirebaseAPI>();
    }

    public void SetPlayer(string name, int points)
    {
        //Score-object of this player is in player
        player = new Score();
        player.name = name;
        player.points = points;
    }

    public void ReturnToStartScreenButton()
    {
        //Probably need this button
    }

    public void ShowScores()
    {
        List<Score> scores = new List<Score>();
        scores = _firebase.GetScores();

        //Firebasen pitäis olla järjestyksessä mutta ei ollu. Kunnes kexin kuinka Firebasen saa järjestykseen ni tää pitää sorttaa:
        scores.Sort((Score x, Score y) => { return x.points.CompareTo(y.points); });

        scores.Reverse();


        //Debug.Log("Got a list with " + scores.Count);
        int i = 0;
        foreach (Score s in scores)
        {
            if (i >= 5 || i > scores.Count-1)
            {
                break;
            }
            Debug.Log(i + "" + s.name + ""+ scores.Count);
            Transform ScorePanel = ScoreBoard.transform.Find("ScorePanel " + i);
            ScorePanel.transform.Find("Score").GetComponent<Text>().text = s.points + "";
            ScorePanel.transform.Find("Name").GetComponent<Text>().text = s.name;

            if (!s.Equals(player))
            {
                //Player gets top 10 highscore: Do things
            }
            i++;
        }
    }
}


