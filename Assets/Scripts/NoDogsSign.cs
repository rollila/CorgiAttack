using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class NoDogsSign : MonoBehaviour {
	private Animator anim;
	private Canvas canvas;
	private Menu menu;
	public int points;

	private BoxCollider2D boxy;

	void Awake() {
		anim = GetComponent<Animator> ();
		boxy = GetComponent<BoxCollider2D> ();
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
		menu = canvas.GetComponent<Menu> ();
		points = 100; //hmm...
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			//jos corgi dash -> hajoaa

			//animation test:
			if (other.GetComponent<PlatformerCharacter2D> ().IsDashing ()) {
				anim.SetBool ("corgiBreaksSign", true);
				other.gameObject.GetComponent<PlatformerCharacter2D> ().AddPoints (points);
				Debug.Log ("Corgi dashed through sign and got points!");
				//animation["sign_break"].wrapMode = WrapMode.Once;
				//animation.Play ("sign_break");
				boxy.enabled = false;
				StartCoroutine(Destroy());
			} else {
				Debug.Log ("Corgi hit sign");
				int playerScore = other.GetComponent<PlatformerCharacter2D> ().GetPoints ();
				menu.Death(playerScore);
			}
		}
	}

	//nope
	IEnumerator Destroy() {
		yield return new WaitForSeconds (0.2f); //pituus
		Destroy(this.gameObject);
	}
}
