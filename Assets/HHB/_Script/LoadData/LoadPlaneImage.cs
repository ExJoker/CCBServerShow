using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlaneImage : MonoBehaviour {

	public static LoadPlaneImage  Instance;
	public Texture2D NowSphereTexture;

	// Use this for initialization
	void Start () {
		Instance=this;
	}
	
	public void LoadPlaneImageById(string ImageId)
	{
		StopCoroutine("LoadPlaneImageToCanvas");
		StartCoroutine(LoadPlaneImageToCanvas(ImageId));
	}


	IEnumerator LoadPlaneImageToCanvas(string ImageId)
	{
		
		yield return null;
		//读取图片并显示
		ObjectManager.Instance.SphereLoding.SetActive(true);//显示加载面板
		string sql="select ImageAddres from Image WHERE id="+ImageId+";";
		List<string[]> result =new List<string[]>();
		result=DataDBContorller.Select(sql);
		Debug.Log("resultCount:"+result.Count+"----"+sql);
		string TempURL="file://" + Application.streamingAssetsPath +@result[0][0];
		Debug.Log("加载平面图片ID:"+ImageId+"       URl:"+TempURL);
		WWW www =new WWW(TempURL);
		yield return www;
		NowSphereTexture =www.texture;
		ObjectManager.Instance.PlaneImageBody.GetComponent<RawImage>().texture=NowSphereTexture;
		ObjectManager.Instance.SphereLoding.SetActive(false);//隐藏加载面板
		ObjectManager.Instance.MainCanvas.SetActive(false);//隐藏主页UI界面
		Resources.UnloadUnusedAssets();

	}
	
}
