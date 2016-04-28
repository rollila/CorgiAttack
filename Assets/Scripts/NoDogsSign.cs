using UnityEngine;
using System.Collections;

public class NoDogsSign : MonoBehaviour {
	private Animator anim;

	void Awake() {
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) { //player
			//jos corgi dash -> hajoaa

			//animation test:
			anim.SetBool("corgiBreaksSign", true);
			//animation["sign_break"].wrapMode = WrapMode.Once;
			//animation.Play ("sign_break");
			//Destroy();
		}
	}

	//nope
	IEnumerator Destroy() {
		yield return new WaitForSeconds (0.1f); //pituus
		Destroy(this.gameObject);
	}
}
