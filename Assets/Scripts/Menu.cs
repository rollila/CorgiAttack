using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	//All UI Windows
	public GameObject startScreen;
	public GameObject statScreen;
	public GameObject scoreboardScreen;
	public GameObject globalScreen;
	public GameObject localScreen;
	public GameObject scoreMenuScreen;
	public GameObject badScoreScreen;
	public GameObject goodScoreScreen;
	public GameObject howToScreen;
	public GameObject doYouReallyScreen;
	public GameObject enterNameScreen;
	public GameObject settingsScreen;

	//Assorted stuff
	private GameObject currentScreen;
	private StatHandler handler;
	private AudioSource audioS;

	//Settings
	private bool lessDoge;
	private int musicVol;
	public GameObject moreDogeScreen;

	void Awake() {
		Time.timeScale = 0; //pause game
		handler = GetComponent<StatHandler> ();
		audioS = GetComponent<AudioSource> ();
	}

	void Start() {
		Player player = handler.GetStats();

		if (player.playerName == null) {
			EnterName ();
		} else {
			ActivateStartScreen ();
		}
	}

	public void EnterName() {
		enterNameScreen.SetActive (true);
		SetCurrentScreen (enterNameScreen);
	}

	public void SubmitName(string name) {
		SaveName (name);
	}

	public void SubmitNameButton() {
		InputField field = enterNameScreen.GetComponent<InputField> ();
		string text = enterNameScreen.transform.FindChild("InputField").FindChild("Text").GetComponent<Text>().text;
		SubmitName (text);
		currentScreen.SetActive (false);
		ActivateStartScreen ();
	}

	public void ReloadGame() {
		SceneManager.LoadScene (0);
	}

	public void DoYouReallyWantToQuit() {
		DeactivateStartScreen ();
		doYouReallyScreen.SetActive (true);
		SetCurrentScreen (doYouReallyScreen);
	}
	
	public void QuitPressed() {
		Application.Quit ();
	}

	public void SettingsPressed() {
		DeactivateStartScreen ();
		settingsScreen.SetActive (true);
		SetCurrentScreen (settingsScreen);

		//togglet
		Player player = handler.GetStats();
		Toggle ExtraDoge = GameObject.Find ("ExtraDogeToggle").gameObject.GetComponent<Toggle> ();
		Toggle LessDoge = GameObject.Find ("LessDogeToggle").gameObject.GetComponent<Toggle> ();

		if (player.lessDoge == 0) {
			LessDoge.isOn = false;
		} else if (player.lessDoge == 1) {
			LessDoge.isOn = true;
		}

		if (player.moreDoge == 0) {
			ExtraDoge.isOn = false;
		} else if (player.moreDoge == 1) {
			ExtraDoge.isOn = true;
		}

		//player.musicVol //tallennettu musiikkivolume
	}

	void SaveSettings() {
	}

	public bool GetLessDoge() {
		return lessDoge;
	}

	public void ToggleLessDoge(bool newVal) {
		lessDoge = newVal;
	}

	public void ToggleMoreDoge(bool newVal) {
		moreDogeScreen.SetActive (newVal);
	}

	public void StartPressed() {
		Time.timeScale = 1;
		DeactivateStartScreen ();
	}

	public void HowToPressed() {
		howToScreen.SetActive (true);
		SetCurrentScreen (howToScreen);
	}

	public void StatPressed() {
		DeactivateStartScreen ();
		statScreen.SetActive (true);
		SetCurrentScreen (statScreen);
		Player player = handler.GetStats();
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
		scoreboardScreen.GetComponent<FirebaseAPI> ().PreloadScores (); //lataa serveriltä global scoret
	}

	public void LocalPressed() {
		scoreMenuScreen.SetActive (false);
		localScreen.SetActive (true);
		SetCurrentScreen (localScreen);
		scoreboardScreen.GetComponent<Scoreboard> ().ShowLocal ();
	}

	public void GlobalPressed() {
		//tää koko scoreboard hässäkkä pitäis tehä paremmin
		scoreMenuScreen.SetActive (false);
		globalScreen.SetActive (true);
		SetCurrentScreen (globalScreen);
		scoreboardScreen.GetComponent<Scoreboard> ().ShowScores ();
	}

	public void Death(int playerScore) {
		Time.timeScale = 0; //pause game, vai jotain muuta?
		scoreboardScreen.SetActive (true);
		Ranking[] TopTen = scoreboardScreen.GetComponent<Scoreboard> ().GetTopTen ();
		if (TopTen [9].score < playerScore) { //onko score korkeempi ku 10. sija (tällä hetkellä local only)
			GoodScore (playerScore);
		} else {
			BadScore (playerScore);
		}
	}

	void SaveName(string playerName) {
		Player player = handler.GetStats();
		player.playerName = playerName;
		handler.SaveStats (player);
	}

	void SaveStats(int playerScore) {
		Player player = handler.GetStats();
		player.totalPoints += playerScore;
		player.totalRounds += 1;
		if (player.highScore < playerScore) {
			player.highScore = playerScore;
		}

		handler.SaveStats (player);
	}

	public void ResetStats() {
		Player player = handler.GetStats ();
		player.totalPoints = 0;
		player.totalRounds = 0;
		player.highScore = 0;
		//player.playerName = null;
		handler.SaveStats (player);
		StatPressed ();
	}

	public void BadScore(int playerScore) {
		SetCurrentScreen (badScoreScreen);
		badScoreScreen.SetActive (true);
		badScoreScreen.transform.FindChild("Score").GetComponent<Text> ().text = playerScore+"";
		SaveStats (playerScore);
	}

	public void GoodScore(int playerScore) {
		SetCurrentScreen (goodScoreScreen);
		goodScoreScreen.SetActive (true);
		goodScoreScreen.transform.FindChild("Score").GetComponent<Text> ().text = playerScore+"";

		SaveStats (playerScore);
		Player player = handler.GetStats ();
		scoreboardScreen.GetComponent<Scoreboard> ().SaveLocal (playerScore, player.playerName);

		//tällä hetkellä global score lähetetään vain jos local score on hyvä
		scoreboardScreen.GetComponent<Scoreboard> ().SaveGlobal (playerScore, player.playerName);
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
		currentScreen.SetActive (false);
		startScreen.SetActive (false);
	}

	public void ActivateStartScreen() {
		SetCurrentScreen (startScreen);
		startScreen.SetActive (true);
	}

	public void SetCurrentScreen(GameObject screen) {
		audioS.Play ();
		currentScreen = screen;
	}
}
