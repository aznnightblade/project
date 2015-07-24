using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
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

	private NavMeshAgent agent = null;

	[SerializeField]
	float freezeTimer = 0.0f;

	public Vector3 screenPos;
	public Vector3 targetScreenPos;
	public Vector3 direction;
	public float rot;

	[SerializeField]
	Strategy Type = Strategy.Attack;
	[SerializeField]
	GameObject[] Flanks = null;
	bool clockwise = false;
	int flankNum = 0;

    public Soundmanager sounds;
	// Use this for initialization
	void Start () {
        sounds = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<Soundmanager>();
		agent = gameObject.GetComponent<NavMeshAgent> ();
		enemy = gameObject.GetComponent<EnemyStatistics> ();
		if (Type == Strategy.Attack) {
			agent.destination = GameObject.FindGameObjectWithTag ("Player").transform.position;
			attackTarget = GameObject.FindGameObjectWithTag ("Player").transform;

			if(Ranged) {
				agent.stoppingDistance = 2.0f;
				agent.autoBraking = true;
			}
		} else if (Type == Strategy.Flank && !Ranged) {
			Flanks = GameObject.FindGameObjectsWithTag("Melee Flank");
			flankNum = Random.Range (0, 1);
			agent.destination = Flanks [flankNum].transform.position;
		} else {
			flankNum = Random.Range (0, 1);
			attackTarget = GameObject.FindGameObjectWithTag ("Player").transform;
			Flanks = GameObject.FindGameObjectsWithTag("Ranged Flank");

			if(flankNum == 0) {
				agent.destination = Flanks[1].transform.position;
			} else {
				agent.destination = Flanks[3].transform.position;
				clockwise = true;
				flankNum = 3;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (freezeTimer <= 0.0f) {
            agent.Resume();
			if(flankNum < Flanks.Length && Type == Strategy.Flank)
				agent.destination = Flanks[flankNum].transform.position;
			else
				agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

			if (enemy.Health <= 0) {
				PlayerStatistics player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStatistics> ();
				//player.Money += enemy.MoneyDropped;
				player.Experience += enemy.ExperienceWorth;
                sounds.Sounds[2].Play();
				Destroy (gameObject);
			}

			// Do this for ranged enemies only
			if (Ranged) {
				if (fireTimer <= 0.0f && agent.remainingDistance <= 50.0f) {
					fireTimer = shotDelay - (0.005f * enemy.Agility);
					bulletFired = true;
				}
			}

			fireTimer -= Time.deltaTime;
		} else {
			freezeTimer -= Time.deltaTime;
            agent.Stop();
            transform.Translate(Vector3.zero);
		}
	}

	void FixedUpdate() {
		if (freezeTimer <= 0.0f) {
			if(!Ranged && Type == Strategy.Flank){
				if(agent.remainingDistance < 1.0f && (flankNum == 0 || flankNum == 1)){
					int rNum = Random.Range(0, 25 + Mathf.CeilToInt(enemy.Health/enemy.MaxHealth * 75));

					if(rNum > 0 && rNum < 25) {
						agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
					} else {
						agent.destination = Flanks[2].transform.position;
						flankNum = 2;
					}
				} else if(agent.remainingDistance < 1.0f && flankNum == 2) {
					agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
					flankNum = 4;
				}

				if(flankNum == 2) {
					int rNum = Random.Range(0, 100);

					if(rNum > 30 && rNum < 40) {
						agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
						flankNum = 4;
					}
				}
			} else if (Type == Strategy.Flank) {
				if(agent.remainingDistance < 1.0f) {
					if (clockwise) {
						flankNum++;

						if(flankNum >= Flanks.Length) {
							flankNum = 0;
						}

						agent.destination = Flanks[flankNum].transform.position;
					} else {
						flankNum--;

						if(flankNum <= -1) {
							flankNum = Flanks.Length - 1;
						}

						agent.destination  = Flanks[flankNum].transform.position;
					}
				}
			}
	
			if (bulletFired && agent.remainingDistance <= 2.0f) {
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
