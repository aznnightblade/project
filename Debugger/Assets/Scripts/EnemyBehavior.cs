using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	enum Strategy { Attack, Flank };

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
	bool bulletFired = false;
	Transform target = null;
	Transform attackTarget = null;

	[SerializeField]
	float freezeTimer = 0.0f;

	public Vector3 screenPos;
	public Vector3 targetScreenPos;
	public Vector3 direction;
	public float rot;

	[SerializeField]
	Strategy Type = Strategy.Attack;
	[SerializeField]
	Transform[] Flanks = null;
	bool clockwise = false;
	int flankNum = 0;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
		gameObject.GetComponent<Rigidbody> ().maxAngularVelocity = Speed;
		enemy = gameObject.GetComponent<EnemyStatistics> ();
		if (Type == Strategy.Attack) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			attackTarget = target;
		} else if (Type == Strategy.Flank && !Ranged) {
			target = Flanks [Random.Range (0, 1)];
		} else {
			int rNum = Random.Range (0, 1);
			attackTarget = GameObject.FindGameObjectWithTag ("Player").transform;

			if(rNum == 0) {
				target = Flanks[1];
				flankNum = 1;
			} else {
				target = Flanks[3];
				clockwise = true;
				flankNum = 3;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (freezeTimer <= 0.0f) {
			if (enemy.Health <= 0) {
				PlayerStatistics player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStatistics> ();
				//player.Money += enemy.MoneyDropped;
				player.Experience += enemy.ExperienceWorth;
				Destroy (gameObject);
			}

			// Do this for ranged enemies only
			if (Ranged) {
				Vector3 pos = transform.position;
				Vector3 targetPoint = target.position;
				if (fireTimer <= 0.0f && Vector3.Distance (targetPoint, pos) <= 50.0f) {
					fireTimer = shotDelay - (0.005f * enemy.Agility);
					bulletFired = true;
				}
			}

			fireTimer -= Time.deltaTime;
		} else {
			freezeTimer -= Time.deltaTime;
		}
	}

	void FixedUpdate() {
		if (freezeTimer <= 0.0f) {
			// Draw a vector from enemy to player and move in that direction
			Vector3 pos = transform.position;
			Vector3 targetPoint = target.position;

			if (!Ranged || (Ranged && Vector3.Distance (attackTarget.position, pos) > 2.0f && Type == Strategy.Attack) ||
			    (Ranged && Type == Strategy.Flank)) {
				Vector3 distance = target.position - pos;
				distance.Normalize ();

				pos.x = pos.x + distance.x * Speed * Time.deltaTime;
				pos.z = pos.z + distance.z * Speed * Time.deltaTime;

				transform.position = pos;

				if(!Ranged){
					if(Vector3.Distance(pos, targetPoint) < 1.0f && (target == Flanks[0] || target == Flanks[1])){
						int rNum = Random.Range(0, 25 + Mathf.CeilToInt(enemy.Health/enemy.MaxHealth * 75));

						if(rNum > 0 && rNum < 25)
							target = GameObject.FindGameObjectWithTag("Player").transform;
						else
							target = Flanks[2];
					} else if(Vector3.Distance(pos, targetPoint) < 1.0f && target == Flanks[2]) {
						target = GameObject.FindGameObjectWithTag("Player").transform;
					}

					if(target == Flanks[2]) {
						int rNum = Random.Range(0, 100);

						if(rNum > 30 && rNum < 40)
							target = GameObject.FindGameObjectWithTag("Player").transform;
					}
				} else {
					if(Vector3.Distance(pos, targetPoint) < 1.0f) {
						if (clockwise) {
							flankNum++;

							if(flankNum >= Flanks.Length) {
								flankNum = 0;
							}

							target = Flanks[flankNum];
						} else {
							flankNum--;

							if(flankNum <= -1) {
								flankNum = Flanks.Length - 1;
							}

							target  = Flanks[flankNum];
						}
					}
				}
			}

			if (bulletFired && Vector3.Distance (targetPoint, pos) <= 2.0f) {
				FireBullet ();
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			// Player takes damage
			PlayerStatistics player = other.gameObject.GetComponent<PlayerStatistics>();
			int totalDamage = enemy.Damage - player.Defense;
			player.Health -= totalDamage;
		}
	}

	void FireBullet() {
		screenPos = Camera.main.WorldToScreenPoint(transform.position);
		targetScreenPos = Camera.main.WorldToScreenPoint (attackTarget.position);
		direction = targetScreenPos - screenPos;
		direction.z = 0;
        direction.Normalize();
        rot = -((Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI) - 90.0f);
		
		Transform newBullet = Instantiate (bullet, transform.position, Quaternion.Euler (90, rot, 0)) as Transform;
		GameObject bul = newBullet.gameObject;
		bul.GetComponent<EnemyBulletScript> ().Owner = enemy;
		newBullet.tag = "Enemy Bullet";
		//newBullet.GetComponentInChildren<SpriteRenderer> ().color = Color.red;

		bulletFired = false;
	}

	public float FreezeTimer {
		get { return freezeTimer; }
		set { freezeTimer = value; }
	}
}
