using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRoomMessage : MonoBehaviour {

	public static LoadRoomMessage  Instance;
	private List<string[]> result;
	// Use this for initialization
	void Start () {
		Instance=this;
	}
	
	public void LoadHouseMessageByHouse_Id(string House_Id)
	{
		StopCoroutine("LoadHouseMessageToUI");
		StartCoroutine(LoadHouseMessageToUI(House_Id));
	}


	IEnumerator LoadHouseMessageToUI(string House_Id)
	{
		yield return null;
		string sql= "SELECT biuldingName,region.regionName,TrafficRegion,HouseType,biuldingAddres,HousePosition,SellTell,MoneyTell,HouseSize FROM biulding,region,image WHERE region.id=biulding.region_Id and biulding.id ="+House_Id+";";
		result=DataDBContorller.Select(sql);
		Transform HouseMessage =ObjectManager.Instance.RoomCanvas_HouseMessage.transform;
		foreach (var item in result)
		{
			HouseMessage.GetChild(1).GetComponent<Text>().text=item[0];
			HouseMessage.GetChild(2).GetComponent<Text>().text="房源行政区:  "+item[1];
			HouseMessage.GetChild(3).GetComponent<Text>().text="交通区位:  "+item[2];
			HouseMessage.GetChild(4).GetComponent<Text>().text="地址:  "+item[4];
			HouseMessage.GetChild(5).GetComponent<Text>().text="经纬度:  "+item[5];
			if(item[3].Equals("集中式租赁项目"))
			{
				HouseMessage.GetChild(6).GetComponent<Text>().text="客服热线:  "+item[6];
				HouseMessage.GetChild(7).GetComponent<Text>().text="";
			}
			else
			{
				HouseMessage.GetChild(6).GetComponent<Text>().text="售楼热线:  "+item[6];
				HouseMessage.GetChild(7).GetComponent<Text>().text="建行贷款咨询热线:  "+item[7];
			}
			HouseMessage.GetChild(8).GetComponent<Text>().text="户型:  "+item[8];
		}

	}
}
