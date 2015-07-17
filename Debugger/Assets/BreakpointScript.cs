using UnityEngine;
using System.Collections;

public class BreakpointScript : MonoBehaviour {

	[SerializeField]
	float freezeTime = 1.5f;

	[SerializeField]
	float shotDelay = 3.0f;

	[SerializeField]
	float lifeSpan = 5.0f;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		lifeSpan -= Time.deltaTime;

		if (lifeSpan <= 0.0f)
			Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (gameObject.tag == "Player Breakpoint" && col.tag == "Enemy") {
		}

		if (gameObject.tag == "Enemy Breakpoint" && col.tag == "Player") {
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().FreezeTimer = freezeTime;
		}
	}

	public float ShotDelay { get { return shotDelay; } }
}