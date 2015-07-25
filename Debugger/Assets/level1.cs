using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level1 : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel("TestEnvironment");
            GameManager.tutorial = true;
        }

    }
}
