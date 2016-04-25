using UnityEngine;
using System.Collections;

public class ButterflyPoints : MonoBehaviour {
	//private PlatformerCharacter2D corgiController;
	public int points;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		points = 100; //TBD, ehkä vois vaa päättää kerronnaisena siitä paljon on jo pisteitä tjsp mut testaan ny tällä
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Corgi touched butterfly");
		if (other.gameObject.layer == 10) { //player 
			if (!anim.GetBool ("touched")) {
				Debug.Log ("Corgi got points from touching butterfly!");
				other.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().AddPoints (points);
				anim.SetBool ("touched", true);
			}
		}
	}
}
