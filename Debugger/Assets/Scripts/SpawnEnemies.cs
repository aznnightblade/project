using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Destroy(gameObject);
		}
	}
}
