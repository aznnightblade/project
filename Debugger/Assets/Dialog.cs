using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    public string[] Tutorial;
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
                GUILayout.Label(Tutorial[0]);
                GUILayout.Label(Tutorial[1]);
                GUILayout.Label(Tutorial[2]);
            }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
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
