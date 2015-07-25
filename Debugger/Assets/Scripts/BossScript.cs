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
	float timeBetweenPhysicalHits = 1.5f;
	float hitPlayerTimer = 0.0f;

	[SerializeField]
	float activateDistance = 10.0f;

	[SerializeField]
	Transform[] shotLocations = null;

	public bool active = false;

	Random rnd = new Random();

	private NavMeshAgent agent = null;
	[SerializeField]
	Transform[] Waypoints = null;
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

			if(boss.Health <= 0) {
				gameObject.GetComponent<VictoryScript>().Victory();
				Destroy(gameObject);
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
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 targetScreenPos = Camera.main.WorldToScreenPoint (shotLocations[Random.Range(0, shotLocations.Length)].position);
		Vector3 direction = targetScreenPos - screenPos;
		direction.Normalize ();
		float rotation = -((Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI) - 90.0f);

		Transform newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(90, rotation, 0))) as Transform;
		newBullet.gameObject.GetComponent<EnemyBulletScript> ().Owner = boss;
		newBullet.gameObject.tag = "Enemy Bullet";
		newBullet.localScale *= 2;
	}

	void OnCollisionEnter(Collision Col) {
		if(Col.gameObject.tag == "Player" && hitPlayerTimer <= 0.0f) {
			PlayerStatistics player = Col.gameObject.GetComponent<PlayerStatistics>();
            if (player.HurtTimer <= 0.0f)
            {
                int totalDamage = boss.Damage - player.Defense;
                player.Health -= totalDamage;
                player.CalculateInvulerability(totalDamage);
            }
		}
	}
}
