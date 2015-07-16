using UnityEngine;
using System.Collections;

public class GoToTeleporter : MonoBehaviour {

	[SerializeField]
	Transform teleporter;

	[SerializeField]
	Transform teleportLocation;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material.color = Color.green;
	}
	
	void OnDrawGizmos (){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(teleportLocation.position, teleporter.localScale);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			Vector3 teleportTo = teleportLocation.transform.position;
			teleportTo.y = col.gameObject.transform.localPosition.y;

			col.gameObject.transform.localPosition = teleportTo;
		}
	}
}
