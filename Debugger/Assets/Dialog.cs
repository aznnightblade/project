using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

    public string[] Questions;
    bool DisplayDialog = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(700, 600,400,400));
            if(DisplayDialog==true)
            {
                GUILayout.Label(Questions[0]);
                GUILayout.Label(Questions[1]);
                GUILayout.Label(Questions[2]);
            }

            GUILayout.EndArea();
    }
    void OnTriggerEnter()
    {
        DisplayDialog = true;
    }
    void OnTriggerExit()
    {
        DisplayDialog = false;
    }
}
