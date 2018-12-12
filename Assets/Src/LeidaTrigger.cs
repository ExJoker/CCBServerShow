using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeidaTrigger : MonoBehaviour {
	private GameObject Spot;
	private GameObject Circle;

	// Use this for initialization
	void Start () {
		Spot=transform.Find("Spot").gameObject;
		Circle=transform.Find("Circle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	

	void OnTriggerEnter()
	{
		Spot.SetActive(true);
		Circle.SetActive(true);
	}


}
