using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			PlayerStatistics player = other.gameObject.GetComponent<PlayerStatistics>();

			// If player is at least 20% below max health
			if(player.Health <= player.MaxHealth - (player.MaxHealth / 10)) {
				// Restore 10% of max health to player
				player.Health += player.MaxHealth / 10;
			} else {
				// Prevent overhealing
				player.Health = player.MaxHealth;
			}
			Destroy(gameObject);
		}
	}
}
