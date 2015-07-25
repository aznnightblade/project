using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    public string[] Tutorial;
    bool Dialoguebox = false;
	public GameObject Bubble;
	public GameObject text;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(800, 200, 600, 600));
        GUIStyle myStyle = new GUIStyle();
        if (Dialoguebox == true)
            {
                myStyle.fontSize = 16;
                GUILayout.Label(Tutorial[0],myStyle);
                GUILayout.Label(Tutorial[1],myStyle);
                GUILayout.Label(Tutorial[2], myStyle);
                GUILayout.Label(Tutorial[3], myStyle);
            }
        GUILayout.EndArea();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            Dialoguebox = true;
			Bubble.SetActive(true);
			text.SetActive(true);
        }
       
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Dialoguebox = false;
			Bubble.SetActive(false);
			text.SetActive(false);
        }
    }
}
