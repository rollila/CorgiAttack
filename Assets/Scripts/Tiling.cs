using UnityEngine;
using System.Collections;

public class Tiling : MonoBehaviour {

    public Transform[] spawnables;
    public float movedX;

	// Use this for initialization
	void Start () {
        Instantiate(spawnables[Random.Range(0, 2)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x / movedX > 15)
        {
            Debug.Log("Spawn shit now");
            Instantiate(spawnables[Random.Range(0, 2)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            movedX++;
        }

	
	}
}
