using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
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
	public GameObject creditScreen;
	public GameObject betweenRoundsScreen;
	public GameObject pauseScreen;
	public GameObject logo;

	//lives
	public GameObject LifeScreen;
	private LifeHandler lHandler;

	//Assorted stuff
	private GameObject currentScreen;
	private StatHandler handler;
	private AudioSource audioS;
	private FirebaseAPI firebase;
	private Scoreboard scoreboard;

	//Settings
	private bool moreDoge;
	private bool lessDoge;
	private float musicVol;
	private float sfxVol;
	public GameObject moreDogeScreen;
	public AudioMixer mixer;


	void Awake() {
		Time.timeScale = 0; //pause game
		lHandler = GameObject.Find("LifeHandler").gameObject.GetComponent<LifeHandler>();
		handler = GetComponent<StatHandler> ();
		firebase = GetComponent<FirebaseAPI> ();
		scoreboard = GetComponent<Scoreboard> ();
		audioS = GetComponent<AudioSource> ();
	}

	void Start() {
		//lue tallennetut settingingit:
		Player player = handler.GetStats();
	
		lessDoge = player.lessDoge;
		moreDoge = player.moreDoge;
		ToggleMoreDoge (moreDoge);
		SetSFXVolume (player.sfxVol);
		SetMusicVolume (player.musicVol);

		if (player.playerName == null) {
			EnterName ();
		} else {
			if (!lHandler.GetRoundInProgress ()) {
				ActivateStartScreen ();
				firebase.PreloadScores ();
			} else {
				Time.timeScale = 1;
			}
		}

		//päivitä elämät
		ChangeLives ();
	}

	public void PauseGame() {
		Time.timeScale = 0;
		pauseScreen.SetActive (true);
	}

	public void UnpauseGame() {
		Time.timeScale = 1;
		pauseScreen.SetActive (false);
	}

	public void EnterName() {
		enterNameScreen.SetActive (true);
		SetCurrentScreen (enterNameScreen);
	}

	public void ChangeNamePressed() {
		currentScreen.SetActive (false);
		logo.SetActive(false);
		EnterName ();
	}

	public void SubmitName(string name) {
		SaveName (name);
	}

	public void SubmitNameButton() {
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
		Slider music = GameObject.Find ("MusicSlider").gameObject.GetComponent<Slider> ();
		Slider SFX = GameObject.Find ("SFXSlider").gameObject.GetComponent<Slider> ();

		LessDoge.isOn = player.lessDoge;
		ExtraDoge.isOn = player.moreDoge;
		music.value = player.musicVol;
		SFX.value = player.sfxVol;
	}

	public void SaveSettings() {
		Player player = handler.GetStats();
		player.lessDoge = lessDoge;
		player.moreDoge = moreDoge;
		player.musicVol = musicVol;
		player.sfxVol = sfxVol;

		handler.SaveStats (player);
		ReturnPressed ();
	}

	public void SetSFXVolume(float sVol) {
		mixer.SetFloat ("SFXVol", sVol);
		sfxVol = sVol;
	}

	public void SetMusicVolume(float mVol) {
		mixer.SetFloat ("MusicVol", mVol);
		musicVol = mVol;
	}

	public bool GetLessDoge() {
		return lessDoge;
	}

	public void ToggleLessDoge(bool newVal) {
		lessDoge = newVal;
	}

	public void ToggleMoreDoge(bool newVal) {
		moreDoge = newVal;
		moreDogeScreen.SetActive (newVal);
	}

	public void StartPressed() {
		logo.SetActive (false);
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
		//firebase.PreloadScores (); //lataa serveriltä global scoret
	}

	public void LocalPressed() {
		scoreMenuScreen.SetActive (false);
		localScreen.SetActive (true);
		SetCurrentScreen (localScreen);
		scoreboard.ShowLocal ();
	}

	public void GlobalPressed() {
		//tää koko scoreboard hässäkkä pitäis tehä paremmin
		scoreMenuScreen.SetActive (false);
		globalScreen.SetActive (true);
		SetCurrentScreen (globalScreen);
		scoreboard.ShowScores ();
	}

	public void BetweenRounds() {
		betweenRoundsScreen.SetActive (true);
		SetCurrentScreen (betweenRoundsScreen);

		List<int> scores = lHandler.GetScores ();

		for (int i=0; i < scores.Count; i++) {
			int num = i + 1;
			Transform ScorePanel = betweenRoundsScreen.transform.Find("Round"+num+"Points");
			ScorePanel.GetComponent<Text> ().text = scores [i]+"";
		}

	}

	void ChangeLives() {
		int livesMissing = 3 - lHandler.GetLives ();

		for (int i = 0; i < livesMissing; i++) {
			LifeScreen.transform.GetChild(i).GetComponent<Image> ().color = Color.gray;
		}
	}

	void ResetLives() {
		foreach (Transform life in LifeScreen.transform) {
			life.GetComponent<Image> ().color = Color.white;
		}
	}

	public void Death(int playerScore) {
		Time.timeScale = 0; //pause game, vai jotain muuta?

		//elämä vaihtuu
		lHandler.ReduceLife ();
		ChangeLives ();
		lHandler.StoreScore (playerScore);

		if (lHandler.GetLives () == 0) {
			//lopullinen score
			int totalScore = lHandler.GetTotalScore();

			scoreboardScreen.SetActive (true);
			List<Ranking> TopTen = scoreboard.GetTopTen ();
			if (TopTen [9].score < totalScore) { //onko score korkeempi ku 10. sija (tällä hetkellä local only)
				GoodScore (totalScore);
			} else {
				BadScore (totalScore);
			}

			//reset lives+score+end round
			lHandler.StartRound ();
		} else {
			//jatkuu
			BetweenRounds();
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
		goodScoreScreen.GetComponent<AudioSource> ().Play ();
		SaveStats (playerScore);
		Player player = handler.GetStats ();
		scoreboard.SaveLocal (playerScore, player.playerName);

		//tällä hetkellä global score lähetetään vain jos local score on hyvä
		scoreboard.SaveGlobal (playerScore, player.playerName);
	}

	public void CreditPressed() {
		//currentScreen.SetActive (false);
		SetCurrentScreen (creditScreen);
		creditScreen.SetActive (true);
	}

	public void SettingsReturn() {
		currentScreen.SetActive (false);
		SetCurrentScreen (settingsScreen);
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
		logo.SetActive (true);
		SetCurrentScreen (startScreen);
		startScreen.SetActive (true);
	}

	public void SetCurrentScreen(GameObject screen) {
		audioS.Play ();
		currentScreen = screen;
	}
}
