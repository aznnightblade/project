﻿using UnityEngine;
using System.Collections;

public class DampenerScript : MonoBehaviour {

	[SerializeField]
	int health = 20;
	[SerializeField]
	float bulletScaleToDamage = 1.25f;
	[SerializeField]
	bool destroyed = false;
    public Soundmanager sounds;

    void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("Sound Manager").GetComponent<Soundmanager>();
    }
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player Bullet") {
			float bulletScale = col.transform.localScale.x / 0.5f;
			if(bulletScale >= bulletScaleToDamage && health > 0) {
				PlayerStatistics bulletOwner = col.gameObject.GetComponent<BulletScript>().Owner;
				health -= Mathf.CeilToInt (bulletOwner.Damage * 3 * ((bulletOwner.ChargedDamageScale - (bulletScaleToDamage - 1)) * (bulletScale - 1)));
                sounds.Sounds[4].Play();
				if (health <= 0) {
					destroyed = true;
                    sounds.Sounds[1].Play();
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
