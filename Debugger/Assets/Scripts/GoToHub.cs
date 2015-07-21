using UnityEngine;
using System.Collections;

public class GoToHub : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		Application.LoadLevel("HudWorld");
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
