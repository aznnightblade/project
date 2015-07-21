﻿using UnityEngine;
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
	float timeBetweenPhysicalHits = 1.5f;
	float hitPlayerTimer = 0.0f;

	[SerializeField]
	float activateDistance = 10.0f;

	public bool active = false;

	Random rnd = new Random();

	private NavMeshAgent agent = null;
	[SerializeField]
	Transform[] Waypoints;
	Vector3 destination = Vector3.zero;

	void Start() {
		agent = gameObject.GetComponent<NavMeshAgent>();
		gameObject.GetComponent<NavMeshAgent>().destination = Waypoints[Random.Range(0,Waypoints.Length)].transform.position;
	}

	void Update() {
		if (active == false) {
			if(Vector3.Distance(target.position, transform.position) <= activateDistance){
				active = true;
			}
		} else {
			if(gameObject.GetComponent<NavMeshAgent>().remainingDistance <= 3) {
				gameObject.GetComponent<NavMeshAgent>().destination = Waypoints[Random.Range(0,Waypoints.Length)].transform.position;
			}

			if(fireTimer <= 0.0f){
				CreateBullet();
				fireTimer = ShootDelay - (boss.Agility * ReductionPerAgility);
			}

			fireTimer -= Time.deltaTime;
			hitPlayerTimer -=Time.deltaTime;
		}
	}

	void CreateBullet(){
		Vector3 direction = target.transform.position - transform.position;
		direction.Normalize ();
		float rotation = (Mathf.Atan2(-direction.y, direction.x) * 180 / Mathf.PI) - 90;

		Transform newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(90, rotation, 0))) as Transform;
		newBullet.gameObject.GetComponent<EnemyBulletScript> ().Owner = boss;
		newBullet.gameObject.tag = "Enemy Bullet";
		newBullet.localScale *= 2;
	}

	void OnCollisionEnter(Collision Col) {
		if(Col.gameObject.tag == "Player" && hitPlayerTimer <= 0.0f) {
			PlayerStatistics player = Col.gameObject.GetComponent<PlayerStatistics>();
			player.Health = player.Health - (boss.Damage - player.Defense);
			hitPlayerTimer = timeBetweenPhysicalHits;
		}
	}
}