using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class CorruptionScript : MonoBehaviour {

	[SerializeField]
	int health = 100;
	[SerializeField]
	int defense = 2;
    public Soundmanager sounds;

    void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<Soundmanager>();
    }
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player Bullet") {
			float bulletScale = col.gameObject.GetComponentInChildren<Transform>().localScale.x / 0.2f;
			PlayerStatistics bulletOwner = col.gameObject.GetComponent<BulletScript>().Owner;

			health -= Mathf.CeilToInt(bulletOwner.Damage + bulletOwner.Damage *(bulletOwner.ChargedDamageScale * (bulletScale - 1)));
            sounds.Sounds[4].Play();
			Destroy(col.gameObject);

			if(health <= 0)
            {
                sounds.Sounds[1].Play();
                Destroy(gameObject);
            }
				
		}
	}
}
