using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class FallArea : MonoBehaviour {
	private Canvas canvas;
	private Menu menu;
	private AudioSource audioS;
	private BoxCollider2D boxy;

	// Use this for initialization
	void Awake () {
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
		menu = canvas.GetComponent<Menu> ();
		audioS = GetComponent<AudioSource> ();
		boxy = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			Debug.Log("Corgi fell");
			boxy.enabled = false;
			int playerScore = other.GetComponent<PlatformerCharacter2D> ().GetPoints ();
			other.GetComponent<PlatformerCharacter2D> ().CorgiFell ();
			menu.Death(playerScore);
			audioS.Play ();
		}
	}
}
