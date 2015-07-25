using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class HealthPickUp : MonoBehaviour {

    public Soundmanager sounds;
   void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<Soundmanager>();
    }
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			PlayerStatistics player = other.gameObject.GetComponent<PlayerStatistics>();
            sounds.Sounds[3].Play();
			// If player is at least 20% below max health
			if(player.Health <= player.MaxHealth - (player.MaxHealth / 5)) {
				// Restore 10% of max health to player
				player.Health += player.MaxHealth / 5;
			} else {
				// Prevent overhealing
				player.Health = player.MaxHealth;
			}
			Destroy(gameObject);
		}
	}
}
