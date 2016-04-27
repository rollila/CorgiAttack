using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public GameObject startScreen;
	public GameObject statScreen;
	public GameObject scoreboardScreen;
	public GameObject globalScreen;
	public GameObject localScreen;
	public GameObject scoreMenuScreen;
	public GameObject badScoreScreen;
	public GameObject goodScoreScreen;
	private GameObject currentScreen;

	void Awake() {
		Time.timeScale = 0; //pause game
		ActivateStartScreen();
	}

	public void ReloadGame() {
		SceneManager.LoadScene (0);
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
		SetCurrentScreen (statScreen);
		Player player = statScreen.GetComponent<StatHandler> ().GetStats();
		statScreen.transform.FindChild("Name").GetComponent<Text> ().text = player.playerName+"";
		statScreen.transform.FindChild("Highscore").GetComponent<Text> ().text = player.highScore+"";
		statScreen.transform.FindChild("TotalRounds").GetComponent<Text> ().text = player.totalRounds+"";
		statScreen.transform.FindChild("TotalPoints").GetComponent<Text> ().text = player.totalPoints+"";
	}

	public void ScoreboardPressed() {
		DeactivateStartScreen ();
		scoreboardScreen.SetActive (true);
		scoreMenuScreen.SetActive (true);
		SetCurrentScreen (scoreboardScreen);
	}

	public void LocalPressed() {
	}

	public void GlobalPressed() {
		//tää koko scoreboard hässäkkä pitäis tehä paremmin
		scoreMenuScreen.SetActive (false);
		globalScreen.SetActive (true);
		SetCurrentScreen (globalScreen);
		scoreboardScreen.GetComponent<Scoreboard> ().ShowScores ();
	}

	public void DeathScreen() {
	}

	public void BadScore() {
		SetCurrentScreen (badScoreScreen);
		badScoreScreen.SetActive (true);
	}

	public void GoodScore() {
		SetCurrentScreen (goodScoreScreen);
		goodScoreScreen.SetActive (true);
	}

	public void ScoreboardReturn() {
		currentScreen.SetActive (false);
		SetCurrentScreen (scoreboardScreen);
		ReturnPressed ();
	}

	public void ReturnPressed() {
		currentScreen.SetActive (false);
		ActivateStartScreen ();
	}

	void DeactivateStartScreen() {
		startScreen.SetActive (false);
	}

	public void ActivateStartScreen() {
		SetCurrentScreen (startScreen);
		startScreen.SetActive (true);
	}

	public void SetCurrentScreen(GameObject screen) {
		currentScreen = screen;
	}
}
