using UnityEngine;
using System.Collections;

public class disableMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(disableThisObj());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator disableThisObj()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
