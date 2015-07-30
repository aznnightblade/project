using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {

	EnemyStatistics owner;

	[SerializeField]
	float speed = 8.0f;
	Vector3 direction;
	float distance = 0.0f;
	Vector3 startLocation;

	[SerializeField]
	float travelDistance = 100.0f;
    public Soundmanager sounds;
	// Use this for initialization
	void Start () {
		//owner = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyStatistics> ();
		startLocation = transform.position;
		distance = travelDistance + 0.25f * owner.Dexterity;
        sounds = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<Soundmanager>();
		float degrees = transform.rotation.eulerAngles.y + 90.0f;
		float radians = degrees * (Mathf.PI / 180.0f);
		direction = new Vector3 (-Mathf.Cos (radians), Mathf.Sin (radians), 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;

		pos.x += direction.x * speed * Time.deltaTime;
		pos.z += direction.y * speed * Time.deltaTime;

		transform.position = pos;
		if (Vector3.Distance (pos, startLocation) >= distance) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
            PlayerStatistics player = other.GetComponent<PlayerStatistics>();
            if (player.HurtTimer <= 0.0f)
            {
                int totalDamage = owner.Damage - player.Defense;
                player.Health -= totalDamage;
                player.CalculateInvulerability(totalDamage);
                sounds.Sounds[6].Play();
                Destroy(gameObject);
            }
		}
	}

	public EnemyStatistics Owner { get { return owner; } set { owner = value; } }
}
