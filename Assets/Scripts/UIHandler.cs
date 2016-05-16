using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	//Score UI places:
	private Transform UIpoints;
	private Transform UIincPoints;
	private Transform UIMultiplier;

	// Use this for initialization
	void Awake () {
		UIpoints = GameObject.Find("Canvas").transform.FindChild("BottomUIBar").FindChild("Points");
		UIincPoints = GameObject.Find("Canvas").transform.FindChild("BottomUIBar").FindChild("IncPoints");
		UIMultiplier = GameObject.Find("Canvas").transform.FindChild("BottomUIBar").FindChild("Multiplier");
	}

	public void UpdateScore(int score) //update score on UI
	{
		UIpoints.GetComponent<Text>().text = score + "";
	}

	public void AddPoints(int points) { //add extra points to UI
		UIincPoints.GetComponent<Text> ().text = "+ "+points + "";
		StartCoroutine (WaitASecond ());
		//UIincPoints.GetComponent<Text> ().text = "";
	}

	public void SetMultiplier(int value) {
		UIMultiplier.GetComponent<Text> ().text = value + "X";
	}

	public void EmptyMultiplier() {
		UIMultiplier.GetComponent<Text> ().text = "";
	}

	IEnumerator WaitASecond() { //wait 1 sec before removing from UI
		yield return new WaitForSeconds(1);
		UIincPoints.GetComponent<Text> ().text = "";
	}

}
