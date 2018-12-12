using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZaoBoEEfCtrl : MonoBehaviour {
    private float Timer;
    public float CountinueTime = 0.5f;
    public float IntervalTime = 5f;
    public GameObject CtrlUI;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if (Timer >= IntervalTime)
        {
            TriggerOnce(CountinueTime);
            Timer = 0;
        }

    }

    void TriggerOnce( float CountinueTime)
    {
        CtrlUI.SetActive(true);
        StartCoroutine(TriggerUI(CountinueTime));
    }

    IEnumerator TriggerUI(float CountinueTime)
    {
        yield return new WaitForSecondsRealtime(CountinueTime);
        CtrlUI.SetActive(false);
        //yield return new WaitForSecondsRealtime(0.1f);
        //CtrlUI.SetActive(true);
        //yield return new WaitForSecondsRealtime(CountinueTime);
        //CtrlUI.SetActive(false);
    }
}
