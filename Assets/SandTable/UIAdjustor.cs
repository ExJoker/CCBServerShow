using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Canvas))]
public class UIAdjustor : MonoBehaviour {

    public Camera cam;
    public RectTransform bgRectTrans;

    private RectTransform uiRectTrans;

	// Use this for initialization
	void Start () {
        uiRectTrans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (cam == null || bgRectTrans == null) return;

        float halfAngle = cam.fieldOfView / 2;
        float bgDepth = bgRectTrans.localPosition.z;
        float bgHeight = bgRectTrans.sizeDelta.y;
        float camPitch = cam.transform.rotation.eulerAngles.x;

        float fovHypotenuse = bgDepth / Mathf.Cos(halfAngle * Mathf.Deg2Rad);
        float fovBotAndDownAngle = 90 - camPitch - halfAngle;
        float fovTopAndForwardAngle = camPitch - halfAngle;

        float uiDepth = fovHypotenuse * Mathf.Sin(fovBotAndDownAngle * Mathf.Deg2Rad);

        float transHeight = fovHypotenuse * Mathf.Cos(fovBotAndDownAngle * Mathf.Deg2Rad);
        float uiPosY = -(transHeight - uiDepth * Mathf.Tan(fovTopAndForwardAngle * Mathf.Deg2Rad)) / 2;

        float pixelHypotenuse = 0.5f * bgHeight / Mathf.Sin(halfAngle * Mathf.Deg2Rad);
        float pixelDepth = pixelHypotenuse * Mathf.Sin(fovBotAndDownAngle * Mathf.Deg2Rad);
        float pixelHeight = pixelHypotenuse * Mathf.Cos(fovBotAndDownAngle * Mathf.Deg2Rad);

        float uiHeight = pixelHeight - pixelDepth * Mathf.Tan(fovTopAndForwardAngle * Mathf.Deg2Rad);

        uiRectTrans.position = cam.transform.position + new Vector3(0, uiPosY, uiDepth);
        uiRectTrans.sizeDelta = new Vector2(uiRectTrans.sizeDelta.x, uiHeight);
	}
}
