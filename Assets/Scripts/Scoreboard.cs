using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{

    private string url = "https://fiery-heat-9158.firebaseio.com/corgiScores.json";

    // Use this for initialization
    void Start()
    {
        //SendScoreButtonPress();
        //GetScores();
    }

    void SendScoreButtonPress()
    {
        //TODO:
        string playerName = "tester"; //set from input, max 10 letters (validation set in firebase)
        int playerScore = 5; //set playerScore

        if (playerName == null)
        {
            //validate name?
        }

        playerName = playerName.Trim();

        PostScore(playerName, playerScore);

    }

    void PostScore(string name, int score)
    {
        Hashtable data = new Hashtable();
        data.Add("name", name);
        data.Add("score", score);

        HTTP.Request theRequest = new HTTP.Request("post", url, data);
        theRequest.Send((request) =>
        {
            bool result = false;
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text, ref result);
            if (!result)
            {
                //ERROR:
                Debug.LogWarning("Could not parse JSON response!");
                return;
            }
            else
            {
                //HTTP POST worked:
                Debug.Log("POSTed");
            }
        });
    }

    void GetScores()
    {
        //List<> scores = new List<>();
        HTTP.Request someRequest = new HTTP.Request("get", url);
        someRequest.Send((request) => {
            Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);
            if(decoded == null) 
            {
                Debug.LogError("server returned null or     malformed response ):");
                return;
            }

            foreach (DictionaryEntry json in decoded)
            {
                Hashtable jsonObj = (Hashtable)json.Value;
                string name = (string)jsonObj["name"];
                int score = (int)jsonObj["score"];

                //scores.Add(name+" "+score);

                Debug.Log("GET RESULT:"+name + " " + score);
            }
        });
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

