using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : Singleton<AreaManager>
{
   
    public RectTransform showRect;
    public AreaSelf[] areas;
    private AreaSelf curArea;

    private int KeepTime = 15, WaitTime = 5;

    private DelegatePosition delegatepos = new DelegatePosition();

	void OnEnable ()
    {
        curArea = areas[Random.Range(0, areas.Length)];
        curArea.DisplayThis(showRect);
        StartCoroutine(KeepPanel());
    }

    void Update ()
    {
        delegatepos.Update();
    }

    IEnumerator KeepPanel()
    {
        yield return new WaitForSeconds(KeepTime);
        curArea.HideThis();
        delegatepos.Start(showRect, new Vector3(showRect.localPosition.x, -2600, 0));

        StartCoroutine(RandomNextArea());
    }
    IEnumerator RandomNextArea()
    {
        
        yield return new WaitForSeconds(WaitTime);
        curArea = areas[Random.Range(0, areas.Length)];
        curArea.DisplayThis(showRect);
        StartCoroutine(KeepPanel());
    }
}
