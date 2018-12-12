using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DelegateScale
{
    public delegate void Helper();
    private Helper thisHelper;

    private RectTransform targetRect;
    private Vector3 targetSize;

    private bool _IsCarryOut = false;
    public float _Speed = 0.2f;

    public void Start (RectTransform tRect, Vector3 tPos3, Helper eve = null)
    {
        thisHelper = eve;
        targetRect = tRect;
        targetSize = tPos3;
        _IsCarryOut = true;
        invoke = 0;
    }

    int invoke = 0;
    public void Update ()
    {
        if (_IsCarryOut)
        {
            invoke += 1;
            if (invoke >= 5)
            {
                targetRect.localScale = Vector3.Lerp(targetRect.localScale, targetSize, _Speed);
                if (Vector3.Distance(targetRect.localScale, targetSize) <= 0.1f)
                {
                    _IsCarryOut = false;
                    targetRect.localScale = targetSize;
                    if (thisHelper != null)
                        thisHelper();
                }
            }
        }
    }
}
