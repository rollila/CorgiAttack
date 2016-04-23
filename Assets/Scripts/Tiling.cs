using UnityEngine;
using System.Collections;

public class Tiling : MonoBehaviour {

    public Transform[] spawnables;
    public float movedX;

	//Lisan lisäämät poistettavat 
	public float posY;
	public float startX;

	// Use this for initialization
	void Start () {
		//Lisäsin tälläsen y-positionaajaan ainaki väliaikasesti ja toi startX siirtää spawnia vähä kauemmas nii ettei se näy
		posY = 4;
		startX = 13;

        Instantiate(spawnables[Random.Range(0, 2)], new Vector3(transform.position.x, transform.position.y - posY, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x / movedX > 10)
        {
            Debug.Log("Spawn shit now");
            Instantiate(spawnables[Random.Range(0, 2)], new Vector3(transform.position.x + startX, transform.position.y - posY, 0), Quaternion.identity);
            movedX++;
        }

	
	}
}
