using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSQl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TestSql(){
		DataDBContorller.OpenSqlConnection(DataDBContorller.connectionString);
	}

	public void CloseSql(){
		DataDBContorller.CloseConnection();
	}

	public void Select()
	{
		Debug.Log((DataDBContorller.Select("select * FROM region;")[0] as string[])) ;
	}
}
