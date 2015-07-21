using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	[SerializeField]
	EnemyStatistics enemy;
	[SerializeField]
	float Speed = 1.0f;
	[SerializeField]
	bool Ranged = false;

	Transform target = null;



	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
		gameObject.GetComponent<Rigidbody> ().maxAngularVelocity = Speed;
		enemy = gameObject.GetComponent<EnemyStatistics> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy.Health == 0)
			Destroy (gameObject);
	}

	void FixedUpdate() {

			// Draw a vector from enemy to player and move in that direction
			Vector3 pos = transform.position;
			Vector3 targetPoint = target.position;
			Vector3 distance = target.position - pos;
			distance.Normalize ();

			pos.x = pos.x + distance.x * Speed * Time.deltaTime;
			pos.z = pos.z + distance.z * Speed * Time.deltaTime;

			transform.position = pos;


	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player" && gameObject.name == "Enemy") {
			// Player takes damage
		}
	}
}
