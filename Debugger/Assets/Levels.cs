using UnityEngine;
using System.Collections;

public class Levels : MonoBehaviour {
    public string[] Destination;

    bool Dialoguebox = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(800, 600, 400, 400));
        if (Dialoguebox == true)
        {
            GUILayout.Label(Destination[0]);
            if (GUILayout.Button(Destination[1]))
            {
                Dialoguebox = false;
                Application.LoadLevel("Tutorial");
            }
            if (GUILayout.Button(Destination[2]))
            {
                Dialoguebox = false;
                Application.LoadLevel("TestEnvironment");
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Dialoguebox = true;
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Dialoguebox = false;
        }
    }
}
