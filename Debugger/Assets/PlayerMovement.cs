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
	[SerializeField]
	float ReductionPerAgility = 0.005f;
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
			fireTimer = ShootDelay - (ReductionPerAgility * gameObject.GetComponent<PlayerStatistics>().Agility);
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
		Vector3 pos = transform.position;
		Vector3 offset = transform.rotation.eulerAngles;
		Vector3 rot = transform.rotation.eulerAngles;					// Creates a rotation Quaternion based on the players rotation.
		rot.x = 90;

		if (gameObject.GetComponent<PlayerStatistics> ().NumThreads > 1) {
			offset.y = offset.y * (Mathf.PI / 180);
			offset = new Vector3(-Mathf.Cos(offset.y), 0, Mathf.Sin(offset.y));
			offset *= 0.05f;
		}

		switch (gameObject.GetComponent<PlayerStatistics> ().NumThreads) {
		case 1:
			Instantiate (bullet, pos, Quaternion.Euler (rot));		// Creates a new bullet.
			break;
		case 2:
			Instantiate (bullet, pos - offset, Quaternion.Euler (rot));		// Creates a new bullet.
			Instantiate (bullet, pos + offset, Quaternion.Euler (rot));		// Creates a new bullet.
			break;
		case 3:
			Instantiate (bullet, pos - offset, Quaternion.Euler (rot.x, rot.y + 10, rot.z));		// Creates a new bullet.
			Instantiate (bullet, pos, Quaternion.Euler (rot));										// Creates a new bullet.
			Instantiate (bullet, pos + offset, Quaternion.Euler (rot.x, rot.y - 10, rot.z));		// Creates a new bullet.
			break;
		case 4:
			Instantiate (bullet, pos - (2 * offset), Quaternion.Euler (rot.x, rot.y + 10, rot.z));		// Creates a new bullet.
			Instantiate (bullet, pos - offset, Quaternion.Euler (rot));									// Creates a new bullet.
			Instantiate (bullet, pos + offset, Quaternion.Euler (rot));		// Creates a new bullet.
			Instantiate (bullet, pos + (2 * offset), Quaternion.Euler (rot.x, rot.y - 10, rot.z));		// Creates a new bullet.
			break;
		default:
			Instantiate (bullet, transform.localPosition, Quaternion.Euler (rot));		// Creates a new bullet.
			break;
		}

		bulletFired = false;
	}
}
