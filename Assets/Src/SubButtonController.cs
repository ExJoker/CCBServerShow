using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubButtonController : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent.parent.SetAsLastSibling();
    }
    private void Update()
    {

        transform.Rotate(transform.forward, Time.deltaTime * -30);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

}
