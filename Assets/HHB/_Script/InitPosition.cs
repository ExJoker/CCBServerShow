using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPosition : MonoBehaviour {
    Vector3 backgroundInitPos;
    Vector3 showFrameInitPos;

    private void OnEnable()
    {
        transform.GetComponent<RawImage>().color = new Color(1,1,1,0);
    }
    // Use this for initialization
    void Start () {
        backgroundInitPos = transform.position;
        showFrameInitPos = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnDisable()
    {
        transform.position = backgroundInitPos;
        transform.GetChild(0).position = showFrameInitPos;
        transform.GetComponent<RawImage>().color = new Color(1,1,1,0);

    }
}
