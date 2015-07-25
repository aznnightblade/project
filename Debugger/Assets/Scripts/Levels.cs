using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Levels : MonoBehaviour {

    public GameObject Level1;
	// Use this for initialization
	void Start () {
        Level1.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.tutorial==true)
        {
            Level1.SetActive(true);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Application.LoadLevel("NewTutorial");
        }

    }
}
