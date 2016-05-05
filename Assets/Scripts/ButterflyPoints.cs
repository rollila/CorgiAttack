using UnityEngine;
using System.Collections;

public class ButterflyPoints : MonoBehaviour {
	//private PlatformerCharacter2D corgiController;
	public int points;
	private Animator anim;
	private AudioSource audioS;
	private Canvas canvas;
	private Menu menu;
	private BoxCollider2D boxy;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		points = 100; //TBD, ehkä vois vaa päättää kerronnaisena siitä paljon on jo pisteitä tjsp mut testaan ny tällä
		audioS = GetComponent<AudioSource>();
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
		menu = canvas.GetComponent<Menu> ();
		transform.FindChild ("BflyCanvas").gameObject.SetActive (!menu.GetLessDoge ());
		boxy = GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			Debug.Log ("Corgi touched butterfly");
			if (!anim.GetBool ("touched")) {
				Debug.Log ("Corgi got points from touching butterfly!");
				other.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().AddPoints (points);
				boxy.enabled = false;
				anim.SetBool ("touched", true);
				audioS.Play ();
				//StartCoroutine(Destroy());
			}
		}
	}

	IEnumerator Destroy() {
		yield return new WaitForSeconds (0.2f); //pituus
		Destroy(this.gameObject);
	}
}
