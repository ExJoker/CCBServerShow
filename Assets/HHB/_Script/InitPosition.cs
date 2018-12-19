using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosition : MonoBehaviour {
    Vector3 backgroundInitPos;
	// Use this for initialization
	void Start () {
        backgroundInitPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        transform.position = backgroundInitPos;
    }
}
