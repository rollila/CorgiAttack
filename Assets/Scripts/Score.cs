using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public string name { get; set; }
    public int points { get; set; }

    public Score() { }
    public Score(string pName, int pPoints)
    {
        name = pName;
        points = pPoints;
    }

}
