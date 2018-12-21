using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadSphere : MonoBehaviour {

	public static LoadSphere  Instance;
	public Texture2D NowSphereTexture;
	public bool IsAtHouse =false;//当前是否已经滚入房间内

	// Use this for initialization
	void Start () {
		Instance=this;
	}
	
	public void LoadSphereTextureById(string ImageId)
	{
		StopCoroutine("LoadSphereTextureToSphere");
		StartCoroutine(LoadSphereTextureToSphere(ImageId));
	}


	IEnumerator LoadSphereTextureToSphere(string ImageId)
	{
		
		yield return null;
		CueContorller.Instance.ResetCuePool();//重置对象池
		//读取图片并显示
		ObjectManager.Instance.SphereLoding.SetActive(true);
		string sql="select ImageAddres,r_x,r_y,r_z from Image WHERE id="+ImageId+";";
		List<string[]> result =new List<string[]>();
		result=DataDBContorller.Select(sql);
		Debug.Log("resultCount:"+result.Count+"----"+sql);
		string TempURL="file://" + Application.streamingAssetsPath +@result[0][0];
		Debug.Log("加载全景图片ID:"+ImageId+"       URl:"+TempURL);
		WWW www =new WWW(TempURL);
		yield return www;
		NowSphereTexture =www.texture;
		ObjectManager.Instance.RoomSphere.GetComponent<Renderer>().materials[0].mainTexture = NowSphereTexture;
		ObjectManager.Instance.RoomSphere.transform.GetChild(1).name=ImageId;//将ID存在物体名字上
		//读取标识
		List<string[]> Cueresult =new List<string[]>();

		sql="select * from roomcue where image_Id ="+ImageId+";";
		Cueresult=DataDBContorller.Select(sql);
		Debug.Log("resultCount:"+result.Count+"----"+sql);
		foreach (var item in Cueresult)
		{
			//根据数据库中存储的CueType从对象池获取一个对应的标识对象
			Debug.Log(item[2]);
			GameObject TempCue=CueContorller.Instance.GetGameObj((RoomEnum.UIType)int.Parse(item[2]));
			//设置父对象
			TempCue.transform.SetParent(ObjectManager.Instance.SphereCanvas.transform);
			//设置提示文字
			TempCue.transform.GetChild(1).GetComponent<Text>().text=item[6];
			//设置指向ID
			TempCue.transform.GetChild(2).name=item[10];
			//设置位置
			TempCue.transform.localPosition=new Vector3(float.Parse(item[3]),float.Parse(item[4]),float.Parse(item[5]));
			Debug.Log(TempCue.transform.position);
			//设置旋转
			TempCue.transform.localEulerAngles=new Vector3(float.Parse(item[7]),float.Parse(item[8]),float.Parse(item[9]));
			//显示提示
			TempCue.SetActive(true);
		}
		ObjectManager.Instance.SphereLoding.SetActive(false);//显示加载面板
        //ObjectManager.Instance.MainCanvas.SetActive(false);//隐藏主页UI界面
        ObjectManager.Instance.outputCanvas.SetActive(true);
        ObjectManager.Instance.outputCanvas.transform.GetChild(0).GetComponent<RawImage>().DOFade(1, 1.0f).
            OnComplete(delegate () {
                 //ObjectManager.Instance.outputCanvas.transform.GetChild(0).GetChild(0).DOLocalMove(Vector3.zero,1.0f).
                 //SetEase<Tween>(Ease.OutElastic);
                 ObjectManager.Instance.ShowFrame.GetComponent<RawImage>().DOFade(1, 0.50f);
                ObjectManager.Instance.MainCanvas.SetActive(false);
            });

        Resources.UnloadUnusedAssets();

        if (!IsAtHouse)
        {
            for (int i = 0; i < 51; i++)
            {
                ObjectManager.Instance.RoomSphere.transform.position = new Vector3(0, 0, -0.02f * i);   //拉近房间的动画
                ObjectManager.Instance.RoomSphere.transform.Rotate(1.8f, 0, 0);                         //调整房间的方向
                yield return new WaitForSeconds(0.01f);
            }
        } 
        //设置全景球的旋转为数据库中读取到的数据
        if (result[0][1] !="")
		{
			Vector3 temp=new Vector3(float.Parse(result[0][1]),float.Parse(result[0][2]),float.Parse(result[0][3]));
			ObjectManager.Instance.RoomSphere.transform.eulerAngles=temp;
		}
		else
		{//没有旋转数据就把球还原
			Vector3 temp=new Vector3(0,0,0);
			ObjectManager.Instance.RoomSphere.transform.eulerAngles=temp;
		}

		

		IsAtHouse =true;//将当前状态设置为在房内

	}
	
}
