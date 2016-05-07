﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeHandler : MonoBehaviour {
	public int lives;
	public List<int> scores;

	void Awake() {
		DontDestroyOnLoad (this);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		scores = new List<int> ();
		lives = 3;	
	}

	public int GetLives() {
		return lives;
	}
	
	public void ReduceLife() {
		lives--;
	}

	public void ResetLives() {
		lives = 3;
	}

	//kenties niitä voi poimia kentältä!
	public void AddLife() {
		if (lives < 3) {
			lives++;
		}
	}

	public List<int> GetScores() {
		return scores;
	}

	public int GetTotalScore()  {
		int sum = 0;
		foreach (int score in scores) {
			sum += score;
		}
		return sum;
	}

	public void StoreScore(int score) {
		scores.Add (score);
	}

	public void ResetScores() {
		scores = new List<int> ();
	}
}