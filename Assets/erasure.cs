using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class erasure : MonoBehaviour {
	public Text Erasure;
	private string[] Always;
	// Use this for initialization
	void Awake() {
		Always = new string[] {"Open your eyes I see, you eyes are open~", "Wear no disguise for me, come into the open~", "When it's cold outisde, am I here in vain~?", "Hold on to the night, there will be no shame~", "Always~ I wanna be with you~, And make believe with you~", "And live in harmony harmony ~~OH LOVE~~", "Melting ice for me, jump into the ocean~", "Hold back the tide I see, your love in the motion~"};
	}

	void Start () {
		Erasure.text = Always [Random.Range (0, Always.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
