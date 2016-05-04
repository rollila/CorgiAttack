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

	void Start() {
		transform.FindChild ("SignCanvas").gameObject.SetActive (!menu.GetLessDoge ());
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player

			//corgi dashaa eli sign hajoaa
			if (other.GetComponent<PlatformerCharacter2D> ().IsDashing ()) {
				anim.SetBool ("corgiBreaksSign", true);
				other.gameObject.GetComponent<PlatformerCharacter2D> ().AddPoints (points);
				Debug.Log ("Corgi dashed through sign and got points!");
				boxy.enabled = false;
				StartCoroutine(Destroy());
			} else { //corgi kuolee
				other.GetComponent<PlatformerCharacter2D> ().CorgiCollision ();
				Debug.Log ("Corgi hit sign");
				int playerScore = other.GetComponent<PlatformerCharacter2D> ().GetPoints ();
				menu.Death(playerScore);
			}
		}
	}

	//jotta hajoamisanimaatio ehtis näkyä ennenku objekti tuhotaan niin pitää odottaa vähäse
	IEnumerator Destroy() {
		yield return new WaitForSeconds (0.2f); //pituus
		Destroy(this.gameObject);
	}
}
