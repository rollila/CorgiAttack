using UnityEngine;
using System.Collections;

public class FallArea : MonoBehaviour {
	public Canvas canvas;
	private Menu menu;

	// Use this for initialization
	void Awake () {
		//canvas = GameObject.Find ("Canvas");
		menu = canvas.GetComponent<Menu> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			Debug.Log("Corgi fell");
			int playerScore = other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().GetPoints ();
			menu.Death(playerScore);
			//täällä vois esim soittaa musaa tjsp
		}
	}
}
