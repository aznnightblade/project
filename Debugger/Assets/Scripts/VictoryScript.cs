using UnityEngine;
using System.Collections;

public class VictoryScript : MonoBehaviour {

	[SerializeField]
	Transform VIRTORYCUBE = null;

	public void Victory() {
		Instantiate (VIRTORYCUBE, gameObject.transform.position, Quaternion.identity);
	}
}
