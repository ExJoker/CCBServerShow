using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using MyXML;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    string ShowingName = "";
    public String ip;
    int ReceivePort ;
    int SendPort ;
    //public AudioClip[] clips = new AudioClip[12];          //音乐播放器的数组
    public Dictionary<string, AudioClip> clipList = new Dictionary<string, AudioClip>();
    public bool IsPosition;
    private void Awake()
    {

        //foreach (var item in clips)                        //循环将播放器数组加入字典
        //{
        //    clipList.Add(item.name, item);
        //}
        if (Instance == null)
        {
            Instance = this;
        }
   //     LoadAllData.Instance.GetFile();
        XMLUtil xml = new XMLUtil(Application.streamingAssetsPath + "/IP.xml"); //传入IP端口号xml表 路径
        ip = xml.GetInnerValueStr("root/ip");                                   //获取xml中的IP
        ReceivePort = int.Parse(xml.GetInnerValueStr("root/ReceivePort"));      //获取收包  端口号
        SendPort = int.Parse(xml.GetInnerValueStr("root/SendPort"));            //获取发包  端口号
        IsPosition =Convert.ToBoolean(int.Parse(xml.GetInnerValueStr("root/IsPosition"))); //转换为Bool
        UDPServer.Instance.InitSocket(ip, ReceivePort, SendPort);               //调用Socket方法 传值
        Debug.Log(ip + "~~~~" + ReceivePort);
    }

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
                //ShowUI(showMessage[0], (ShowType)Enum.Parse(typeof(ShowType), showMessage[1]));
            }
            ShowingName = "";

        }

        /*
        if (Input.GetKey(KeyCode.A))
        {
            ShowUI("chengshichuntian", ShowType.HouseStyle);
        }
        if (Input.GetKey(KeyCode.S))
        {
            ShowUI("rundayuanting", ShowType.All);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ShowUI("rundayuanting", ShowType.HouseStyle);
        }
        if (Input.GetKey(KeyCode.F))
        {
            ShowUI("jinzhonghuanfuwugongyu", ShowType.HouseStyle);
        }
        if (Input.GetKey(KeyCode.G)) {
            ShowingName = "End";
        }
        */
    }

    public void handleReceiveMessage(string msg)
    {
        ShowingName = msg;
;    }
    /// <summary>
    /// 开启UI的显示
    /// </summary>
    /// <param name="HouseName">小区名字，全小写</param>
    void ShowUI(string HouseName, ShowType showType)
    {

        if (LoadAllData.Instance.houseList.ContainsKey(HouseName))
        {
            HandleUI.Instance.showUI(LoadAllData.Instance.houseList[HouseName], showType);
        }

    }
    public void SendMSG(string MSG)
    {
        UDPServer.Instance.SocketSend(Encoding.UTF8.GetBytes(MSG));
        Debug.Log(MSG);
    }
    private void OnApplicationQuit()
    {
        UDPServer.Instance.SocketQuit();
    }

}