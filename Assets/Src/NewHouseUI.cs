using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;

public class NewHouseUI : MonoBehaviour {
	public static NewHouseUI Instance;
    Vector2 zoePos = new Vector2(0, 0);
    GameObject CanvasUI;
    Texture HouseTexture;
    public Material HouseMaterial;
    public GameObject ShowSphere;
    private string QuString;
    public GameObject AllNewHouseShow;
    public GameObject NextNewHouseShow;
    public GameObject[] ObjNewHouseShow;        //4张图加载房子图
    public GameObject[] ObjNewHouseShowButton;  //4张图做边框
    WWW LoadTexture;
    string sPath;
    string NewPath;  //NewHouseShow的图片的路径

    public bool isAtHouse = false;
    public bool isAtModel = false;
     
    //相机的旋转角度
    public  float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;
    //相机的位置
    public float px = 0.0f;
    public float py = 0.0f;
    public float pz = -1.0f;

    public float ry = 0.0f;
    //限制旋转角度，物体绕x轴旋转限制，不能上下颠倒。
    float yMinLimit = -40;
    float yMaxLimit = 40;

    string ShowingName = "";
    GameObject CanvasMain;
    void Awake()
    {
        Instance = this;
        CanvasMain = GameObject.Find("Main Camera");
        CanvasUI = GameObject.Find("CanvasBG");
    }

    //加载全部图片
    public Texture[] HouseAllTexture;
   
    void EndGC()
    {
        LoadTexture = null;
        Resources.UnloadUnusedAssets();
    }

    //接收x和y值，实现同步看房
    void Update()
    {
        if (ShowingName != "")
        {
            if (ShowingName == "End")
            {
                HandleUI.Instance.UIReset();
            }
            else
            {
                string[] showMessage = ShowingName.Split('*');

                if (showMessage[0] == "xy")
                {
                    x = float.Parse(showMessage[1]);
                    y = float.Parse(showMessage[2]);
                    isAtHouse = true;
                }
                else if (showMessage[0] == "Change")
                {
					Debug.Log("Change["+showMessage[1]+"]");
					UdpRespons.Instance.LoadImageToSphere(showMessage[1]);
                }
                else if (showMessage[0] == "Out")
                {
					Debug.Log("Show");
                    UdpRespons.Instance.ReturnMainCanvas();
                }
				else if (showMessage[0] == "Message")
                {

                   UdpRespons.Instance.LoadMessageToUI(showMessage[1]);
                }
				else if (showMessage[0] == "Show")
                {
				   Debug.Log("Show");
                   UdpRespons.Instance.ShowMessage();
                }
				else if (showMessage[0] == "Hiden")
                {
				   Debug.Log("Hiden");
                   UdpRespons.Instance.HidenMessage();
				   Debug.Log("Hiden_Ok");
                }
                else if (showMessage[0] == "rxy")
                {
                    ry = float.Parse(showMessage[2]);
                }
                else if (showMessage[0] == "pxy")
                {
                    px = float.Parse(showMessage[1]);
                    py = float.Parse(showMessage[2]);
                    pz = float.Parse(showMessage[3]);
                    isAtModel = true;
                }
            }
            ShowingName = "";
        }
    }
    public void NewhandleReceiveMessage(string msg)
    {
        ShowingName = msg;
    }

   

    void LateUpdate()
    {
        //限制绕x轴的移动。
        // y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
        if (isAtHouse)
        {
            CanvasMain.transform.rotation = Quaternion.Euler(y, -x, z);
        }
        if (isAtModel)
        {
            CanvasMain.transform.position = new Vector3(px, py, pz);
        }
    }

    //进房间
    IEnumerator InHouse()
    {
        for (int i = 0; i < 51; i++)
        {
            ShowSphere.transform.position = new Vector3(0, 0, -0.02f * i);   //拉近房间的动画
            ShowSphere.transform.Rotate(1.8f, 0, 0);                         //调整房间的方向
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void ClickHousePoint(string HouseName)
    {
        //加载图片
        //HouseTexture = Resources.Load(HouseName, typeof(Texture)) as Texture;
        //sPath = Application.streamingAssetsPath + "/" + HouseName + ".jpg";
        //LoadTexture = new WWW(sPath);
        //HouseTexture = LoadTexture.texture;
        HouseMaterial.mainTexture = HouseAllTexture[int.Parse(HouseName)];
        Resources.UnloadUnusedAssets();

        CanvasUI.SetActive(false);                                               //关闭旧UI摄像头
        StartCoroutine(InHouse());
    }

    //在房间中换房间图
    public void ClickHouseRoom(string Room)
    {
        //加载图片
        //HouseTexture = Resources.Load(Room, typeof(Texture)) as Texture;
        sPath = Application.streamingAssetsPath + "/" + Room + ".jpg";
        LoadTexture = new WWW(sPath);
        HouseTexture = LoadTexture.texture;
        HouseMaterial.mainTexture = HouseTexture;

        Resources.UnloadUnusedAssets();
    }

    //退出房间
    public void ClickReturnHouse()
    {
        StopAllCoroutines();
        ObjectManager.Instance.MainCanvas.SetActive(true);                                                //小区界面

        //房间球还原
        ShowSphere.transform.position = new Vector3(0,0,-1);
        ShowSphere.transform.rotation = Quaternion.Euler(-91.8f, 0, 0);

        //主摄像机还原
        CanvasMain.transform.rotation = Quaternion.Euler(0, 0, 0);
        x = 0;
        y = 0;
    }
}
