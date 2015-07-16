using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	PlayerStatistics player;
	[SerializeField]
	Transform bullet;

	[SerializeField]
	float Speed = 5;

	[SerializeField]
	float ShootDelay = 0.25f;
	bool bulletFired = false;
	float fireTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		bullet.tag = "Player Bullet";										// Tags bullets created by player as a player bullet.
		//bullet.GetComponent<BulletScript> ().Owner = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && fireTimer <= 0.0f) {
			fireTimer = ShootDelay;
			bulletFired = true;
		}

		fireTimer -= Time.deltaTime;
	}

	void FixedUpdate (){
		Vector3 pos = transform.localPosition;

		// Creates a ray based off of the mouses current position on the screen.
		// The ray is used to create a vector to then have the player look towards that point.
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		Vector3 target = hit.point;
		target.y = transform.localPosition.y; // Keeps the same depth as the player. Y is depth since we are looking down and working on an x/z plane.
		transform.LookAt (target);

		// Moves player based on input
		pos.x = pos.x + Input.GetAxisRaw ("Horizontal") * Speed * Time.deltaTime;
		pos.z = pos.z + Input.GetAxisRaw ("Vertical") * Speed * Time.deltaTime;

		transform.localPosition = pos;

		if (bulletFired == true)
			FireBullet ();
	}

	void FireBullet(){
		Vector3 rot = transform.rotation.eulerAngles;					// Creates a rotation Quaternion based on the players rotation.
		rot.x = 90;

		Instantiate (bullet, transform.localPosition, Quaternion.Euler(rot));		// Creates a new bullet.

		bulletFired = false;
	}
}
