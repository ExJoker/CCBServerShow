using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaShake : MonoBehaviour
{
    public float speed = 0.2f;
    //  public Material test;
    public void Shake()
    {
        StopAllCoroutines();
        GetComponent<Image>().material.color = new Color(1, 1, 1, 0);
        StartCoroutine(shakelpha());

    }
    IEnumerator shakelpha()
    {
        while (GetComponent<Image>().material.color.a < 0.999f)
        {
            GetComponent<Image>().material.SetColor("_Color", new Color(1, 1, 1, Mathf.Lerp(GetComponent<Image>().material.color.a, 1, speed)));
            yield return null;
        }
        yield return null;
    }
}
