using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	public Button startBut;
	public Button statBut;
	public Button scoreBut;
	public Button closeBut;
	public Button howBut;

	private StatHandler handler;
	private Player player;
	private FirebaseAPI _firebase;

	public GameObject statScreen;
	public GameObject scoreboardScreen;
	public GameObject globalScreen;
	public GameObject localScreen;
	public GameObject scoreMenuScreen;
	private GameObject currentScreen;

	void Awake() {
		handler = gameObject.GetComponent<StatHandler> ();
		_firebase = scoreboardScreen.GetComponent<FirebaseAPI> ();
		//_firebase.enabled = true;
		//_firebase.PreloadScores(); //try to preload scores on launch
		Time.timeScale = 0; //pause game
	}
	// Use this for initialization
	void Start () {
	}
	
	public void QuitPressed() {
		Application.Quit ();
	}

	public void StartPressed() {
		Time.timeScale = 1;
		DeactivateStartScreen ();
	}

	public void StatPressed() {
		DeactivateStartScreen ();
		statScreen.gameObject.SetActive (true);
		currentScreen = statScreen;

		player = handler.GetStats ();
		statScreen.transform.FindChild("Name").GetComponent<Text> ().text = player.playerName+"";
		statScreen.transform.FindChild("Highscore").GetComponent<Text> ().text = player.highScore+"";
		statScreen.transform.FindChild("TotalRounds").GetComponent<Text> ().text = player.totalRounds+"";
		statScreen.transform.FindChild("TotalPoints").GetComponent<Text> ().text = player.totalPoints+"";
	}

	public void ScoreboardPressed() {
		DeactivateStartScreen ();
		scoreboardScreen.SetActive (true);
		scoreMenuScreen.SetActive (true);
		currentScreen = scoreboardScreen;
	}

	public void LocalPressed() {
	}

	public void GlobalPressed() {
		//tää koko scoreboard hässäkkä pitäis tehä paremmin
		scoreMenuScreen.SetActive (false);
		globalScreen.SetActive (true);
		currentScreen = globalScreen;
		scoreboardScreen.GetComponent<Scoreboard> ().ShowScores ();
	}

	public void ScoreboardReturn() {
		currentScreen.SetActive (false);
		currentScreen = scoreboardScreen;
		ReturnPressed ();
	}

	public void ReturnPressed() {
		currentScreen.SetActive (false);
		this.gameObject.SetActive (true);
	}

	void DeactivateStartScreen() {
		this.gameObject.SetActive (false);
	}
}
