using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
//This library: https://github.com/andyburke/UnityHTTP

[RequireComponent(typeof(Score))]
[RequireComponent(typeof(Scoreboard))]
public class FirebaseAPI : MonoBehaviour
{
    private string url = "https://fiery-heat-9158.firebaseio.com/corgiScores.json";

    //Nää scriptat (Firebase <-> Scoreboard) on nyt näin koska en saanu toimii muuten
    public Scoreboard _board;

    public List<Score> scoreCache = new List<Score>();

    void Awake()
    {
        //Preload scorelist:
		//PreloadScores();
    }

	public void PreloadScores() {
		StartCoroutine(GetAndWaitForRequest());
	}

    // Use this for initialization
    void Start()
    {
        _board = GetComponent<Scoreboard>();
		//PreloadScores ();
    }

    public void TestButton()
    {
        //TESTING:
        //PostScore("Me2", 2);
        //_board.SetPlayer("eioleolemassa", 0);
        _board.ShowScores();
    }

	public void SendScore(string playerName, int playerScore)
    {
        if (playerName == null)
        {
            //validate name?
        }

        playerName = playerName.Trim();

        PostScore(playerName, playerScore);
        //_board.SetPlayer(playerName, playerScore);

		//update cached scores
		UpdateScoreCache ();
    }

    void PostScore(string name, int score)
    {
        Hashtable data = new Hashtable();
        data.Add("name", name);
        data.Add("score", score);

        HTTP.Request theRequest = new HTTP.Request("post", url, data);
        theRequest.Send((request) =>
        {
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (jsonObj == null)
            {
                //ERROR:
                Debug.LogWarning("Could not parse JSON response! God help us!");
                return;
            }
            else
            {
                //HTTP POST worked:
                Debug.Log("POST: "+request.response.Text);
            }
        });
    }

    public List<Score> GetScores()
    {
        //return cached score, because no need to constantly load scores (i hope)
        if (scoreCache == null)
        {
            //Send and wait for a GET
            StartCoroutine(GetAndWaitForRequest());
            return scoreCache;
        } else
        {
            return scoreCache;
        }
    }

    public IEnumerator UpdateScoreCache()
    {
        yield return StartCoroutine(GetAndWaitForRequest());
    }

    IEnumerator GetAndWaitForRequest()
    {
        List<Score> scores = new List<Score>();

        HTTP.Request someRequest = new HTTP.Request("get", url);
        someRequest.Send();

        while (!someRequest.isDone)
        {
            yield return null;
        }

        Hashtable decoded = (Hashtable)JSON.JsonDecode(someRequest.response.Text);
        if (decoded == null)
        {
            Debug.LogError("Server has issues.");
        }

        foreach (DictionaryEntry json in decoded)
        {
            Hashtable jsonObj = (Hashtable)json.Value;
            string name = (string)jsonObj["name"];
            int score = (int)jsonObj["score"];
            Score temp = new Score(name, score);
            scores.Add(temp);
        }

        Debug.Log("Fetched and recieved from server scores:" +scores.Count);
        scoreCache = scores;
    }
}

/* Does not workerino
void PostScore(string name, int score)
{
    WWWForm form = new WWWForm();
    form.AddField("name", name);
    form.AddField("score", score);

    WWW www = new WWW(url, form);
    StartCoroutine(WaitForRequest(www));
    Debug.Log(url);
}

void GetScores()
{
    WWW www = new WWW(url);
    StartCoroutine(WaitForRequest(www));
}

IEnumerator WaitForRequest(WWW www)
{

    yield return www;

    // TODO: PROPER ERRORCHECKING:
    if (www.error == null)
    {
        Debug.Log("WWW Ok!: " + www.text);
        //Data: www.text
    }
    else {
        Debug.Log("WWW Error: " + www.error + www.text);
    }
}
*/

