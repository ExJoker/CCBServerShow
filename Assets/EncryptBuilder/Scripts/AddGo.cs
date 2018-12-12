using UnityEngine;
using System.Collections;

public class AddGo : MonoBehaviour
{

    // Use this for initialization
    public GUIText pTxt;
    Timer test;
    int i;

    void Start()
    {
        test = new Timer(1);
        test.tick += Test;
        test.Start();

        i = 0;
        pTxt.text = i.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        test.Update();
    }

    void Test()
    {
        i++;
        pTxt.text = i.ToString();
        test.Restart();
    }
}