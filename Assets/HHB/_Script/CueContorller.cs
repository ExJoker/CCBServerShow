using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueContorller : MonoBehaviour {
	public static CueContorller Instance;	
	public bool ClearCanvas=false;
	private List<GameObject> LeftCuePool = new List<GameObject>();//对象池
	private List<GameObject> RightCuePool = new List<GameObject>();
	private List<GameObject> ForwordCuePool = new List<GameObject>();
	private List<GameObject> ChangeCuePool = new List<GameObject>();


	private List<GameObject> LeftCueUse = new List<GameObject>();//已使用对象
	private List<GameObject> RightCueUse = new List<GameObject>();
	private List<GameObject> ForwordCueUse = new List<GameObject>();
	private List<GameObject> ChangeCueUse = new List<GameObject>();


	private GameObject LeftCue;
	private GameObject RightCue;
	private GameObject ForwordCue;
	private GameObject ChangeCue;
	private GameObject RegionImg;
	private GameObject HouseBtn;
	private GameObject ImageItem;

	void Awake()
	{
		//Resource加载
		LeftCue=Resources.Load("RoomCue/LeftCue") as GameObject;
		RightCue=Resources.Load("RoomCue/RightCue") as GameObject;
		ForwordCue=Resources.Load("RoomCue/ForwordCue") as GameObject;
		ChangeCue=Resources.Load("RoomCue/ChangeCue") as GameObject;
		RegionImg=Resources.Load("RoomCue/RegionBtn") as GameObject;
		HouseBtn=Resources.Load("RoomCue/HouseBtn") as GameObject;
		ImageItem=Resources.Load("RoomCue/RoomImageItem") as GameObject;
		//批量实例化
		AoutInstantiate(LeftCuePool,LeftCue,5);
		AoutInstantiate(RightCuePool,RightCue,5);
		AoutInstantiate(ForwordCuePool,ForwordCue,5);
		AoutInstantiate(ChangeCuePool,ChangeCue,5);

		Instance=this;
	}
	// Use this for initialization
	void Start () {
		if(ClearCanvas)
		{
			foreach(Transform g in ObjectManager.Instance.SphereCanvas.transform)
			{
				Destroy(g.gameObject);
			}
		}
		//清空画布原有UI对象

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void AoutInstantiate(List<GameObject> Pool,GameObject g,int InstantiateNum)
	{
		for(int i=0; i<InstantiateNum; i++)
		{
			Pool.Add(GameObject.Instantiate( g,Vector3.zero,g.transform.rotation,transform));
			Pool[Pool.Count-1].SetActive(false);
		}
	}

	public GameObject GetGameObj(RoomEnum.UIType cueType)
	{
		List<GameObject> pool=null;
		List<GameObject> poolUse=null;
		GameObject cue=null;
		switch(cueType)
		{
			case RoomEnum.UIType.ChangeCue:
				pool =ChangeCuePool;
				poolUse =ChangeCueUse;
				cue=ChangeCue;
			break;
			case RoomEnum.UIType.ForwordCue:
				pool =ForwordCuePool;
				poolUse =ForwordCueUse;
				cue=ForwordCue;
			break;
			case RoomEnum.UIType.LeftCue:
				pool =LeftCuePool;
				poolUse =LeftCueUse;
				cue=LeftCue;
			break;
			case RoomEnum.UIType.RightCue:
				pool =RightCuePool;
				poolUse =RightCueUse;
				cue=RightCue;
			break;
		}

		if(pool.Count>0)
		{
			return UseGameObj(pool,poolUse);
		}
		else
		{
			pool.Add(GameObject.Instantiate( cue,Vector3.zero,cue.transform.rotation,transform));
			return UseGameObj(pool,poolUse);
		}

	}



	private GameObject UseGameObj(List<GameObject> pool,List<GameObject> poolUse)
	{
		poolUse.Add(pool[0]);
		pool.Remove(pool[0]);
		return poolUse[poolUse.Count-1];
	}

	public void ResetCuePool()
	{
		ChangeCuePool.AddRange(ChangeCueUse);
		LeftCuePool.AddRange(LeftCueUse);
		RightCuePool.AddRange(RightCueUse);
		ForwordCuePool.AddRange(ForwordCueUse);

		foreach(GameObject g in ChangeCuePool)
		{
			g.SetActive(false);
			g.transform.parent=ObjectManager.Instance.ObjectPool.transform;
		}
		foreach(GameObject g in LeftCuePool)
		{
			g.SetActive(false);
			g.transform.parent=ObjectManager.Instance.ObjectPool.transform;
		}
		foreach(GameObject g in RightCuePool)
		{
			g.SetActive(false);
			g.transform.parent=ObjectManager.Instance.ObjectPool.transform;
		}
		foreach(GameObject g in ForwordCuePool)
		{
			g.SetActive(false);
			g.transform.parent=ObjectManager.Instance.ObjectPool.transform;
		}
	}



	
}
