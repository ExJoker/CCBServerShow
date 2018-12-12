using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class CsCheckDogInit : MonoBehaviour {

    public enum EncyptType
    {
        RUNTIMEDB=1,
        GRANDDOG=2
    }

    public EncyptType nType;
    public string szProName;
	public GameObject objFalse;
	public GameObject objShow;
	Timer test;

    private bool bInit=false;


    [DllImport("SfEnIden.dll", EntryPoint = "fnMyDll")]
    public static extern int fnMyDll();


    [DllImport("SfEnIden.dll", EntryPoint = "multi")]
    public static extern bool sfEnIdenCheck(int nCheckType, string szProName);

    

    // Use this for initialization
    void Start () {

        //MyCalc.pMsgBox ("Hello", "abc", 0);
        //Cursor.visible = false;
        //bInit = sfEnIdenCheck((int)nType, szProName);
        //    Debug.Log(bInit);

        //Debug.Log(fnMyDll());

#if UNITY_EDITOR
        bInit =true;
        Debug.Log("Editor!");
        objFalse.SetActive(false);
        objShow.SetActive(true);
#else
        try
		{
			Debug.Log(fnMyDll());
		}
		catch(Exception ex)
		{
			Debug.Log("Can't Init!");
			StartCoroutine(QUITE());
		}

        
		Debug.Log("Check~Once~~~");

        bInit=sfEnIdenCheck((int)nType,szProName);
        // objFalse, objShow
        if (!bInit)
		{
			Debug.Log("Quit");
			StartCoroutine(QUITE());
            objFalse.SetActive(true);
            objShow.SetActive(false);
		}
        else
        {
            objFalse.SetActive(false);
            objShow.SetActive(true);
        }


		test = new Timer(60.0f);
        test.tick += Test;
        test.Start();

#endif


    }

    IEnumerator QUITE()
	{
		yield return new WaitForSeconds (3f);
		Application.Quit ();
	}

	
	// Update is called once per frame
	void Update () {
        if (!bInit)
        {
            Debug.Log("Quit App!!");
			StartCoroutine(QUITE());
        }


        #if UNITY_EDITOR
            //Debug.Log("Editor!");
        #else
            test.Update();
        #endif
	}

    void Test()
    {
        Debug.Log("Check~");
        if (!bInit)
        {
            Debug.Log("Quit App!!");
			StartCoroutine(QUITE());
        }
        try
        {
            //if (!MyCalc.HasDog(szProName))
            if (!sfEnIdenCheck( (int)nType, szProName))
            {
                Debug.Log("Quit Test");
				StartCoroutine(QUITE());

            }
        }
        catch (Exception ex)
        {
            Debug.Log("Can't Init!");
			StartCoroutine(QUITE());
        }
		test.Restart();
    }
}
