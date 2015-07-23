using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class BulletScript : MonoBehaviour {
	
	PlayerStatistics owner;
    PlayerMovement movement;
    public AudioClip hitSFx;
	[SerializeField]
	float Speed = 8.0f;
	Vector3 direction;
	float Distance = 0.0f;
	Vector3 StartLocation;
    
	[SerializeField]
	float InitialTravelDistance = 100.0f;
	[SerializeField]
	float IncreasedDistancePerDex = 0.250f;

    public Soundmanager sounds;
	// Use this for initialization
	void Start () {
		owner = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStatistics>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		StartLocation = transform.position;
		Distance = InitialTravelDistance + (owner.Dexterity * IncreasedDistancePerDex);

        sounds = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<Soundmanager>();
		float degrees = transform.rotation.eulerAngles.y + 90.0f;
		float radians = degrees * (Mathf.PI / 180.0f);
		direction = new Vector3(-Mathf.Cos(radians), Mathf.Sin(radians), 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		
		pos.x += (direction.x * Speed * Time.deltaTime);
		pos.z += (direction.y * Speed * Time.deltaTime);

		transform.position = pos;

		if (Vector3.Distance(pos, StartLocation) >= Distance)
			Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy") {
			EnemyStatistics enemy = col.GetComponent<EnemyStatistics>();
			enemy.Health = enemy.Health - (owner.Damage - enemy.Defense);
			Destroy(gameObject);
            sounds.Sounds[0].Play();
		}
	}

	public PlayerStatistics Owner { get { return owner; } }
}
