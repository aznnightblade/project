using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

	[SerializeField]
	EnemyStatistics  boss = null;
	[SerializeField]
	Transform bullet = null;
	[SerializeField]
	Transform target = null;
	
	float slowTimer = 0.0f;
	[SerializeField]
	float ShootDelay = 0.25f;
	[SerializeField]
	float ReductionPerAgility = 0.005f;
	float fireTimer = 0.0f;

	[SerializeField]
	float activateDistance = 2.0f;

	bool active = false;

	Random rnd = new Random();

	private NavMeshAgent agent = null;
	[SerializeField]
	Transform[] Waypoints;
	Vector3 destination = Vector3.zero;

	void Start() {
		agent = gameObject.GetComponent<NavMeshAgent>();
		destination = Waypoints[Random.Range(0,Waypoints.Length)].transform.position;
	}

	void Update() {
		if (active == false) {
			if(Vector3.Distance(target.position, transform.position) <= activateDistance){
				active = true;
			}
		} else {
			if(gameObject.GetComponent<NavMeshAgent>().remainingDistance <= 3) {
				destination = Waypoints[Random.Range(0,Waypoints.Length)].transform.position;
			}
		}
	}
}
