using UnityEngine;
using System.Collections;

public class GoToTeleporter : MonoBehaviour {

	[SerializeField]
	Transform teleporter = null;

	[SerializeField]
	Transform teleportLocation = null;

	[SerializeField]
	GameObject dampener1 = null;
	[SerializeField]
	GameObject dampener2 = null;

	// Use this for initialization
	void Start () {

	}

	void Update() {
		if ((dampener1 == null || dampener1.GetComponent<DampenerScript>().Destroyed == true) &&
		    (dampener2 == null || dampener2.GetComponent<DampenerScript>().Destroyed == true))
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		else
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
	}
	
	void OnDrawGizmos (){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(teleportLocation.position, teleporter.localScale);
	}

	void OnTriggerEnter (Collider col)
	{
		if ((dampener1 == null || dampener1.GetComponent<DampenerScript>().Destroyed == true) &&
		    (dampener2 == null || dampener2.GetComponent<DampenerScript>().Destroyed == true)) {
			if (col.gameObject.tag == "Player") {
				Vector3 teleportTo = teleportLocation.transform.position;
				teleportTo.y = col.gameObject.transform.localPosition.y;

				col.gameObject.transform.localPosition = teleportTo;
			}
		}
	}
}
