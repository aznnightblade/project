using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	Transform spawnTrigger;
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
		spawnTrigger = gameObject.GetComponent<Transform> ();
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
				Vector3 spawnPoint = spawnTrigger.position + new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f));
				Instantiate(enemy, spawnPoint, Quaternion.Euler(Vector3.zero));
			}
			for(int i = 0; i < numSpawns_Ranged; i++) {
				Vector3 spawnPoint = spawnTrigger.position + new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f));
				Instantiate(ranged, spawnPoint, Quaternion.Euler(Vector3.zero));
			}
			for(int i = 0; i < numSpawns_Strong; i++) {
				Vector3 spawnPoint = spawnTrigger.position + new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f));
				Instantiate(strong, spawnPoint, Quaternion.Euler(Vector3.zero));
			}
			Destroy(gameObject);
		}
	}
}
