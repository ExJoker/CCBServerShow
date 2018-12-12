using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourcesMgr : MonoBehaviour {
    public string PointPath { get; private set; }
    public string TransportPath { get; private set; }
    public string IsShowingHousetypePath { get; private set; }
    public string OutsidePath { get; private set; }

    public List<string> pointsList;

    // Use this for initialization
    void Start () {
        PointPath = Application.streamingAssetsPath + "/jiyuecheng";
        SetPath();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPath()
    {
        string[] dirs = Directory.GetDirectories(PointPath);

        foreach (var ele in dirs)
        {
            Debug.Log(ele);
            string[] arr = ele.Split('\\');
            string folderName = arr[arr.Length - 1];
            switch (folderName)
            {
                case "IsShowingHousetype":
                    IsShowingHousetypePath = ele;
                    break;
                case "outside":
                    OutsidePath = ele;
                    break;
                case "transport":
                    TransportPath = ele;
                    break;
            }
        }
    }
}
