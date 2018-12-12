using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Singleton<T>:MonoBehaviour where T:Singleton<T> {

    private static T _instance;

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

	protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
    }

    protected void OnDestory()
    {
        _instance = null;
    }

}
