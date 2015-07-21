using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	BoxCollider spawnTrigger;
	[SerializeField]
	GameObject enemy = null;
	[SerializeField]
	GameObject ranged = null;
	[SerializeField]
	GameObject strong = null;
	[SerializeField]
	int numSpawns_Enemy = 1;
	[SerializeField]
	int numSpawns_Ranged = 1;
	[SerializeField]
	int numSpawns_Strong = 1;

	// Use this for initialization
	void Start () {
		spawnTrigger = gameObject.GetComponent<BoxCollider> ();
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
			for(int i = 0; i < numSpawns_Enemy; i++) {


			}
			Destroy(gameObject);
		}
	}
}
