using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCtrl : MonoBehaviour {
	public GameObject Spot;
	public void TriggerAnima()
	{
		this.gameObject .SetActive(false);
		Spot.SetActive(false);
	}
}
