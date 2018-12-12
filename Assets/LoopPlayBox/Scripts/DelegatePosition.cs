using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegatePosition
{
    public delegate void Helper();
    private Helper thisHelper;

    private RectTransform targetRect;
    private Vector3 targetpos;

    public bool _IsCarryOut = false;
    public float _Speed = 0.2f;

    public void Start (RectTransform tRect, Vector3 tPos3, Helper eve = null)
    {
        thisHelper = eve;
        targetRect = tRect;
        targetpos = tPos3;
        _IsCarryOut = true;
    }
    public void NewOrder(Vector3 tPos3, Helper eve = null)
    {
        thisHelper = eve;
        targetpos = tPos3;
        _IsCarryOut = true;
    }

    public void Update ()
    {
        if (_IsCarryOut)
        {
            targetRect.localPosition = Vector3.Lerp(targetRect.localPosition, targetpos, _Speed);
            if (Vector3.Distance(targetRect.localPosition, targetpos) <= 5)
            {
                _IsCarryOut = false;
                targetRect.localPosition = targetpos;
                if (thisHelper != null)
                    thisHelper();
            }
        }
	}
}
