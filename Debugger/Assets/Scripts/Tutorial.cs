using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public string []Corruption;
	bool Dialoguebox = false;
	// Use this for initialization
	void Start () {
	
	
	}
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(800, 600, 400, 400));
		if (Dialoguebox == true)
		{
			GUILayout.Label(Corruption[0]);
			GUILayout.Label(Corruption[1]);
			GUILayout.Label(Corruption[2]);
		}
        GUILayout.EndArea();
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
	// Update is called once per frame
	void Update () {
	
	}
}
