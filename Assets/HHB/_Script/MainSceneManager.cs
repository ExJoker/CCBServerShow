using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  

public class MainSceneManager : Singleton<MainSceneManager>
{

    //初始相机的位置和旋转角度
    public Vector3 initPosition = new Vector3(0, 6.4f, -7.7f);  
    public Quaternion initQuaternion = Quaternion.Euler(40f, 0, 0);
    //俯视相机的位置和旋转角度
    private Vector3 planformPosition = new Vector3(0f, 10f, 0f);
    private Quaternion planformQuaternion = Quaternion.Euler(90f, 0, 0);
    private GameObject currCamera;


    void Start()
    {        
        currCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currCamera.transform.DOMove(initPosition, 1f);
        currCamera.transform.DORotateQuaternion(initQuaternion, 1f);
    }

    void Update()
    {
      transform.eulerAngles = new Vector3(0, NewHouseUI.Instance.ry,0);
      transform.localScale = new Vector3(NewHouseUI.Instance.px, NewHouseUI.Instance.py, NewHouseUI.Instance.pz);

    }

    void FixedUpdate()
    {
        
    }

    private void ShowFrontView()
    {
       Tween tweener = currCamera.transform.DOMove(initPosition,1f);
       currCamera.transform.DORotateQuaternion(initQuaternion, 1f);
    }

    private void ShowPlanform()
    {
        currCamera.transform.DOMove(planformPosition, 1f);
        currCamera.transform.DORotateQuaternion(planformQuaternion, 1f);
    }


}
