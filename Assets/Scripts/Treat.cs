using UnityEngine;
using System.Collections;

public class Treat : MonoBehaviour {
	private float multiplier;
	private Animator anim;
	private AudioSource audioS;
	private Canvas canvas;
	private Menu menu;
	private BoxCollider2D boxy;
	// Use this for initialization

	void Start () {
		anim = GetComponent<Animator>();
		audioS = GetComponent<AudioSource>();
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
		menu = canvas.GetComponent<Menu> ();
		//transform.FindChild ("TreatCanvas").gameObject.SetActive (!menu.GetLessDoge ());
		boxy = GetComponent<BoxCollider2D> ();
		multiplier = 2;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			Debug.Log ("Corgi touched butterfly");
			if (!anim.GetBool ("touched")) {
				
				other.gameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().SetMultiplier (multiplier);
				boxy.enabled = false;
				anim.SetBool ("touched", true);
				audioS.Play ();
				Destroy (this.gameObject);

			}
		}
	}
}
