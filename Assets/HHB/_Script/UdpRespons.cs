using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UdpRespons : MonoBehaviour {
	public static UdpRespons Instance;
	void Start()
	{
		Instance=this;
	}

	public void LoadImageToSphere(string ImageId)
	{
		Debug.Log("ImageID"+ImageId);
		List<string[]> result =new List<string[]>();
		result = DataDBContorller.Select("select HouseShowType FROM biulding WHERE id in(SELECT biulding_Id from image WHERE id="+ImageId+");");
		int HouseShowType = int.Parse(result[0][0]);//获取到展示类型   1为全景  2为平面
		switch(HouseShowType)
		{
			case 1:
				LoadSphere.Instance.LoadSphereTextureById(ImageId);
				Debug.Log("当前全景看房模式");
			break;
			case 2:
			    LoadPlaneImage.Instance.LoadPlaneImageById(ImageId);
				ObjectManager.Instance.PlaneImageCanvas.SetActive(true);
				Debug.Log("当前平面看房模式");
			break;
            case 3:
                LoadPlaneImage.Instance.LoadPlaneImageById(ImageId);
                ObjectManager.Instance.PlaneImageCanvas.SetActive(true);
                Debug.Log("当前平面看房模式");
                break;
            case 4:
                Debug.Log("当前模型看房模式");
                Load3DModel.Instance.LoadModelByImageID(ImageId);
                break;

        }
	}

    public void ReturnMainCanvas()
    {
        StopAllCoroutines();
        LoadSphere.Instance.IsAtHouse = false;
        ObjectManager.Instance.MainCanvas.SetActive(true);                                                //小区界面
        ObjectManager.Instance.PlaneImageCanvas.SetActive(false);
        Load3DModel.Instance.ResetAllModel();
        ObjectManager.Instance.RoomSphere.SetActive(true);
        //房间球还原
        ObjectManager.Instance.RoomSphere.transform.position = new Vector3(0, 0, -1);
        ObjectManager.Instance.RoomSphere.transform.rotation = Quaternion.Euler(-91.8f, 0, 0);
        //主摄像机还原
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, -1);
        NewHouseUI.Instance.x = 0;
        NewHouseUI.Instance.y = 0;
        NewHouseUI.Instance.z = 0;
        NewHouseUI.Instance.px = 0;
        NewHouseUI.Instance.py = 0;
        NewHouseUI.Instance.pz = -1.0f;
        NewHouseUI.Instance.isAtHouse = false;
        NewHouseUI.Instance.isAtModel = false;
        ObjectManager.Instance.RoomCanvas_HouseMessage.SetActive(false);
        foreach (var item in AreaManager.Instance.areas)
        {
            //item.GetComponent<Image>().enabled = false;
            //item.transform.Find("Light").gameObject.SetActive(false);
        } 
    }
    

	public void LoadMessageToUI(string House_Id)
	{
		LoadRoomMessage.Instance.LoadHouseMessageByHouse_Id(House_Id);
	}

	public void ShowMessage()
	{
		ObjectManager.Instance.RoomCanvas_HouseMessage.SetActive(true);
	}

	public void HidenMessage()
	{
		ObjectManager.Instance.RoomCanvas_HouseMessage.SetActive(false);
	}
}
