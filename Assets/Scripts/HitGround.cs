using UnityEngine;
using System.Collections;

public class HitGround : MonoBehaviour {
	private Canvas canvas;
	private Menu menu;

	// Use this for initialization
	void Awake () {
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
		menu = canvas.GetComponent<Menu> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			Debug.Log("Corgi hit edge of platform");
			int playerScore = other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().GetPoints ();
			menu.Death(playerScore);
			//täällä vois esim soittaa musaa tjsp
		}
	}
}
