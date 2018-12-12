using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public enum State { Idle, Show, Editor}

    public State CurrentState { get; private set; }

	// Use this for initialization
	void Start () {
        CurrentState = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
