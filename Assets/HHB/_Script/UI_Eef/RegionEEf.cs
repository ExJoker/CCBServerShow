using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionEEf : MonoBehaviour {
	public float TriggerTime =5f;
	private float Timer =0f;
	private int count;
	public int animaSpeed=80;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer+=Time.deltaTime;
		if(Timer>=TriggerTime)
		{
			Timer =0f;
			if(count==4)
			{
				transform.localPosition=new Vector2(-14490,transform.localPosition.y);
				count=-1;
			}
			StartCoroutine(RegionEEF());
        }
	}

	IEnumerator RegionEEF()
	{
		Debug.Log("滚动");
		int nowScreenWidth=Screen.width;
		float width = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
		width = width/(5760/nowScreenWidth);
		int TempanimaSpeed = animaSpeed/(5760/nowScreenWidth);
		for (int i = 0; i < width; i+=TempanimaSpeed)
		{
			if(i+TempanimaSpeed>width)
			{
				transform.position=new Vector2(transform.position.x+(width-i),transform.position.y);
			}
			else
			{
				transform.position=new Vector2(transform.position.x+TempanimaSpeed,transform.position.y);
			}
			yield return null;
		}
		count++;
	}

	void OnEnable()
	{
		transform.localPosition=new Vector2(-12420,transform.localPosition.y);
	}
}
