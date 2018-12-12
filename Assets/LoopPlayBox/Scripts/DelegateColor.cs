using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DelegateColor
{
    public delegate void Helper();
    private Helper thisHelper;

    private Image targetImage;
    private Color targetColor;

    private bool _IsCarryOut = false;
    public float _Speed = 0.2f;

    public void Start (Image tImage, Color tColor, Helper eve = null)
    {
        thisHelper = eve;
        targetImage = tImage;
        targetColor = tColor;
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
                targetImage.color = Color.Lerp(targetImage.color, targetColor, _Speed);
                if (Vector4.Distance(targetImage.color, targetColor) <= 0.1f)
                {
                    _IsCarryOut = false;
                    targetImage.color = targetColor;
                    if (thisHelper != null)
                        thisHelper();
                }
            }
        }
    }
}
