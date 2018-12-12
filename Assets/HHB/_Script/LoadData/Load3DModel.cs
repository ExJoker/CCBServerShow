using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load3DModel : Singleton<Load3DModel> {

    private List<GameObject> loadedModels = new List<GameObject>();

    public void LoadModelByImageID(string ImageId)
	{
        Debug.Log("LoadModelByImageID : "+ ImageId);
        StopCoroutine("LoadModelToScene");
        StartCoroutine(LoadModelToScene(ImageId));
    }


	IEnumerator LoadModelToScene(string ImageId)
	{		
		yield return null;
		CueContorller.Instance.ResetCuePool();//重置对象池
		//读取图片并显示
		ObjectManager.Instance.SphereLoding.SetActive(true);
		string sql= "SELECT biuldingName FROM biulding WHERE region_Id = "+ ImageId+";";
		List<string[]> result =new List<string[]>();
		result=DataDBContorller.Select(sql);
        string name = result[0][0];
        GameObject g = Instantiate(Resources.Load(name) as GameObject, Vector3.zero, Quaternion.identity);
        loadedModels.Add(g);
        yield return new WaitForSeconds(0.5f);
        #region 加载图片
        // string TempURL="file://" + Application.streamingAssetsPath +@result[0][0];
        //Debug.Log("加载全景图片ID:"+ImageId+"       URl:"+TempURL);
        //WWW www =new WWW(TempURL);
        //yield return www;
        //NowSphereTexture =www.texture;
        //ObjectManager.Instance.RoomSphere.GetComponent<Renderer>().materials[0].mainTexture = NowSphereTexture;
        //ObjectManager.Instance.RoomSphere.transform.GetChild(1).name=ImageId;//将ID存在物体名字上
        ////读取标识
        //List<string[]> Cueresult =new List<string[]>();
        //if(IsReadCue)
        //{
        //	sql="select * from roomcue where image_Id ="+ImageId+";";
        //	Cueresult=DataDBContorller.Select(sql);
        //	Debug.Log("resultCount:"+Cueresult.Count+"----"+sql);
        //	foreach (var item in Cueresult)
        //	{
        //		//根据数据库中存储的CueType从对象池获取一个对应的标识对象
        //		GameObject TempCue=CueContorller.Instance.GetGameObj((RoomEnum.UIType)int.Parse(item[2]));
        //		//设置父对象
        //		TempCue.transform.SetParent(ObjectManager.Instance.SphereCanvas.transform);
        //		//设置提示文字
        //		TempCue.transform.GetChild(1).GetComponent<Text>().text=item[6];
        //		//设置指向ID
        //		TempCue.transform.GetChild(2).name=item[10];
        //		//设置位置
        //		TempCue.transform.localPosition=new Vector3(float.Parse(item[3]),float.Parse(item[4]),float.Parse(item[5]));
        //		//设置旋转
        //		TempCue.transform.localEulerAngles=new Vector3(float.Parse(item[7]),float.Parse(item[8]),float.Parse(item[9]));
        //		//显示提示
        //		TempCue.SetActive(true);

        //	}

        //}
        //ObjectManager.Instance.SphereLoding.SetActive(false);//显示加载面板
        //ObjectManager.Instance.MainCanvas.SetActive(false);//隐藏主页UI界面
        //Resources.UnloadUnusedAssets();
        //if(!IsAtHouse)
        //{
        //	for (int i = 0; i < 51; i++)
        //	{
        //		ObjectManager.Instance.RoomSphere.transform.position = new Vector3(0, 0, -0.02f * i);   //拉近房间的动画
        //		ObjectManager.Instance.RoomSphere.transform.Rotate(1.8f, 0, 0);                         //调整房间的方向
        //		yield return new WaitForSeconds(0.01f);
        //	}
        //}
        ////设置全景球的旋转为数据库中读取到的数据
        //if(result[0][1] !="")
        //{
        //	Vector3 temp=new Vector3(float.Parse(result[0][1]),float.Parse(result[0][2]),float.Parse(result[0][3]));
        //	ObjectManager.Instance.RoomSphere.transform.eulerAngles=temp;
        //}
        //else
        //{//没有旋转数据就把球还原
        //	Vector3 temp=new Vector3(0,0,0);
        //	ObjectManager.Instance.RoomSphere.transform.eulerAngles=temp;
        //}
        #endregion
        //显示房间内UI
        ObjectManager.Instance.MainCanvas.SetActive(false);
        ObjectManager.Instance.SphereLoding.SetActive(false);
        ObjectManager.Instance.RoomSphere.SetActive(false);
		//IsAtHouse =true;//将当前状态设置为在房内
	}

    public void ResetAllModel()
    {
        foreach (GameObject g in loadedModels)
        {
            Destroy(g);
        }
    }

}
