using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	[SerializeField]
	EnemyStatistics enemy;
	[SerializeField]
	Transform bullet = null;
	[SerializeField]
	float Speed = 1.0f;
	[SerializeField]
	bool Ranged = false;
	float fireTimer = 0.0f;
	[SerializeField]
	float shotDelay = 0.25f;
	[SerializeField]

	bool bulletFired = false;
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
		if (enemy.Health <= 0) {
			PlayerStatistics player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStatistics>();
			//player.Money += enemy.MoneyDropped;
			player.Experience += enemy.ExperienceWorth;
			Destroy (gameObject);
		}

		// Do this for ranged enemies only
		if (Ranged) {
			Vector3 pos = transform.position;
			Vector3 targetPoint = target.position;
			if(fireTimer <= 0.0f && Vector3.Distance(targetPoint, pos) <= 50.0f) {
				fireTimer = shotDelay - (0.005f * enemy.Agility);
				bulletFired = true;
			}
		}

		fireTimer -= Time.deltaTime;
	}

	void FixedUpdate() {

		// Draw a vector from enemy to player and move in that direction
		Vector3 pos = transform.position;
		Vector3 targetPoint = target.position;

		Vector3 toTarget = targetPoint - pos;
		toTarget.Normalize();
		float rotation = (Mathf.Atan2(-toTarget.y, toTarget.x) * 180 / Mathf.PI) - 90;
		transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));

		if (!Ranged || (Ranged && Vector3.Distance (targetPoint, pos) > 2.0f)) {
			Vector3 distance = targetPoint - pos;
			distance.Normalize ();

			pos.x = pos.x + distance.x * Speed * Time.deltaTime;
			pos.z = pos.z + distance.z * Speed * Time.deltaTime;

			transform.position = pos;
		}

		if (bulletFired && Vector3.Distance (targetPoint, pos) <= 2.0f) {
			FireBullet();
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player" && gameObject.name == "Enemy") {
			// Player takes damage
			PlayerStatistics player = other.gameObject.GetComponent<PlayerStatistics>();
			int totalDamage = enemy.Damage - player.Defense;
			player.Health -= totalDamage;
		}
	}

	void FireBullet() {
		Vector3 pos = transform.position;
		Vector3 rot = transform.rotation.eulerAngles;
		rot.x = 90;

		Transform newBullet = Instantiate (bullet, pos, Quaternion.Euler (rot)) as Transform;
		GameObject bul = newBullet.gameObject;
		bul.GetComponent<EnemyBulletScript> ().Owner = enemy;
		newBullet.tag = "Enemy Bullet";
		//newBullet.GetComponentInChildren<SpriteRenderer> ().color = Color.red;

		bulletFired = false;
	}
}
