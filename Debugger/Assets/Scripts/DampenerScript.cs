using UnityEngine;
using System.Collections;

public class DampenerScript : MonoBehaviour {

	[SerializeField]
	int health = 20;
	[SerializeField]
	float bulletScaleToDamage = 1.25f;
	[SerializeField]
	bool destroyed = false;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player Bullet") {
			float bulletScale = col.gameObject.GetComponentInChildren<Transform>().localScale.x / 0.2f;
			if(bulletScale >= bulletScaleToDamage && health > 0) {
				PlayerStatistics bulletOwner = col.gameObject.GetComponent<BulletScript>().Owner;
				health -= Mathf.CeilToInt (bulletOwner.Damage * ((bulletOwner.ChargedDamageScale - (bulletScaleToDamage - 1)) * (bulletScale - 1)));
				
				if (health <= 0) {
					destroyed = true;
					gameObject.GetComponent<Renderer>().material.color = Color.black;
				}
			}
			
			Destroy(col.gameObject);
		}
	}

	public void Repair() {
		health = 20;
		gameObject.GetComponent<Renderer>().material.color = Color.white;
		destroyed = false;
	}

	public bool Destroyed { get { return destroyed; } }
}
