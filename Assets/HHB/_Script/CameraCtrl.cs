using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class CameraCtrl : MonoBehaviour {

	public static CameraCtrl Instance;
    GameObject CameraMain;
    //物体的旋转角度
	[HideInInspector]
    public float x = 0.0f;
	[HideInInspector]
    public float y = 0.0f;
    //旋转速度，需要调试
    float xSpeed = 250.0f;
    float ySpeed = 130.0f;
    //限制旋转角度，物体绕x轴旋转限制，不能上下颠倒。
     float yMinLimit;
     float yMaxLimit;
	[HideInInspector]
	public bool IsActive=true;


  
    //三段方法，鼠标控制摄像头方位
    private void Start()
    {
		Instance=this;
		CameraMain =Camera.main.gameObject;
        //初始化旋转角度，记录开始时物体的旋转角度。
        Vector3 angels = CameraMain.transform.eulerAngles;
        x = angels.y;
        y = angels.x;
        yMinLimit = -35;
        yMaxLimit = 35;
    }
    private void Update()
    {
        if (LoadSphere.Instance.IsAtHouse && Input.GetMouseButton(0))
        {
            //鼠标沿x轴的移动乘以系数当作物体绕y轴的旋转角度。
            //鼠标沿y轴的移动乘以系数当作物体绕x轴的旋转角度。
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            GameManager.Instance.SendMSG("xy*" + x.ToString() + "*" + y.ToString());  //发送x和y值到大屏

        }

        //触屏方案2，未实验
        if (LoadSphere.Instance.IsAtHouse && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            x += Input.GetTouch(0).deltaPosition.x * 12 * 0.02f;
            y += Input.GetTouch(0).deltaPosition.y * 6 * 0.02f;
            GameManager.Instance.SendMSG("xy*" + x.ToString() + "*" + y.ToString());  //发送x和y值到大屏
        }


    }
    void LateUpdate()
    {
        if (LoadSphere.Instance.IsAtHouse)
        {
            //限制绕x轴的移动。
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
			if(IsActive)
            CameraMain.transform.rotation = Quaternion.Euler(y, -x, 0);
        }
    }
  
    
	  //GameManager.Instance.SendMSG("In*" + HouseId.ToString());  //UDP发送

    

  

}
