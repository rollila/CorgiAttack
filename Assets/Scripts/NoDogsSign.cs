using UnityEngine;
using System.Collections;

public class NoDogsSign : MonoBehaviour {
	private Animator anim;
	private Canvas canvas;
	private Menu menu;

	void Awake() {
		anim = GetComponent<Animator> ();
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
		menu = canvas.GetComponent<Menu> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			//jos corgi dash -> hajoaa

			//animation test:
			if (other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().IsDashing ()) {
				anim.SetBool ("corgiBreaksSign", true);
				//animation["sign_break"].wrapMode = WrapMode.Once;
				//animation.Play ("sign_break");
				StartCoroutine(Destroy());
			} else {
				int playerScore = other.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().GetPoints ();
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
