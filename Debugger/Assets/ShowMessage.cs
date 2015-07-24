using UnityEngine;
using System.Collections;

public class ShowMessage : MonoBehaviour {
	public Sprite Bubble;
	public GameObject text;
	// Use this for initialization
	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player") {
			text.SetActive (true);
		} 
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			text.SetActive (false);
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
