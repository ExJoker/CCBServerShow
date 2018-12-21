using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {
	public static ObjectManager Instance;
	public GameObject MainCanvas;
	public GameObject ObjectPool;
	public GameObject MainCamera;
	[SerializeField,Header("Sphere")]
	public GameObject RoomSphere;
	public GameObject SphereCanvas;
	public GameObject SphereLoding;
	public GameObject PlaneImageBody;//平面图容器
	public GameObject PlaneImageCanvas;//平面图画布
	[SerializeField,Header("区域显示控制")]
	public GameObject outputCanvas;
	public GameObject Scrollbar;
	[SerializeField,Header("房源信息控制")]
	public GameObject HousePlan;
	public GameObject HouseMessage;
	[SerializeField,Header("房源外景缩略图")]
	public GameObject Thumbnail;
	public GameObject ThumbnailLoading;
	[SerializeField,Header("房间内UI控制")]
	public GameObject RoomCanvasLoading;
	public GameObject ShowFrame;
	public GameObject RoomCanvas_ScrollBody;
	public GameObject RoomCanvas_HidenImageItemBtn;
	public GameObject RoomCanvas_HouseMessage;
	void Awake()
	{
		Instance =this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
