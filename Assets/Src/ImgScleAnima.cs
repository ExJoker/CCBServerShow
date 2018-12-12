using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgScleAnima : MonoBehaviour {
    public float ZoomSize=2;
    public float Speed = 1;
    private bool Big=true;
    private bool Sm;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Big && transform.localScale.x <= ZoomSize && transform.localScale.y <= ZoomSize)
        {
            transform.localScale = new Vector2(transform.localScale.x + Time.deltaTime * Speed, transform.localScale.y + Time.deltaTime * Speed);
        }
        else
        {
            Sm = true;
            Big = false;
        }

        if (Sm && transform.localScale.x > 1 && transform.localScale.y > 1)
        {
            transform.localScale = new Vector2(transform.localScale.x - Time.deltaTime * Speed, transform.localScale.y - Time.deltaTime * Speed);
        }
        else
        {
            Big = true;
            Sm = false;
        }
    }
}
