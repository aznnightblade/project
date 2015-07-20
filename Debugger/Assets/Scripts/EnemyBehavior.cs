using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	[SerializeField]
	EnemyStatistics enemy;
	[SerializeField]
	float Speed = 5.0f;
	[SerializeField]
	bool Ranged = false;

	int maxHealth = 0;
	int currHealth = 0;


	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.magenta;
		gameObject.GetComponent<Rigidbody> ().maxAngularVelocity = Speed;
		enemy = gameObject.GetComponent<EnemyStatistics> ();
		maxHealth = currHealth = 10 * enemy.Endurance;
	}
	
	// Update is called once per frame
	void Update () {
		if (currHealth == 0)
			Destroy (gameObject);
	}
}
