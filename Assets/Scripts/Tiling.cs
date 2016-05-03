using UnityEngine;
using System.Collections;

public class Tiling : MonoBehaviour {

    public Transform[] spawnables;
    public float movedX;
	public float fixY;
	public float offCamera;

	// Use this for initialization
	void Start () {
		//näihin molempiin on varmaa parempi ratkaisu
		fixY = 7f; //en saanu muuten paikalleen
		offCamera = 15f; //spawnaa ulompana

		Instantiate(spawnables[Random.Range(0, 2)], new Vector3(transform.position.x, transform.position.y-fixY, 0-5), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x / movedX > 10)
        {
            Debug.Log("Spawn shit now");
			Instantiate(spawnables[Random.Range(0, 7)], new Vector3(transform.position.x+offCamera, transform.position.y-fixY, 0-5), Quaternion.identity);
            movedX++;
        }

	
	}
}
