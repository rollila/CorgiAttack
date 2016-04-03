using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour
{

    public float scrollSpeed = 3f;
    public GameObject wtf;
    //public Renderer rndr = GetComponent<Renderer>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        Vector2 offset2 = new Vector2((Time.time * scrollSpeed), 0);
        wtf.GetComponent<Renderer>().material.mainTextureOffset = offset2;
        //rndr.material.mainTextureOffset = offset;
    }
}
